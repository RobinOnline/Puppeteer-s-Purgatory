using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private float Range;
    [SerializeField] private LayerMask pickUpLayer;

    private Camera cam;

    private Inventory inv;
    private void Start()
    {
        GetRef();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
    }

    void GetRef()
    {
        cam = Camera.main;
        inv = GetComponent<Inventory>();
    }
}
