using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    public static TrainManager zugmanager;
    public PlayerMovement player;
    public WagonTrigger trigger;
    public EnemyController enemyController;

    private void Awake()
    {
        if(zugmanager ==null)
        {
            zugmanager = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void Enemyspotted()
    {
        //enemyAttackState.agent.SetDestination(player.transform.position);
        enemyController.agent.destination = player.transform.position;
    }




}
