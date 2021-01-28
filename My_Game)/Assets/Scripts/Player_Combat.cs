using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player_Combat : MonoBehaviour
{

    public Animator anim;
    private Fireball fireball;
    public Transform attack_Point;
    public float attack_range ;
    private float Attack_rate = 23f;// chi
    private float Fireball_rate = 23f;// chi
    public LayerMask enemy_Layer;
    private float Damage = 25f;
    float Next_Attact_Time = 0f;
    private Main_Hero m_h;
    float Next_FireBall_Time = 0f;

    private void Awake()
    { 
        fireball = Resources.Load<Fireball>("fireball");
    }
    private void Start()
    {
        m_h = GetComponent<Main_Hero>();
        
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
        //    Debug.Log("Chi");
            if (CrossPlatformInputManager.GetButtonDown("attack"))
            {
                anim.SetTrigger("Attack");
                Invoke("Attack", 0.2f);
                Next_Attact_Time = Time.time + 1f / Attack_rate;
            }
           else if(Time.time>= Next_FireBall_Time)
            {
                if (CrossPlatformInputManager.GetButtonDown("Skill_1"))
                {
                    anim.SetTrigger("Casting");
                    Invoke("Shoot_Fireball", 0.3f);
                   // Next_Attact_Time = Time.time + 1f / Attack_rate;
                 //   Next_FireBall_Time = Time.time + 1f / Fireball_rate;
                }
            }
        }
    }

    void Attack()
    {

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

    private void Shoot_Fireball()
    {
        Vector3 position = transform.position;
        if (m_h.Facing_Right == false)
        {
            position.x -= 1.12f;//Настроить откуда вылетает
        }
        else
        {
            position.x += 1.12f;//Настроить откуда вылетает
        }
        position.y -= 1.1f;
           Fireball new_Fireball =  Instantiate(fireball, position, fireball.transform.rotation) as Fireball;

        if (m_h.Facing_Right == false)
        {

            new_Fireball.transform.localScale = new Vector3(-1f, 1f, 1f);
            new_Fireball.Direction = new_Fireball.transform.right * (-1);
        }
        else
        {
            new_Fireball.transform.localScale = new Vector3(1f, 1f, 1f);
            new_Fireball.Direction = new_Fireball.transform.right ;
        }

    }

    private void OnDrawGizmosSelected()
    {
        if(attack_Point == null)
           return;
        
        Gizmos.DrawWireSphere(attack_Point.position, attack_range);
    }

 
}
