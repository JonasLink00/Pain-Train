using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonTrigger : MonoBehaviour
{
    public bool GetAttackt = false;
    private void OnTriggerExit(Collider other)
    {
        //Deaktiviert Event bei kontakt mit Player
        if (other.GetComponent<Rigidbody>() != null)
        {
            GetAttackt = true;
        }
    }
}
