using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : MonoBehaviour
{
    public int strength;

    public void Attack(IAttackable attackable)
    {
        attackable.GetAttacked(strength);
    }
}
