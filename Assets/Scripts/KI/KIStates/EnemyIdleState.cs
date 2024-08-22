using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyController _controller) : base(_controller)
    {
        
    }

    public override void Enter()
    {
        //Debug.Log("IdleEnter");
    }

    public override void Exit()
    {
        controller.IdleTimer = 3f;
    }

    public override void Update()
    {
        controller.IdleTimer -= Time.deltaTime;
    }
}
