using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAttackable, IAttacker, ICanDie
{
    public Health Health;
    public Strength strength;

    private void OnTriggerEnter(Collider collision)
    {
        var attackable = collision.GetComponent <IAttackable>();
        if (attackable == null)
            return;

        Attack(attackable);
    }
    public void GetAttacked(int damange)
    {
        Health.DecreaseHealth(damange);
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
