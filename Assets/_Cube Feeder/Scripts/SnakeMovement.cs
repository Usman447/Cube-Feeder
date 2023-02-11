using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class SnakeMovement : MonoBehaviour
{
    [Header("Snake Setup")]
    [SerializeField] int GapBetweenBodies = 12;
    [SerializeField] float MoveSpeed = 7f;
    [SerializeField] int MaxPositionCount = 30000;
    [SerializeField] GameObject BodyPrefab;
    [SerializeField] GameObject HeadPrefab;
    public float DestoryTimeDifference = 0.2f;


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
    public float Depth = 1;


    [Header("UI Stuff")]
    [SerializeField] TextMeshProUGUI SnakeSize;


    // Private Variables
    Vector3 moveDirection = Vector3.zero;
    Vector3 dir = Vector3.zero;
    List<GameObject> BodyParts;
    List<Vector3> PositionHistory;
    public SpwanFood spawnFood { get; private set; }

    public int snakeBodySize { get; private set; } = 0;
    bool isFirst = true;
    bool isDestory = false;

    private void Start()
    {
        spawnFood = FindObjectOfType<SpwanFood>();
        BodyParts = new List<GameObject>();
        PositionHistory = new List<Vector3>();

        GameObject body = Instantiate(HeadPrefab);
        BodyParts.Add(body);
    }

    private void Update()
    {
        if (!isDestory)
        {
            dir = TouchMove.MoveDir;

            if (dir.magnitude >= 0.999999f)
            {
                moveDirection = dir;
            }

            ContinuousMovement();
            HandleBodyPartsMovement();
        }
        else
        {
            if (destory1 && destory2)
                SceneManager.LoadScene(0);

        }
    }


    void ContinuousMovement()
    {
        transform.Translate(moveDirection * MoveSpeed * Time.deltaTime, Space.Self);
        PositionHistory.Insert(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, groundLayer))
        {
            transform.rotation = GetTargetRotation(hit.normal);

            Vector3 targetPosition = hit.point;
            targetPosition += hit.normal * Depth;
            transform.position = targetPosition;
        }
        else
            Debug.Log("Not Collide");
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

    public int newGapVal = 0;

    void HandleBodyPartsMovement()
    {
        newGapVal = GetGaps();
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionHistory[Mathf.Min(index * newGapVal, PositionHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * MoveSpeed * Time.deltaTime;
            body.transform.LookAt(point, -transform.forward);
            index++;
        }

        if(PositionHistory.Count > MaxPositionCount)
            PositionHistory.RemoveAt(MaxPositionCount);
    }

    public void AddSnakeBody()
    {
        GameObject body;
        if (isFirst)
        {
            body = Instantiate(BodyPrefab, transform.position, Quaternion.identity);
            body.tag = "First";
            body.name = "Head";
            isFirst = false;
        }
        else
        {
            body = Instantiate(BodyPrefab, BodyParts[BodyParts.Count - 1].transform.position, Quaternion.identity);
            body.tag = "Body";
            body.name = snakeBodySize.ToString();
        }

        BodyParts.Add(body);
        snakeBodySize++;
        SnakeSize.text = snakeBodySize.ToString();
    }

    public void SubstractSnakeBody()
    {
        if (BodyParts.Count > 1)
        {
            GameObject destroyBody = BodyParts[BodyParts.Count - 1];
            BodyParts.Remove(destroyBody);
            Destroy(destroyBody, 0.5f);
            snakeBodySize--;
            SnakeSize.text = snakeBodySize.ToString();
            if (snakeBodySize <= 0)
                isFirst = true;
        }
    }
    public void SnakeEatFood()
    {
        AddSnakeBody();
        if (spawnFood != null)
        {
            spawnFood.FoodEaten();
            spawnFood.SpawnNewFood();
        }
    }

    public void SnakeSizeDcrease()
    {
        SubstractSnakeBody();
        if (spawnFood != null)
        {
            spawnFood.PowerFoodEaten();
            spawnFood.SpawnNewFood();
        }
    }

    bool destory1 = false, destory2 = false;

    public void DestoryPlayer(int bodyCollisionIndex)
    {
        isDestory = true;
        moveDirection = Vector3.zero;

        StartCoroutine(ForwardDestruction(bodyCollisionIndex, BodyParts.Count));
        StartCoroutine(BackwardDestruction(bodyCollisionIndex - 1, 1));
    }

    IEnumerator ForwardDestruction(int start, int end)
    {
        for (int i = start; i < end; ++i)
        {
            GameObject destroyBody = BodyParts[i];
            //BodyParts.Remove(destroyBody);
            Destroy(destroyBody);
            yield return null;
        }
        destory1 = true;
    }

    IEnumerator BackwardDestruction(int start, int end)
    {
        for (int i = start; i >= end; --i)
        {
            GameObject destroyBody = BodyParts[i];
            //BodyParts.Remove(destroyBody);
            Destroy(destroyBody);
            yield return null;
        }
        destory2 = true;
    }

    int GetGaps()
    {
        return (int)(Application.targetFrameRate * GapBetweenBodies) / 60;
    }
}
