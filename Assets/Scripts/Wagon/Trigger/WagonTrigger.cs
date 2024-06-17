using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonTrigger : MonoBehaviour
{
    public bool GetAttacked = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerInput>())
        {
            GetAttacked = true;
        }
    }
}
