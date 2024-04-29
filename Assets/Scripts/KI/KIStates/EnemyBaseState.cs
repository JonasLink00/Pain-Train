using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState 
{
   protected EnemyController controller;
   
   public EnemyBaseState(EnemyController _controller)
   {
       controller = _controller;
   }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
