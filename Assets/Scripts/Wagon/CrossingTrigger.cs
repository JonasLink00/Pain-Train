using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject SideWallL, SideWallR;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && other.gameObject.GetComponent<PlayerMovement>())
        {
           
            diaktivateWall(SideWallL);
            diaktivateWall(SideWallR);

        }
    }

    private void diaktivateWall(GameObject Wall)
    {
        Wall.GetComponent<MeshRenderer>().enabled = false;
    }
}
