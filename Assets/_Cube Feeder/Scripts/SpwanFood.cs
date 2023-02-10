using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanFood : MonoBehaviour
{
    public GameObject MeshObject;

    [Header("Food Types")]
    public GameObject[] SimpleFood;
    public GameObject[] PowerFood;

    public int StartingFood = 20;
    public float FoodDepth = 1;
    public LayerMask layer;

    Mesh mesh;

    private void Start()
    {
        Physics.queriesHitBackfaces = true;
        mesh = MeshObject.GetComponent<MeshFilter>().sharedMesh;
        for (int i = 0; i < StartingFood; i++)
        {
            SpawnNewFood();
        }
    }

    public void SpawnNewFood()
    {
        StartCoroutine(SpawnFood());
    }

    IEnumerator SpawnFood()
    {
        GameObject food = GetRandomFood();

        if (Physics.Raycast(food.transform.position, food.transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, layer))
        {
            food.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            Vector3 targetPosition = hit.point;
            targetPosition += hit.normal * FoodDepth;
            food.transform.position = targetPosition;
        }
        food.transform.parent = transform;
        yield return null;
    }

    GameObject GetRandomFood()
    {
        Vector3 position = mesh.vertices[Random.Range(0, mesh.vertices.Length)];
        Vector3 normal = mesh.normals[Random.Range(0, mesh.vertices.Length)];

        int randVal = Random.Range(1, 8);
        if(randVal == 3)
        {
            return Instantiate(PowerFood[0], position, Quaternion.FromToRotation(Vector3.forward, normal));
        }
        else if (randVal == 4)
        {
            return Instantiate(PowerFood[1], position, Quaternion.FromToRotation(Vector3.forward, normal));
        }
        else
        {
            return Instantiate(SimpleFood[Random.Range(0, SimpleFood.Length)], position, Quaternion.FromToRotation(Vector3.forward, normal));
        }
    }

}
