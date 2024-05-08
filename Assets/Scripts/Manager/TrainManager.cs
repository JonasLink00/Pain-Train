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
        //Enemy gets the position on the player
        WorkingEnemy.agent.destination = player.transform.position;
        PassangerEnemy.agent.destination = player.transform.position;
    }




}
