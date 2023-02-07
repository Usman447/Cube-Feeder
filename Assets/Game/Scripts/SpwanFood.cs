using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanFood : MonoBehaviour
{
    [SerializeField] GameObject FoodPrefab;

    [SerializeField] int StartingFood = 20;

    [SerializeField] float Height = 11;

    private void Start()
    {
        float d = Height;
        for (int i = 0; i < StartingFood; i++)
        {
            SpawnNewFood(0, -5.5f, 8.5f, d);
            SpawnNewFood(1, -8.5f, 8.5f, d);
            SpawnNewFood(2, -8.5f, 8.5f, d);
            SpawnNewFood(3, -8.5f, 8.5f, d);
            SpawnNewFood(4, -8.5f, 8.5f, d);
            SpawnNewFood(5, -8.5f, 8.5f, d);
            d += 0.0001f;
        }
    }

    public void SpawnNewFood()
    {
        GameObject food = Instantiate(FoodPrefab, GetRandomPoint(Random.Range(0, 6), -8.5f, 8.5f, Height), Quaternion.identity);
        food.transform.parent = transform;
    }

    void SpawnNewFood(int face, float min, float max, float distance)
    {
        GameObject food = Instantiate(FoodPrefab, GetRandomPoint(face, min, max, distance), Quaternion.identity);
        food.transform.parent = transform;
    }

    Vector3 GetRandomPoint(int face, float min, float max, float depth)
    {
        Vector3 RandPoint = Vector3.zero;
        if (face == 0)
        {
            float point1 = Random.Range(min, max);
            float point2 = Random.Range(min, max);
            RandPoint = new Vector3(depth, point1, point2);
        }
        else if (face == 1)
        {
            float point1 = Random.Range(min, max);
            float point2 = Random.Range(min, max);
            RandPoint = new Vector3(point1, depth, point2);
        }
        else if (face == 2)
        {
            float point1 = Random.Range(min, max);
            float point2 = Random.Range(min, max);
            RandPoint = new Vector3(point1, point2, depth);
        }
        else if (face == 3)
        {
            float point1 = Random.Range(min, max);
            float point2 = Random.Range(min, max);
            RandPoint = new Vector3(-depth, point1, point2);
        }
        else if (face == 4)
        {
            float point1 = Random.Range(min, max);
            float point2 = Random.Range(min, max);
            RandPoint = new Vector3(point1, -depth, point2);
        }
        else if (face == 5)
        {
            float point1 = Random.Range(min, max);
            float point2 = Random.Range(min, max);
            RandPoint = new Vector3(point1, point2, -depth);
        }

        return RandPoint;
    }
}
