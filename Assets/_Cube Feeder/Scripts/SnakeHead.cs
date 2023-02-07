using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    SnakeMovement snake;

    private void Start()
    {
        snake = FindObjectOfType<SnakeMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            snake.GrowSnake();
            SpwanFood spawnFood = FindObjectOfType<SpwanFood>();
            if(spawnFood != null)
                spawnFood.SpawnNewFood();
        }
    }
}
