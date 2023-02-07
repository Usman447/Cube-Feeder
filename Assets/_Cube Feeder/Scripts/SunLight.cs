using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class SunLight : MonoBehaviour
{
    public Quaternion Offset;
    public Transform VirtualCam;

    private void LateUpdate()
    {
        if (VirtualCam == null)
            return;
        transform.rotation = VirtualCam.rotation * Offset;

    }
}
