using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestScript : MonoBehaviour
{
    public ToggleGroup toggleGroup;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Toggle theActiveToggle = toggleGroup.GetFirstActiveToggle();
            Debug.Log("It worked! " + theActiveToggle.gameObject.name);
        }
    }
}
