using System.Collections.Generic;
using UnityEngine;

public delegate bool StateMachineDelegate();
public abstract class BaseController : MonoBehaviour
{
    protected Dictionary<EnemyBaseState, Dictionary<StateMachineDelegate, EnemyBaseState>> stateDictionary;

    protected virtual void Start()
    {
        InitFSM();

    }

    protected virtual void Update()
    {
        UpdateFSM();
    }

    protected virtual void InitFSM()
    {

    }

    protected virtual void UpdateFSM()
    {

    }
}
