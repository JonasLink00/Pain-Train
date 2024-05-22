using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingTrigger : MonoBehaviour
{
    [SerializeField] GameObject SideWallL, SideWallR;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            SideWallL.SetActive(false);
            SideWallR.SetActive(false);
        }
    }
}
