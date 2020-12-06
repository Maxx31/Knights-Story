using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float max_Health;
    public float Armor;
    float current_Healh;
     public Animator anim;
    private bool Facing_Right;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        current_Healh = max_Health;

    }

    private void Update()
    {
        if (rb.velocity.y == 0)
        {

            anim.SetBool("Is_Jumping", false);
            anim.SetBool("Is_Falling", false);
        }
        else if (rb.velocity.y > 0)
        {
            anim.SetBool("Is_Jumping", true);
        }
        else
        {
            anim.SetBool("Is_Jumping", false);
            anim.SetBool("Is_Falling", true);
        }
    }

    public void Take_Damage(float damage)
    {
     
        int armor_Reduce = 3;
       if( Skills_Manager.use.Is_Enable_Passive_skills_Warrior[5] == true)
        {
            for(int i = 1; i < Skills_Manager.use.Passive_skills_Warrior[5]; i++)
            {
                armor_Reduce = ((armor_Reduce) * 2) + 1;

            }

        }
        damage -= ( (Armor - armor_Reduce) * damage) / 100; //Влияние Армора на получаемый урон
 
        current_Healh -= damage;
       // Debug.Log("Current health:" + current_Healh);
        
        if(current_Healh <= 0)
        {
            Die();
        }
    }

    
        void Die()
    {
        anim.SetTrigger("Death");
        Debug.Log("Enemy is dead");
    }
    


}
