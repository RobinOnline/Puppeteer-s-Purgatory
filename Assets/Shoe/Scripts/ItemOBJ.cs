using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOBJ : MonoBehaviour
{
    public Item item;

    public void OnPress()
    {
        Inventory.instance.OnHands = this.item as Tools;
        EquipmentManager.instance.EquipTool(Inventory.instance.OnHands.prefab);
    }
}
