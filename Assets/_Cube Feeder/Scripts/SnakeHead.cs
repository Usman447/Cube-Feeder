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
        if (other.CompareTag("Body"))
        {
            Debug.Log("Die and size " + snake.snakeBodySize);
        }

        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            snake.GrowSnake();
            SpwanFood spawnFood = FindObjectOfType<SpwanFood>();
            if(spawnFood != null)
                spawnFood.SpawnNewFood();
        }

        if (other.CompareTag("PowerFood"))
        {
            Debug.Log("Its a power food");
            Destroy(other.gameObject);
            snake.DecreaseSnake();
            SpwanFood spawnFood = FindObjectOfType<SpwanFood>();
            if (spawnFood != null)
                spawnFood.SpawnNewFood();
        }
    }
}
