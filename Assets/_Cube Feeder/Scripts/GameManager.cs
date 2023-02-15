using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class CharactersData
    {
        public GameObject[] Heads;
        public GameObject[] Bodies;

        public GameObject GetHead(int index)
        {
            return Heads[index];
        }

        public GameObject GetBody(int index)
        {
            return Bodies[index];
        }

    }



    public TextMeshProUGUI FpsText;
    public int TargetFrameRate = 60;

    public CharactersData characters;
    SnakeMovement player;


    private void Awake()
    {
        Application.targetFrameRate = 60;
        player = FindObjectOfType<SnakeMovement>();

        int characterType = PlayerPrefs.GetInt("Character", 0);
        player.BodyPrefab = characters.GetBody(characterType);
        player.HeadPrefab = characters.GetHead(characterType);
    }


    private void Update()
    {
        Application.targetFrameRate = TargetFrameRate;

        int fpsVal = Mathf.RoundToInt((1 / Time.deltaTime));
        FpsText.text = fpsVal.ToString();
    }
}
