using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private WagonTrigger trigger;
    [SerializeField] private EnemyController WorkingEnemy;
    [SerializeField] private EnemyController PassangerEnemy;
    [SerializeField] private AudioSource TrainSound;

    public void Playerspotted()
    {
        //Enemy gets the position on the player

        if (WorkingEnemy.gameObject.active == true)
        {
            WorkingEnemy.agent.destination = player.transform.position;
        }
        if (PassangerEnemy.gameObject.active == true)
        {
            PassangerEnemy.agent.destination = player.transform.position;
        }
    }


    public void IncreaseTrainSound()
    {
        TrainSound.volume = 1f;
    }

    public void NormalTrainSound()
    {
        TrainSound.volume = 0.15f;
    }

}
