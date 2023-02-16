using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public CharacterData[] characters;

    [Header("UI Stuff")]
    public TextMeshProUGUI SnakeSize;
    public TextMeshProUGUI FpsText;
    public int TargetFrameRate = 60;

    SnakeMovement player;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        player = FindObjectOfType<SnakeMovement>();

        int characterType = PlayerPrefs.GetInt("Character", 0);

        player.BodyPrefab = characters[characterType].BodyPrefab;
        player.HeadPrefab = characters[characterType].HeadPrefab;
        player.Depth = characters[characterType].BodyDepth;
        player.transform.position = characters[characterType].SpawnPosition;
        player.GapBetweenBodies = characters[characterType].Gap;
    }

    private void Start()
    {
        QualitySettings.SetQualityLevel(2, true);
    }

    private void Update()
    {
        int fpsVal = Mathf.RoundToInt((1 / Time.deltaTime));
        FpsText.text = fpsVal.ToString();
    }
}
