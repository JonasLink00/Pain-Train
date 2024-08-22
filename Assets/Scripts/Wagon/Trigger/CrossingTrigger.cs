using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject SideWallL, SideWallR, FrontWall;

    [SerializeField] private TrainManager TrainManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerInput>())
        {
           //gives a better view on the wagon
            deactivateWall(SideWallL);
            deactivateWall(SideWallR);
            //prevents backtracking 
            activateWall(FrontWall);
            TrainManager.IncreaseTrainSound();
        }
    }

    private void deactivateWall(GameObject Wall)
    {
        Wall.GetComponent<MeshRenderer>().enabled = false;
    }
    private void activateWall(GameObject Wall)
    {
        Wall.GetComponent<BoxCollider>().enabled = true;
    }
}
