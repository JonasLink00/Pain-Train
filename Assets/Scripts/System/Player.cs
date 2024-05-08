using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IAttackable
{
    public Strength strength;
    public Health health;

    

    private void OnTriggerEnter(Collider collision)
    {
        var attackable = collision.GetComponent<IAttackable>();
        if (attackable == null)
            return;

        strength.Attack(attackable);
    }
    public void GetAttacked(int damange)
    {
        health.DecreaseHealth(damange);
    }

    
}
