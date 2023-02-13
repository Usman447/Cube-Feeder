using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DanielLochner.Assets.SimpleScrollSnap;

public class UICanvasManager : MonoBehaviour
{
    SimpleScrollSnap scrollSnap;

    public float swipeRange;
    public float tapRange;

    Vector2 startTouchPosition, currentPosition;
    bool stopTouch = false;

    private void Start()
    {
        scrollSnap = FindObjectOfType<SimpleScrollSnap>();
    }


    private void Update()
    {
        Swipe();
    }

    public void OnCharacterSelected()
    {
        Debug.Log(scrollSnap.SelectedPanel);
    }

    public void PlayButton()
    {
        Debug.Log("Button Pressed");
        SceneManager.LoadScene(1);
    }

    void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;

            if (!stopTouch)
            {
                if (Distance.x < -swipeRange) // Left
                {
                    scrollSnap.GoToNextPanel();
                    stopTouch = true;
                }
                else if (Distance.x > swipeRange) // Right
                {
                    scrollSnap.GoToPreviousPanel();
                    stopTouch = true;
                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
        }
    }

}
