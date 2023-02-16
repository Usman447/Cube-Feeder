using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Character Data", order = 1)]
public class CharacterData : ScriptableObject
{
    public GameObject HeadPrefab;
    public GameObject BodyPrefab;
    public Vector3 SpawnPosition;
    public float BodyDepth;
    public int Gap;
}
