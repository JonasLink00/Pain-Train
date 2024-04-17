using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonTrigger : MonoBehaviour
{
    public PlayerMovement Player;
    private bool GetAttackt;
    private void OnTriggerExit(Collider other)
    {
        //Deaktiviert Event bei kontakt mit Player
        if (other.GetComponent<Rigidbody2D>() != null)
        {
            GetAttackt = true;
        }
    }
}
