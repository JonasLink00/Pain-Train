using UnityEngine;

[RequireComponent(typeof(Health), typeof(Strength))]
public class Player : MonoBehaviour
{
    public Strength strength;
    public Health health;

    

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

    
}
