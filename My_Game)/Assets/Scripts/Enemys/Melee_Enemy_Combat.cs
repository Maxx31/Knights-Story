using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Enemy_Combat : MonoBehaviour
{

    public Transform attack_Point;
    public float attack_range;
    public float Attack_rate ;
    public float Damage = 25f;
    public Animator anim;
    public LayerMask M_Hero;
    public bool Is_Dead = false;

    private float next_Attact_Time = 0f;
    private bool in_range = false;
    private void Update()
    {
        if (Time.time >= next_Attact_Time && in_range == true )
        {
            anim.SetTrigger("Attack");
            Invoke("Attack", 0.7f);
            next_Attact_Time = Time.time + 1f / Attack_rate;
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            in_range = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            in_range = false;
        }
    }

    void Attack()
    {
        if (Is_Dead) return;
        if (in_range)
        {
            Collider2D[] Hit_Enemies = Physics2D.OverlapCircleAll(attack_Point.position, attack_range, M_Hero);

            foreach (Collider2D Hero in Hit_Enemies)
            {
                Hero.GetComponent<Main_Hero>().Take_Damage(Damage);
            }
        }
    }
}
