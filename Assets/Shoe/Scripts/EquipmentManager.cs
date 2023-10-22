using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Transform ToolHolderL = null;
    [SerializeField] private Animator anim;
    [SerializeField] private int Index;
    public GameObject _tool;
    public GameObject EquippedTool;
    [SerializeField] private bool equipped;

    public static EquipmentManager instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetRefs();
    }

    // Update is called once per frame
    void Update()
    {

        if (Inventory.instance.OnHands.grabType == GrabType.Grab)
        {            
            anim.SetInteger("toolType", (int)GrabType.Grab);
        }
    }

    public void EquipTool(GameObject tool)
    {
        _tool = tool;
        if (EquippedTool != null)
        {
            Destroy(EquippedTool);
        }

        EquippedTool = Instantiate(_tool, ToolHolderL);
    }

    private void GetRefs()
    {
        anim = GetComponentInChildren<Animator>();
        equipped = true;
    }
}
