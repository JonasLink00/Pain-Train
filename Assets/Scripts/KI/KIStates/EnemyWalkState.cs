using UnityEngine;
using UnityEngine.AI;

public class EnemyWalkState : EnemyBaseState
{
    private NavMeshAgent agent;

    public EnemyWalkState(EnemyController _controller, NavMeshAgent _agent) : base(_controller)
    {
        agent = _agent;
    }

    public override void Enter()
    {
        //Debug.Log("WalkStateEnter");
        agent.SetDestination(agent.transform.position + Random.insideUnitSphere * 5);
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {

    }
}
