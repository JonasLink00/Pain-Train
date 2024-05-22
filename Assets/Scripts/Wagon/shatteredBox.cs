using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatteredBox : MonoBehaviour
{
    public GameObject box;
    public GameObject shattered;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            BreakTheThing();
        }
    }

    public void BreakTheThing()
    {
        Instantiate(shattered, this.gameObject.transform);
        box.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;

    }

} 
