using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Puppet", menuName = "Items/Puppet")]
public class Puppets : Item
{
    public GameObject prefab;
    public PuppetNumber puppetNumber;
    public PuppetProcess[] RestorationProcess;
}

public enum PuppetNumber { First, Second, Third}

public enum PuppetProcess { Separate_Clothing, Arrange_Parts, Repair, Repaint, Remove_Nails}
