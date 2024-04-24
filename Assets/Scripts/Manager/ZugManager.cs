using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZugManager : MonoBehaviour
{
    public static ZugManager zugmanager;
    public PlayerMovement player;
    public WagonTrigger trigger;
    public EnemyAttackState enemyAttackState;

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
        enemyAttackState.agent.SetDestination(player.transform.position);
    }




}
