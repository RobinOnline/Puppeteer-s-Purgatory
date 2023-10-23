using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private float Range;
    [SerializeField] private LayerMask pickUpLayer;
    [SerializeField] private LayerMask keyPadLayer;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CameraMovement camMov;

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
    }
}
