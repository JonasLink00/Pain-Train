using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    private NavMeshAgent agent;
    public PlayerMovement Player;

    public EnemyAttackState(EnemyController _controller, NavMeshAgent _agent) : base(_controller)
    {
        agent = _agent;
    }

    public override void Enter()
    {
        agent.SetDestination(Player.transform.position);
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }
}
