using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    SnakeMovement player;
    bool isDieCall = false;

    private void Start()
    {
        player = FindObjectOfType<SnakeMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Body") && !isDieCall)
        {
            Debug.Log("Snake Die");
            isDieCall = true;
            player.DestoryPlayer(Convert.ToInt32(other.name));
        }

        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            player.SnakeEatFood();
        }

        // Destory last body part of the snake
        if (other.CompareTag("Decrease"))
        {
            Destroy(other.gameObject);
            player.SnakeSizeDcrease();
        }
    }
}
