using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Body") && !gameManager.isSnakeTransparent)
        {
            Debug.Log("Snake Die");
        }

        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            gameManager.SnakeEatFood();
            gameManager.spawnFood.FoodEaten();
        }

        // Apply snake transparancy
        if (other.CompareTag("Invisible"))
        {
            gameManager.SnakeInvisiblity(other);
            gameManager.spawnFood.PowerFoodEaten();
        }

        // Destory last body part of the snake
        if (other.CompareTag("Decrease"))
        {
            Destroy(other.gameObject);
            gameManager.SnakeSizeDcrease();
            gameManager.spawnFood.PowerFoodEaten();
        }
    }

}
