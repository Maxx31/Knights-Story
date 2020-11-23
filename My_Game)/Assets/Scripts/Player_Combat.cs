using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player_Combat : MonoBehaviour
{

    

   


    public Animator anim;

    public Transform attack_Point;
    public float attack_range ;
    public float Attack_rate = 2f;
    public LayerMask enemy_Layer;
    private float Damage = 25f;
    float Next_Attact_Time = 0f;
    
   
    private void Start()
    {
       
        if(  Skills_Manager.use.Passive_skills_Warrior[0]> 0 && Skills_Manager.use.Is_Enable_Passive_skills_Warrior[0] == true )
        {
            // Дописать разные уровни - + разное кол-во дамаги 
            Damage += 2f + 3 * Skills_Manager.use.Passive_skills_Warrior[0];

        }

       
    }


    void Update()
    {
        if (Time.time >= Next_Attact_Time)
        {
            if (CrossPlatformInputManager.GetButtonDown("attack"))
            {
                Attack();
                Next_Attact_Time = Time.time + 1f / Attack_rate;
            }
        }
    }

    void Attack()
    {
        
        anim.SetTrigger("Attack");
        //Задержка

       Collider2D[] Hit_Enemies = Physics2D.OverlapCircleAll(attack_Point.position, attack_range, enemy_Layer);

          foreach (Collider2D enemy in Hit_Enemies)
        {
            enemy.GetComponent<Enemy>().Take_Damage(Damage);
            
            if (Skills_Manager.use.Is_Enable_Passive_skills_Warrior[0] == true)
            {

               

                if (Skills_Manager.use.Passive_skills_Warrior[2] == 1)
                {
                    if (Random.Range(1, 16) < 3)
                    {
                        enemy.GetComponent<Enemy>().Take_Damage(Damage * 1.3f);
                    }

                }
                else if (Skills_Manager.use.Passive_skills_Warrior[2] == 2)
                {
                    if (Random.Range(1, 31) < 7)
                    {
                        enemy.GetComponent<Enemy>().Take_Damage(Damage * 1.7f);
                    }

                }
                else 
                {
                    if (Random.Range(1, 16) < 4)
                    {
                        enemy.GetComponent<Enemy>().Take_Damage(Damage * 2.1f);
                    }

                }
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if(attack_Point == null)
           return;
        
        Gizmos.DrawWireSphere(attack_Point.position, attack_range);
    }

 
}
