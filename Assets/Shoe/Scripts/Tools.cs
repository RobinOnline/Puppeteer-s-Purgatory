using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Items/Tool")]
public class Tools : Item
{
    [SerializeField] private GameObject prefab;
    public ToolType toolType;

    
}

public enum ToolType { Nails, Wood, Seam_Ripper, Scissors, Nail_Puller, Small_Pry_Bar, Small_Hammer, Soap, 
    Bleach, Watercolour, Varnish, Sewing_Kit, Flashlight }
