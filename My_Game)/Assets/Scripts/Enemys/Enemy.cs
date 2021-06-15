using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    public float Max_Health;
    public float Armor;
    public Animator anim;
    public bool Dead = false;

    [SerializeField]
    private Health_Bar healthbar;
    private float current_Healh;
    private Rigidbody2D rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        current_Healh = Max_Health;
        healthbar.SetHealth(current_Healh, Max_Health);
    }
    public void Take_Damage(float damage)
    {
        if (Dead == true) return;
        int armor_Reduce = 0;
        if ( Skills_Manager.use.Is_Enable_Passive_skills_Warrior[5] == true)
        {
           armor_Reduce = 18;
        }
        damage -= ( (Armor - armor_Reduce) * damage) / 100; //Armor 
 
        current_Healh -= damage;



        if (current_Healh <= 0)
        {

            healthbar.SetHealth(Max_Health, Max_Health);

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
            return;
        }
        healthbar.SetHealth(current_Healh, Max_Health);
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
