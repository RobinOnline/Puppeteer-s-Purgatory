using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Items/Tool")]
public class Tools : Item
{
    [SerializeField] private GameObject prefab;
    public ToolType toolType;
}

public enum ToolType { A, B, C, Voice_Recorder }
