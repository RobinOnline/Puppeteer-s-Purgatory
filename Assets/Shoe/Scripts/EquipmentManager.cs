using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Header("Holders & Layers")]
    public Transform ToolHolderL = null;
    public Transform PuppetHolder = null;
    [SerializeField] private LayerMask puppetLayer;
    [SerializeField] private LayerMask holderLayer;
    [Space]
    [Header("To Assign")]
    [SerializeField] private Camera cam;
    [SerializeField] private Animator anim;
    [SerializeField] private int Index;
    public GameObject _Puppet;
    public GameObject EquippedPuppet;
    [SerializeField] private bool equipped;
    [SerializeField] private float Range;

    public static EquipmentManager instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    void Start()
    {
        GetRefs();
    }
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.E))
        {
            EquipPuppet();
            
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            DropPuppet();
        }
        
    }

    public void EquipPuppet()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Range, puppetLayer) && !equipped)
        {
            Inventory.instance.OnHands = hit.transform.gameObject.GetComponent<ItemOBJ>().item as Puppets;
            _Puppet = Inventory.instance.OnHands.prefab;
            anim.SetInteger("toolType", 2);
            Debug.Log("AAAAA");
            
            Destroy(hit.transform.gameObject);
            EquippedPuppet = Instantiate(_Puppet, ToolHolderL);
            PuppetHolder.gameObject.GetComponent<BoxCollider>().enabled = true;
            equipped = true;
        }
        
    }

    public void DropPuppet()
    {
        Ray ray2 = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit2;

        if (Physics.Raycast(ray2, out hit2, Range, holderLayer) && equipped)
        {
            if (Inventory.instance.OnHands != null)
            {
                PuppetHolder = hit2.transform;               
                Destroy(EquippedPuppet);
                EquippedPuppet = Instantiate(_Puppet, PuppetHolder);
                Inventory.instance.OnHands = null;
                RotationAndScale();
                equipped = false;
            }
        }
    }

    private void GetRefs()
    {
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main;
        equipped = false;
    }

    private void RotationAndScale()
    {
        Quaternion newRotation = Quaternion.Euler(-4, 191, -1);
        EquippedPuppet.transform.rotation = newRotation;
        Vector3 newScale = new Vector3(.1f, .1f, .1f);
        EquippedPuppet.transform.localScale = newScale;
        PuppetHolder.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
