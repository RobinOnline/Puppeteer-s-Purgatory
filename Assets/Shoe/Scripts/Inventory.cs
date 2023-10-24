using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] public Tools[] tools;
    [SerializeField] public Puppets OnHands;

    [SerializeField] private GameObject[] invImg;
    [SerializeField] private PlayerMovement playerScript;
    [SerializeField] private CameraMovement camScript;

    public GameObject InventoryUI;
    public GameObject flashlight;

    public static Inventory instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }


    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        if (tools[12] != null)
        {
            flashlight.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerScript.enabled = !playerScript.isActiveAndEnabled;
            camScript.enabled = !camScript.isActiveAndEnabled;
            InventoryUI.SetActive(!InventoryUI.activeSelf);
            DisplayInventory();
        }
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

    public void ChooseTool(ToolType Type)
    {

    }

    public void DisplayInventory()
    {
        for (int i = 0; i < tools.Length; i++)
        {
            if (tools[i] != null)
            {
                invImg[i].transform.gameObject.SetActive(true);
            }
        }
    }

    private void InitVariables()
    {
        playerScript = GetComponent<PlayerMovement>();
        camScript = GetComponentInChildren<CameraMovement>();
        tools = new Tools[13];
        if (InventoryUI != null)
        {
            InventoryUI.SetActive(false);
        }
    }
}
