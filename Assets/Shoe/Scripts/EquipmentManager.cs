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
    [SerializeField] public LayerMask RestorationLayer;
    [Space]
    [Header("To Assign")]
    [SerializeField] private Camera cam;
    [SerializeField] private Animator anim;
    [SerializeField] private int Index;
    public GameObject _Puppet;
    Puppets newPuppet;
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

        Ray ray2 = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit2;

        if (Physics.Raycast(ray2, out hit2, Range, holderLayer) && equipped)
        {
            PlayerPickUp.instance.Text.text = "Press F to put the puppet in place";
        }
        else
        {
            PlayerPickUp.instance.Text.text = " ";
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
            newPuppet = hit.transform.GetComponent<ItemOBJ>().item as Puppets;
            Destroy(hit.transform.gameObject);
            EquippedPuppet = Instantiate(_Puppet, ToolHolderL);
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
                anim.SetInteger("toolType", 0);
                Destroy(EquippedPuppet);
                EquippedPuppet = Instantiate(_Puppet, PuppetHolder);
                Restoration.instance.enabled = true;
                Restoration.instance.Papeto = EquippedPuppet;
                Restoration.instance.currentPuppet = EquippedPuppet.GetComponent<ItemOBJ>().item as Puppets;
                Restoration.instance.On = true;
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
        Vector3 newScale = new Vector3(.3f, .3f, .3f);
        EquippedPuppet.transform.localScale = newScale;
        if ((int)newPuppet.puppetNumber == 1)
        {
            Vector3 newPos = new Vector3(0.148f, -0.274f, -0.051f);
            EquippedPuppet.transform.localPosition = newPos;
        }
        if ((int)newPuppet.puppetNumber == 2)
        {
            Vector3 newPos2 = new Vector3(.156f, -0.241f, -0.097f);
            EquippedPuppet.transform.localPosition = newPos2;
        }
    }
}
