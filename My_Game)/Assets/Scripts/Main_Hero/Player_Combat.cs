using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player_Combat : MonoBehaviour
{
    public ParticleSystem Super;
    public ParticleSystem Super2;
    public Animator Anim;
    public Transform Attack_Point;
    public float attack_range_base;
    public LayerMask Enemy_Layer;

    private Fireball fireball;
    private Rain rain;
    private float Attack_rate = 23f; 
    private float Fireball_rate = 23f;
    private float Rain_rate = 23f;
    private float SupperAttack_rate = 23f;
    private float Damage = 25f;
    private Main_Hero m_h;
    private float Next_Attact_Time = 0f;
    private float Next_FireBall_Time = 0f;
    private float Next_Rain_Time = 0f;
    private float Next_SupperAttack_Time = 0f;

    private void Awake()
    { 
        fireball = Resources.Load<Fireball>("fireball");
        rain = Resources.Load<Rain>("Rain");
    }
    private void Start()
    {
        m_h = GetComponent<Main_Hero>();

    }


    void Update()
    {
        if (Time.time >= Next_Attact_Time)
        {
            if (CrossPlatformInputManager.GetButtonDown("attack"))
            {
                Anim.SetTrigger("Attack");
                Invoke("Attack_Call", 0.2f);
                Next_Attact_Time = Time.time + 1f / Attack_rate;
            }

            if(Time.time >= Next_FireBall_Time && Time.time >= Next_Attact_Time && Skills_Manager.use.Active_skills_Warrior[1] == true)
            {
                if (CrossPlatformInputManager.GetButtonDown("Skill_2"))
                {
                    Anim.SetTrigger("Casting");
                    Invoke("Shoot_Fireball", 0.3f);
                   // Next_Attact_Time = Time.time + 1f / Attack_rate;
                 //   Next_FireBall_Time = Time.time + 1f / Fireball_rate;
                }
            }
             if (Time.time >= Next_Rain_Time && Time.time >= Next_Attact_Time && Skills_Manager.use.Active_skills_Warrior[2] == true)
            {

                if (CrossPlatformInputManager.GetButtonDown("Skill_3"))
                {
                    Anim.SetTrigger("Casting");
                    Invoke("Shoot_Rain", 0.3f);
                    // Next_Attact_Time = Time.time + 1f / Attack_rate;
                    //   Next_Rain_Time = Time.time + 1f / Rain_rate;
                }
            }
            if (Time.time >= Next_SupperAttack_Time && Time.time >= Next_Attact_Time && Skills_Manager.use.Active_skills_Warrior[0] == true)
            {

                if (CrossPlatformInputManager.GetButtonDown("Skill_1"))
                {
                    Anim.SetTrigger("Super_Attack");
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
        float tDamage = Damage;
        
        if (Skills_Manager.use.Is_Enable_Passive_skills_Warrior[0] == true)
        {
            tDamage *= 1.5f;
        }
            if (Super == true)
        {
            tDamage *= 1.5f;
            attack_range += 2.5f;
        }
       Collider2D[] Hit_Enemies = Physics2D.OverlapCircleAll(Attack_Point.position, attack_range, Enemy_Layer);
          foreach (Collider2D enemy in Hit_Enemies)
        {
            if (Skills_Manager.use.Is_Enable_Passive_skills_Warrior[2] == true)
            {
                    if (Random.Range(1, 16) < 4)
                    {
                    tDamage *= 2;
                    }
            }
            if(enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().Take_Damage(tDamage);
            }
        }
    }

    private void Shoot_Fireball()
    {
        Vector3 position = transform.position;
        if (m_h.Facing_Right == false)
        {
            position.x -= 1.12f;//starting point
        }
        else
        {
            position.x += 1.12f;//starting point
        }
        position.y -= 1.1f;
           Fireball new_Fireball =  Instantiate(fireball, position, fireball.transform.rotation) ;

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
                position.x -= (0.9f + (Number * 1.55f) ); // Place where rain begin
            }
            else
            {
                position.x += (0.9f + (Number * 1.55f));// Place where rain begin
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
        if(Attack_Point == null)
           return;
        
        Gizmos.DrawWireSphere(Attack_Point.position, attack_range_base);
    }

 
}
