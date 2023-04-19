using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SpecialSO", menuName = "SpecialSO", order = 2)]
public class SpecialSO : ScriptableObject
{
    public enum SpecialType {
        coins,
        milk,
        wool,
        ham,
        egg,
        money
    }

    public string specialName;
    public string description;
    public float cost;
    public SpecialType specialType;
    public Sprite image;
    [Header("if Placeable")]
    public bool placeable;
    public GameObject building;
    public Transform[] path;
    public Vector3 placedPosition;
    public Vector3 placedRotation;


}
