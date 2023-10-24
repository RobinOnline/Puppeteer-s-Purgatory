using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetJumpscare : MonoBehaviour
{

    public GameObject PuppetsJumpscare;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PuppetsJumpscare.SetActive(true);
            StartCoroutine(TurnOff());
        }
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(.3f);
        PuppetsJumpscare.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
