using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Tools[] tools;


    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
    }

    public void AddItem(Tools newItem)
    {
        int toolIndex = (int)newItem.toolType;
        if (tools[toolIndex] != null)
        {
            Debug.Log("Slot is occupied)");
        }
        else
        {
            tools[toolIndex] = newItem;
        }
        
    }

    private void InitVariables()
    {
        tools = new Tools[3];
    }
}
