using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SnakeMovement : MonoBehaviour
{
    [Header("Snake Setup")]
    [SerializeField] int GapBetweenBodies = 12;
    [SerializeField] float MoveSpeed = 7f;
    [SerializeField] int MaxPositionCount = 30000;
    [SerializeField] GameObject BodyPrefab;
    [SerializeField] GameObject HeadPrefab;


    [Header("Raycast")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float raycastDistance = 1f;
    public enum UpAxis
    {
        X = 0,
        Y = 1,
        Z = 2,
        NegativeX = 3,
        NegativeY = 4,
        NegativeZ = 5,
    }
    public UpAxis upAxis;


    [Header("UI Stuff")]
    [SerializeField] TextMeshProUGUI FpsText;
    [SerializeField] TextMeshProUGUI SnakeSize;


    // Private Variables
    Vector3 moveDirection;
    Vector3 NewBodyPoint;
    Vector3 dir = Vector3.zero;
    List<GameObject> BodyParts = new List<GameObject>();
    List<Vector3> PositionHistory = new List<Vector3>();

    int snakeBodySize = 0;
    bool isFirst = true;

    private void Start()
    {
        //transform.position = new Vector3(11f, 0f, -7.5f);

        moveDirection = new Vector3Int(0, 0, 0); // Starting Directtion (x, y, z)

        GameObject body = Instantiate(HeadPrefab);
        BodyParts.Add(body);

        PositionHistory.Insert(0, transform.position);
    }

    private void Update()
    {
        int fpsVal = Mathf.RoundToInt((1 / Time.deltaTime));
        FpsText.text = fpsVal.ToString();

        dir = TouchMove.MoveDir;

        if (dir.magnitude > 0.3f)
        {
            moveDirection = dir;
        }
        
        ContinuousMovement();
        HandleBodyPartsMovement();
    }

    void ContinuousMovement()
    {
        transform.Translate(moveDirection * MoveSpeed * Time.deltaTime, Space.Self);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, groundLayer))
        {
            transform.position = hit.point;
            transform.rotation = GetTargetRotation(hit.normal);
        }
    }
    

    private Quaternion GetTargetRotation(Vector3 normal)
    {
        Vector3 _upAxis = GetUpAxis();
        return Quaternion.FromToRotation(_upAxis, normal) * transform.rotation;
    }

    private Vector3 GetUpAxis()
    {
        Vector3 _upAxis = transform.up;
        switch (upAxis)
        {
            case UpAxis.X:
                _upAxis = transform.right;
                break;
            case UpAxis.Y:
                _upAxis = transform.up;
                break;
            case UpAxis.Z:
                _upAxis = transform.forward;
                break;
            case UpAxis.NegativeX:
                _upAxis = -transform.right;
                break;
            case UpAxis.NegativeY:
                _upAxis = -transform.up;
                break;
            case UpAxis.NegativeZ:
                _upAxis = -transform.forward;
                break;
        }
        return _upAxis;
    }

    void HandleBodyPartsMovement()
    {
        PositionHistory.Insert(0, transform.position);
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionHistory[Mathf.Min(index * GapBetweenBodies, PositionHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * MoveSpeed * Time.deltaTime;
            
            body.transform.LookAt(point, -transform.forward);

            NewBodyPoint = point;
            index++;
        }

        if(PositionHistory.Count > MaxPositionCount)
            PositionHistory.RemoveAt(MaxPositionCount);
    }

    public void TurnTowards(Transform trans, Vector3 turnTo)
    {
        Vector3 direction = turnTo - trans.position;
        direction.y = 0;
        int fraction = 1; // might be needed in future
        Quaternion targetRotation = Quaternion.LookRotation(direction, -Vector3.forward);
        float desiredRotation = Mathf.RoundToInt(targetRotation.eulerAngles.y);
        float currentRotation = Mathf.RoundToInt(trans.rotation.eulerAngles.y);
        if (desiredRotation < currentRotation - 1 * fraction ||
            desiredRotation > currentRotation + 1 * fraction)
        {
            trans.rotation = Quaternion.RotateTowards(trans.rotation, targetRotation, 1);
        }
    }

    public void GrowSnake()
    {
        GameObject body;
        if (isFirst)
        {
            body = Instantiate(BodyPrefab, transform.position, Quaternion.identity);
            body.tag = "First";
            isFirst = false;
        }
        else
        {
            body = Instantiate(BodyPrefab, NewBodyPoint, Quaternion.identity);
        }
        BodyParts.Add(body);
        snakeBodySize++;

        SnakeSize.text = snakeBodySize.ToString();
    }
}
