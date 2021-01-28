using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Enemy_Combat : MonoBehaviour
{

    public Transform attack_Point;
    public float attack_range;
    public float Attack_rate ;
    public float Damage = 25f;
    float Next_Attact_Time = 0f;
 
    bool In_range = false;
   public Animator anim;
    public LayerMask M_Hero;
    private void Update()
    {
        if (Time.time >= Next_Attact_Time && In_range == true && transform.parent.GetComponent<Enemy>().Dead == false)
        {
           
            anim.SetTrigger("Attack");
            Invoke("Attack", 0.7f);
          //  Attack();
                Next_Attact_Time = Time.time + 1f / Attack_rate;
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            In_range = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            In_range = false;
        }
    }


    void Attack()
    {
 
        if (In_range)
        {
            Collider2D[] Hit_Enemies = Physics2D.OverlapCircleAll(attack_Point.position, attack_range, M_Hero);

            foreach (Collider2D Hero in Hit_Enemies)
            {
                Hero.GetComponent<Main_Hero>().Take_Damage(Damage);
            }
        }
    }
}
