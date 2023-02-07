using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    public Transform[] views; // X+, Y+, Z+
    [Range(0f, 10f)] public float transitionSpeed = 0.5f;
    Transform currentActiveView;
    int startingView = 0;


    private void Start()
    {
        currentActiveView = views[startingView];
    }

    public void SetCamera(int _CamNumber)
    {
        currentActiveView = views[_CamNumber];
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentActiveView.position, transitionSpeed * Time.deltaTime);

        Vector3 currentAngle = new Vector3(
         Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentActiveView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
         Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentActiveView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
         Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentActiveView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

        transform.eulerAngles = currentAngle;

    }
}
