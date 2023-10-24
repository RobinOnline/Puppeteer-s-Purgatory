using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private float Range;
    [SerializeField] private LayerMask pickUpLayer;
    [SerializeField] private LayerMask keyPadLayer;
    [SerializeField] private LayerMask ladderLayer;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CameraMovement camMov;
    [SerializeField] private GameObject PlayerOBJ;
    public GameObject Canvas;
    public TextMeshProUGUI Text;

    [Space]
    public GameObject StartLadder;
    public GameObject EndLadder;
    [Space]
    public GameObject CodeLock;
    public GameObject InGame;
    public Animation DoorOpen;

    private Camera cam;

    private Inventory inv;

    public static PlayerPickUp instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    private void Start()
    {
        GetRef();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastTool();
            RaycastKeyPad();
            RaycastRestoration();
            RaycastLadder1();
            RaycastLadder2();
        }
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Range, pickUpLayer))
        {
            Text.text = hit.transform.name;
        }
        else
        {
            Text.text = " ";
        }
    }

    private void RaycastTool()
    {
        //Raycast
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Range, pickUpLayer))
        {
            Debug.Log(hit.transform.name);
            Tools newTool = hit.transform.GetComponent<ItemOBJ>().item as Tools;
            inv.AddItem(newTool);
            Destroy(hit.transform.gameObject);
        }
    }

    private void RaycastKeyPad()
    {
        //Raycast
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Range, keyPadLayer))
        {
            Debug.Log(hit.transform.name);
            playerMovement.enabled = false;
            camMov.enabled = false;
            CodeLock.SetActive(true);
            InGame.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void RaycastRestoration()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Restoration.instance.enabled == true)
        {
            if (Physics.Raycast(ray, out hit, Range, EquipmentManager.instance.RestorationLayer))
            {
                Restoration.instance.Repair();
            }
        }
        
    }

    private void RaycastLadder1()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Range, ladderLayer))
        {
            if (StartLadder.GetComponent<BoxCollider>().enabled == true)
            {
                Canvas.SetActive(false);
                PlayerOBJ.SetActive(false);
                PlayerOBJ.transform.position = EndLadder.transform.position;                
                PlayerOBJ.SetActive(true);
            }
            
        }
    }

    private void RaycastLadder2()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Range, ladderLayer))
        {
            Canvas.SetActive(false);
            PlayerOBJ.SetActive(false);
                PlayerOBJ.transform.position = StartLadder.transform.position;
                PlayerOBJ.SetActive(true);

        }
    }

    public void SetTrue()
    {
        DoorOpen.Play();
        playerMovement.enabled = true;
        camMov.enabled = true;
        CodeLock.SetActive(false);
        InGame.SetActive(true);
    }

    void GetRef()
    {
        cam = Camera.main;
        inv = GetComponent<Inventory>();
        playerMovement = GetComponent<PlayerMovement>();
        camMov = GetComponentInChildren<CameraMovement>();
        PlayerOBJ = this.transform.gameObject;
    }
}
