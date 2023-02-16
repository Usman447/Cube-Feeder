using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DanielLochner.Assets.SimpleScrollSnap;

public class UICanvasManager : MonoBehaviour
{
    public GameObject[] UISelectedCharacters;
    public int currentSelectedCharacter = 0;

    SimpleScrollSnap scrollSnap;

    private void Start()
    {
        scrollSnap = FindObjectOfType<SimpleScrollSnap>();
        QualitySettings.SetQualityLevel(3, true);
    }

    public void PlayButton()
    {
        Debug.Log("Button Pressed");
        StartCoroutine(LoadGameAsyncScene());
    }

    IEnumerator LoadGameAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        //SelectCharacter();
        SelectEnvironment();
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }

    void SelectCharacter()
    {
        PlayerPrefs.SetInt("Character", currentSelectedCharacter);
    }

    void SelectEnvironment()
    {
        PlayerPrefs.SetInt("Character", scrollSnap.SelectedPanel);
    }


    public void OnSelectCharacterToggle(int index)
    {
        if(index != currentSelectedCharacter)
        {
            UISelectedCharacters[currentSelectedCharacter].SetActive(false);
            UISelectedCharacters[index].SetActive(true);
            currentSelectedCharacter = index;
            SelectCharacter();
        }
    }

}
