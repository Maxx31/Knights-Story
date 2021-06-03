using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    public float max_Health;
    public float Armor;
    public Animator anim;
    public bool Dead = false;

    private float current_Healh;

    private Rigidbody2D rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        current_Healh = max_Health;
    }
    public void Take_Damage(float damage)
    {
        if (Dead == true) return;
        int armor_Reduce = 3;
       if( Skills_Manager.use.Is_Enable_Passive_skills_Warrior[5] == true)
        {
            for(int i = 1; i < Skills_Manager.use.Passive_skills_Warrior[5]; i++)
            {
                armor_Reduce = ((armor_Reduce) * 2) + 1;

            }

        }
        damage -= ( (Armor - armor_Reduce) * damage) / 100; //Armor 
 
        current_Healh -= damage;
      
        
        if(current_Healh <= 0)
        {
            anim.SetTrigger("Death");
            Dead = true;
            //Stop any movement
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            if (GetComponentInChildren<Melee_Enemy_Combat>() != null)
            {
                GetComponentInChildren<Melee_Enemy_Combat>().Is_Dead = true;
            }
            if (GetComponentInChildren<Magic_enemy_Combat>() != null)
            {
                GetComponentInChildren<Magic_enemy_Combat>().Is_Dead = true;
            }
            transform.GetComponent<Patrol>().enabled = false;

            Invoke("Die", 1f);
        }
    }

    void Die()
    {
        Debug.Log("Enemy is dead");
        Destroy(gameObject);
    }
    
   public void Jump()
    {

        rb.AddForce(Vector2.up * 600f);
    }

}
