using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    public NavMeshAgent agent;
    public TrainManager trainManager;

    public EnemyAttackState(EnemyController _controller, NavMeshAgent _agent) : base(_controller)
    {
        agent = _agent;
    }

    public override void Enter()
    {
        //agent.SetDestination(Player.transform.position);
        if (trainManager.trigger.GetAttacked)
        {
            trainManager.Enemyspotted();
        }
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }
}
