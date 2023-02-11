using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanFood : MonoBehaviour
{
    public GameObject MeshObject;

    [Header("Food Types")]
    public GameObject[] SimpleFood;
    public GameObject PowerFood;

    public ushort StartingFood = 20;
    public float FoodDepth = 1;
    public LayerMask layer;

    ushort currentFoods = 0;
    ushort currentPowerFoods = 0;

    ushort maxFoodCount = 0;
    ushort maxPowerFoodCount = 0;

    Mesh mesh;

    private void Start()
    {
        Physics.queriesHitBackfaces = true;
        mesh = MeshObject.GetComponent<MeshFilter>().sharedMesh;

        maxFoodCount = (ushort)(StartingFood - 2);
        maxPowerFoodCount = (ushort)(StartingFood - maxFoodCount);


        for (int i = 0; i < StartingFood; i++)
        {
            SpawnNewFood();
        }
    }

    public void SpawnNewFood()
    {
        StartCoroutine("SpawnFood");
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
        Vector3 normal = mesh.normals[Random.Range(0, mesh.normals.Length)];

        GameObject food = null;

        if(currentFoods < maxFoodCount)
        {
            currentFoods++;
            food = Instantiate(SimpleFood[Random.Range(0, SimpleFood.Length)], position, Quaternion.FromToRotation(Vector3.forward, normal));
        }
        else if(currentPowerFoods < maxPowerFoodCount)
        {
            currentPowerFoods++;
            food = Instantiate(PowerFood, position, Quaternion.FromToRotation(Vector3.forward, normal));
        }
        else
        {
            currentFoods++;
            food = Instantiate(SimpleFood[Random.Range(0, SimpleFood.Length)], position, Quaternion.FromToRotation(Vector3.forward, normal));
        }

        return food;
    }

    public void FoodEaten()
    {
        currentFoods--;
    }

    public void PowerFoodEaten()
    {
        currentPowerFoods--;
    }
}
