using UnityEngine;

public class Health : HealthSystem
{
    public int MaxHealth;
    public override void IncreaseHealth(int amount)
    {
        currendHealth = Mathf.Clamp(currendHealth + amount, 1, MaxHealth);
    }

    public override void DecreaseHealth(int amount)
    {
        base.DecreaseHealth(amount);
        Debug.Log($"HealthChange:{currendHealth}");
    }
}
