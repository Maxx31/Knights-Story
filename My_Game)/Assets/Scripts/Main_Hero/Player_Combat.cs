using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player_Combat : MonoBehaviour
{
    public ParticleSystem Super;
    public ParticleSystem Super2;
    public Animator anim;
    private Fireball fireball;
    private Rain rain;

    public Transform attack_Point;
    public float attack_range_base ;
    private float Attack_rate = 23f;// chi
    private float Fireball_rate = 23f;// chi
    private float Rain_rate = 23f;// chi
    private float SupperAttack_rate = 23f;// chi
    public LayerMask enemy_Layer;
    private float Damage = 25f;
    float Next_Attact_Time = 0f;
    private Main_Hero m_h;
    float Next_FireBall_Time = 0f;
    float Next_Rain_Time = 0f;
    float Next_SupperAttack_Time = 0f;

    private void Awake()
    { 
        fireball = Resources.Load<Fireball>("fireball");
        rain = Resources.Load<Rain>("Rain");
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
                Invoke("Attack_Call", 0.2f);
                Next_Attact_Time = Time.time + 1f / Attack_rate;
            }

            if(Time.time >= Next_FireBall_Time && Time.time >= Next_Attact_Time)
            {
                if (CrossPlatformInputManager.GetButtonDown("Skill_1"))
                {
                    anim.SetTrigger("Casting");
                    Invoke("Shoot_Fireball", 0.3f);
                   // Next_Attact_Time = Time.time + 1f / Attack_rate;
                 //   Next_FireBall_Time = Time.time + 1f / Fireball_rate;
                }
            }
             if (Time.time >= Next_Rain_Time && Time.time >= Next_Attact_Time)
            {

                if (CrossPlatformInputManager.GetButtonDown("Skill_2"))
                {
                    anim.SetTrigger("Casting");
                    Invoke("Shoot_Rain", 0.3f);
                    // Next_Attact_Time = Time.time + 1f / Attack_rate;
                    //   Next_Rain_Time = Time.time + 1f / Rain_rate;
                }
            }
            if (Time.time >= Next_Rain_Time && Time.time >= Next_Attact_Time)
            {

                if (CrossPlatformInputManager.GetButtonDown("Skill_3"))
                {
                    anim.SetTrigger("Super_Attack");
                    Invoke("SupperAttack", 0.3f);
                    // Next_Attact_Time = Time.time + 1f / Attack_rate;
                    //   Next_SupperAttack_Time = Time.time + 1f / SupperAttack_rate;
                }
            }
        }
    }

    void Attack(bool Super = false)
    {
        float attack_range = attack_range_base;
        if (Super == true)
        {
            attack_range += 2.5f;
        }
       Collider2D[] Hit_Enemies = Physics2D.OverlapCircleAll(attack_Point.position, attack_range, enemy_Layer);
        Debug.Log(Hit_Enemies.Length);
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
            Debug.Log("Bolno v noge");
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

    private void Shoot_Rain()
    {
        for (int Number = 0; Number < 5; Number++)
        {
            Vector3 position = transform.position;
            if (m_h.Facing_Right == false)
             {
                position.x -= (0.9f + (Number * 1.55f) ); //(3.9f + (Number * 1.55f) ) 
            }
            else
            {
                position.x += (0.9f + (Number * 1.55f));//Настроить откуда вылетает  (3.9f + (Number * 1.55f))
            }
            position.y += 7f;
            Rain new_Rain = Instantiate(rain, position, rain.transform.rotation) as Rain;

            if (m_h.Facing_Right == false)
            {

                new_Rain.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                new_Rain.Is_Right = -3.45f;
            }
            else
            {
                new_Rain.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                new_Rain.Is_Right = 3.45f;
            }
        }
    }
    private void SupperAttack()
    {
        var sh = Super.shape;
        var sh2 = Super2.shape;
        if (m_h.Facing_Right == false)
        {
            sh.scale = new Vector3(-1, 1, 1);
            sh2.scale = new Vector3(-1, 1, 1);
        }
        else
        {
            sh.scale = new Vector3(1, 1, 1);
            sh2.scale = new Vector3(1, 1, 1);
        }
        Super.Emit(30);
        Super2.Emit(50);
        
        Attack(true);
    } 
    private void Attack_Call()
    {
        Attack(false);
    }
    private void OnDrawGizmosSelected()
    {
        if(attack_Point == null)
           return;
        
        Gizmos.DrawWireSphere(attack_Point.position, attack_range_base);
    }

 
}
