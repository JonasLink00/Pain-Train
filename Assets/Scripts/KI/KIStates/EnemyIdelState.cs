using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyIdelState : EnemyBaseState
{
    public EnemyIdelState(EnemyController _controller) : base(_controller)
    {

    }

    public override void Enter()
    {

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
