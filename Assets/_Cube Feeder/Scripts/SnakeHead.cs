using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    SnakeMovement snake;
    SpwanFood spawnFood;
    bool isSnakeTransparent = false;

    private void Start()
    {
        snake = FindObjectOfType<SnakeMovement>();
        spawnFood = FindObjectOfType<SpwanFood>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Body") && !isSnakeTransparent)
        {
            Debug.Log("Die and size " + snake.snakeBodySize);
        }

        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            snake.GrowSnake();
            if(spawnFood != null)
                spawnFood.SpawnNewFood();
        }


        // Apply snake transparancy
        if (other.CompareTag("Invisible"))
        {
            Debug.Log("Invisible");
            if (!isSnakeTransparent)
            {
                Destroy(other.gameObject);
                snake.TransparentSnake();
                isSnakeTransparent = true;
                Invoke(nameof(SnakeTransprencyRestore), 4f);
            }
        }

        // Destory last body part of the snake
        if (other.CompareTag("Decrease"))
        {
            Debug.Log("Decrease");
            Destroy(other.gameObject);
            if (!isSnakeTransparent)
            {
                snake.DecreaseSnake();
                if (spawnFood != null)
                    spawnFood.SpawnNewFood();
            }
        }
    }

    void SnakeTransprencyRestore()
    {
        snake.TransparentSnake();
        isSnakeTransparent = false;
    }
}
