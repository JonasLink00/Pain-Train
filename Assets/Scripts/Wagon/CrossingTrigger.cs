using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject SideWallL, SideWallR;
    public LayerMask player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && other.gameObject.GetComponent<PlayerMovement>())
        {
            SideWallL.SetActive(false);
            SideWallR.SetActive(false);
        }
    }
}
