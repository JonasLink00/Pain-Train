using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    //public UnityEvent OnZeroHealth;

    public int currendHealth;

    public abstract void IncreaseHealth(int amount);
    
    
    public virtual void DecreaseHealth(int amount)
    {
        currendHealth = currendHealth - amount;

        if (currendHealth <= 0) 
        {
            Debug.Log("You Died");
            this.gameObject.SetActive(false);
            //OnZeroHealth.Invoke();
        }
    }

   
}
