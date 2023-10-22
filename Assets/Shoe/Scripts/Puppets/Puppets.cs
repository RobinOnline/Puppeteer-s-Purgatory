using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Puppet", menuName = "Items/Puppet")]
public class Puppets : Item
{
    public GameObject prefab;
    public PuppetNumber puppetNumber;
}

public enum PuppetNumber { First, Second, Third}
