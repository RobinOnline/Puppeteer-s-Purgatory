using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTrigger : MonoBehaviour
{
    public GameObject BigPuppet;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(BigPuppet);
            this.gameObject.SetActive(false);
        }
    }
}
