using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    private void OnDestroy()
    {
        var sonar = FindObjectOfType<SimpleSonarShader_Object>();
        if(sonar != null)
            sonar.StartSonarRing(transform.position, 2f);
    }
}
