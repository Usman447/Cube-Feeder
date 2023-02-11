using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI FpsText;
    public int TargetFrameRate = 60;


    void Start()
    {
        Application.targetFrameRate = 60;
    }


    private void Update()
    {
        Application.targetFrameRate = TargetFrameRate;

        int fpsVal = Mathf.RoundToInt((1 / Time.deltaTime));
        FpsText.text = fpsVal.ToString();
    }
}
