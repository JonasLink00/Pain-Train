using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private WagonTrigger trigger;
    [SerializeField] private EnemyController PassangerEnemy;
    [SerializeField] private EnemyController WorkingEnemy;

    public void Playerspotted()
    {
        //Enemy gets the position on the player
        PassangerEnemy.agent.destination = player.transform.position;
        WorkingEnemy.agent.destination = player.transform.position;
    }




}
