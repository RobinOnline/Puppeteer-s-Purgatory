using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Restoration : MonoBehaviour
{
    [SerializeField] public Puppets currentPuppet;
    [SerializeField] public GameObject Papeto;
    public GameObject Gate;
    public GameObject PuppetMonster;
    public static Restoration instance;
    public int maxRestoration;
    public int currentRestoration;

    public TextMeshProUGUI Text;

    public GameObject FirstTools;
    public GameObject SecondTools;
    public GameObject ThirdTools;

    public int currentPuppetNumber;
    public GameObject[] daPuppets;
    public bool On;

    public GameObject FirstTrigger, SecondTrigger, ThirdTrigger;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    private void Start()
    {
        CheckRestoration();
        FirstTools.SetActive(true);
    }
    private void Update()
    {
        
        if (currentRestoration <= maxRestoration)
        {
            EquipmentManager.instance.enabled = false;
        }
        else
        {
            currentPuppetNumber++;
            currentRestoration = 0;
            EquipmentManager.instance.enabled = true;
            Destroy(Papeto);
            if (currentPuppetNumber != 3)
            {
                this.enabled = false;
            }            
            currentRestoration = 0;
        }

        if (On)
        {
            switch (currentPuppetNumber)
            {
                case 0:
                    FirstTools.SetActive(true);
                    On = false;
                    if (daPuppets[1])
                    {
                        daPuppets[1].GetComponent<BoxCollider>().enabled = true;
                    }
                    currentPuppetNumber++;
                    
                    break;
                case 1:
                    SecondTools.SetActive(true);
                    FirstTrigger.SetActive(true);
                    maxRestoration = currentPuppet.RestorationProcess.Length;
                    if (daPuppets[2])
                    {
                        daPuppets[2].GetComponent<BoxCollider>().enabled = true;
                    }
                    On = false;
                    break;
                case 2:
                    ThirdTools.SetActive(true);
                    SecondTrigger.SetActive(true);
                    maxRestoration = currentPuppet.RestorationProcess.Length;
                    On = true;
                    break;

                case 3:
                    PuppetMonster.SetActive(true);
                    Gate.SetActive(false);
                    ThirdTrigger.SetActive(true);
                    break;
            }
           
        }

    }

    public void Repair()
    {
        switch (currentRestoration)
        {
            case 0:
                if (Inventory.instance.tools[0] && Inventory.instance.tools[6])
                {
                    currentRestoration++;
                    SoundManager.Instance.PlayRandomEffect("Repair");
                    Inventory.instance.tools[0] = null;
                }
                else
                {
                    if (!Inventory.instance.tools[6])
                    {
                        Text.text = "I need nails and a hammer";
                    }
                    else
                    {
                        Text.text = "I need more nails";
                    }
                    StartCoroutine(clearText());
                }
                break;

            case 1:
                if (Inventory.instance.tools[2])
                {
                    currentRestoration++;
                    SoundManager.Instance.PlayRandomEffect("Repair");
                    Inventory.instance.tools[2] = null;
                }
                else
                {
                    Text.text = "I need a seam ripper";
                    StartCoroutine(clearText());
                }
                break;

            case 2:
                if (Inventory.instance.tools[5])
                {
                    currentRestoration++;
                    SoundManager.Instance.PlayRandomEffect("Repair");
                    Inventory.instance.tools[5] = null;
                }
                else
                {
                    Text.text = "I need a pry bar";
                    StartCoroutine(clearText());
                }
                break;

            case 3:
                if (Inventory.instance.tools[4])
                {
                    currentRestoration++;
                    SoundManager.Instance.PlayRandomEffect("Repair");
                }
                else
                {
                    Text.text = "I need a nail puller";
                    StartCoroutine(clearText());
                }
                break;
        }
        
    }

    public void CheckRestoration()
    {
        currentPuppetNumber = -1;
        currentPuppet = EquipmentManager.instance.EquippedPuppet.GetComponent<ItemOBJ>().item as Puppets;
        maxRestoration = currentPuppet.RestorationProcess.Length;
    }

    IEnumerator clearText()
    {
        yield return new WaitForSeconds(1f);
        Text.text = " ";
    }
}
