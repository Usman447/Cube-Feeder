using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI FpsText;

    void Start()
    {
        Application.targetFrameRate = 60;
    }


    private void Update()
    {
        int fpsVal = Mathf.RoundToInt((1 / Time.deltaTime));
        FpsText.text = fpsVal.ToString();

    }
}
