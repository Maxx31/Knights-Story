using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float max_Health;

    float current_Healh;
    void Start()
    {
        current_Healh = max_Health;
    }


    public void Take_Damage(float damage)
    {
        
        current_Healh -= damage;
        Debug.Log("Current health:" + current_Healh);
        
        if(current_Healh <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy is dead");
    }
    
}
