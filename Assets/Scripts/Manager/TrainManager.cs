using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private WagonTrigger trigger;
    [SerializeField] private EnemyController WorkingEnemy;
    [SerializeField] private EnemyController PassangerEnemy;

    public void Playerspotted()
    {
        if (WorkingEnemy.gameObject.active == true)
        {
            WorkingEnemy.agent.destination = player.transform.position;
        }
        if (PassangerEnemy.gameObject.active == true)
        {
            PassangerEnemy.agent.destination = player.transform.position;
        }
        //Enemy gets the position on the player
    }




}
