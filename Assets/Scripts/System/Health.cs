using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : HealthSystem
{
    public int MaxHealth;
    public override void IncreaseHealth(int amount)
    {
        currendHealth = Mathf.Clamp(currendHealth + amount, 0, MaxHealth);
    }

    public override void DecreaseHealth(int amount)
    {
        base.DecreaseHealth(amount);
        Debug.Log($"HealthChange:{currendHealth}");
    }
}
