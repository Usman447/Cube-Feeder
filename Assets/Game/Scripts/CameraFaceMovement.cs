using UnityEngine;

public class CameraFaceMovement : MonoBehaviour
{
    [SerializeField] Transform FollowTarget;
    [SerializeField] float DistanceFromTarget = 40f;
    [Range(0f, 3f)] public float MultiplyFactor = 2.5f;

    public enum Face
    {
        X_Positive, Y_Positive, Z_Positive, X_Negative, Y_Negative, Z_Negative
    }

    [SerializeField] Face face;

    private void LateUpdate()
    {
        if (face == Face.X_Positive)
        {
            transform.position = new Vector3(DistanceFromTarget, FollowTarget.position.y * MultiplyFactor, FollowTarget.position.z * MultiplyFactor);
            transform.LookAt(FollowTarget);
        }
        else if (face == Face.X_Negative)
        {
            transform.position = new Vector3(-DistanceFromTarget, FollowTarget.position.y * MultiplyFactor, FollowTarget.position.z * MultiplyFactor);
            transform.LookAt(FollowTarget);
        }
        else if (face == Face.Y_Negative)
        {
            transform.position = new Vector3(FollowTarget.position.x * MultiplyFactor, -DistanceFromTarget, FollowTarget.position.z * MultiplyFactor);
            transform.LookAt(FollowTarget, Vector3.forward);
        }
        else if (face == Face.Y_Positive)
        {
            transform.position = new Vector3(FollowTarget.position.x * MultiplyFactor, DistanceFromTarget, FollowTarget.position.z * MultiplyFactor);
            transform.LookAt(FollowTarget, Vector3.forward);
        }
        else if (face == Face.Z_Negative)
        {
            transform.position = new Vector3(FollowTarget.position.x * MultiplyFactor, FollowTarget.position.y * MultiplyFactor, -DistanceFromTarget);
            transform.LookAt(FollowTarget);
        }
        else if (face == Face.Z_Positive)
        {
            transform.position = new Vector3(FollowTarget.position.x * MultiplyFactor, FollowTarget.position.y * MultiplyFactor, DistanceFromTarget);
            transform.LookAt(FollowTarget);
        }
    }
}
