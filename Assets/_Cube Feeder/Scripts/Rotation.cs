using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector3 ObjectRotation;

    private void Update()
    {
        transform.rotation *= Quaternion.Euler(ObjectRotation * Time.deltaTime);
    }
}
