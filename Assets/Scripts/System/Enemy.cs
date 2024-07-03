using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(Strength))]
public class Enemy : MonoBehaviour, IAttackable, ICanDie
{
    public Health health;
    public Strength strength;

    private void OnTriggerEnter(Collider collision)
    {
        
        var attackable = collision.GetComponent<IAttackable>();
        if (attackable == null)
            return;

        Debug.Log($"{gameObject.name} attacked {collision.gameObject.name}");

        strength.Attack(attackable);

    }

   
    public void GetAttacked(int damange)
    {
        health.DecreaseHealth(damange);
    }
    public void Attack(IAttackable attackable)
    {
        strength.Attack(attackable);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

   
}
