using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject SideWallL, SideWallR, FrontWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerInput>())
        {
           
            diaktivateWall(SideWallL);
            diaktivateWall(SideWallR);
            aktivateWall(FrontWall);
        }
    }

    private void diaktivateWall(GameObject Wall)
    {
        Wall.GetComponent<MeshRenderer>().enabled = false;
    }
    private void aktivateWall(GameObject Wall)
    {
        Wall.GetComponent<BoxCollider>().enabled = true;
    }
}
