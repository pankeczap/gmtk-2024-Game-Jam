using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "WFCSegment", menuName = "WFC/Segment")]
[System.Serializable]
public class WFCSegment : ScriptableObject
{
    public string Name;
    public GameObject Prefab;
    public WFC_ValidConnection Top;
    public WFC_ValidConnection Bottom;
    public WFC_ValidConnection Left;
    public WFC_ValidConnection Right;
}

[System.Serializable]
public class WFC_ValidConnection
{
    public List<WFCSegment> ValidSegments = new List<WFCSegment>();
}
