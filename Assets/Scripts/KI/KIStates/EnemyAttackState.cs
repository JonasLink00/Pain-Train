using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    public NavMeshAgent agent;
    public ZugManager zugManager;

    public EnemyAttackState(EnemyController _controller, NavMeshAgent _agent) : base(_controller)
    {
        agent = _agent;
    }

    public override void Enter()
    {
        //agent.SetDestination(Player.transform.position);
        if (zugManager.trigger.GetAttackt)
        {
            zugManager.Enemyspotted();
        }
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }
}
