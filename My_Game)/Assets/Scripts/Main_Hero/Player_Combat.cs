using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player_Combat : MonoBehaviour
{
    public ParticleSystem Super;
    public ParticleSystem Super2;
    public Animator Anim;


    [SerializeField]
    private GameObject _meleeAttack;
    [SerializeField]
    private float _damage = 25f;

    public float attack_range_base;
    public LayerMask Enemy_Layer;



    private Fireball fireball;
    private Rain rain;
    private float _attackRate = 2f; // Hits per second
    private float Fireball_rate = 0.5f;
    private float Rain_rate = 0.3f;
    private float SupperAttack_rate = 1.2f;
    private Main_Hero m_h;
    private float _nextAttackTime = 0f;
    private float _nextFireballTime = 0f;
    private float _nextRainTime = 0f;
    private float _nextSuperAtackTime = 0f;
    private AudioSource _swordAttackSound;
    private AudioSource _fireballCastSound;
    private AudioSource _superSwordAttackSound;
    private AudioSource _rainCastSound;
    [SerializeField, Header("1- Sword Attack 2 - Fire Ball Cast, 3 - Sword Hit 4 - Rain")]
    private AudioClip[] _audio;

    private void Awake()
    { 
        fireball = Resources.Load<Fireball>("fireball");
        rain = Resources.Load<Rain>("Rain");
    }
    private void Start()
    {
        AudioSet();
       m_h = GetComponent<Main_Hero>();

    }


    void Update()
    {
        if (Time.time >= _nextAttackTime)
        {
            if (CrossPlatformInputManager.GetButtonDown("attack"))
            {
                _swordAttackSound.Play();
                _meleeAttack.GetComponent<Melee_Attack>().Damage = _damage;
                _meleeAttack.GetComponent<Melee_Attack>().Super = false;
                Anim.SetTrigger("Attack");

                _nextAttackTime = Time.time + 1f / _attackRate;
            }

            if(Time.time >= _nextFireballTime && Time.time >= _nextAttackTime && Skills_Manager.use.Active_skills_Warrior[1] == true)
            {
                if (CrossPlatformInputManager.GetButtonDown("Skill_2"))
                {
                    _fireballCastSound.Play();
                    Anim.SetTrigger("Casting");
                    Invoke("Shoot_Fireball", 0.3f);
                    _nextAttackTime = Time.time + 1f / _attackRate;
                    _nextFireballTime = Time.time + 1f / Fireball_rate;
                }
            }
             if (Time.time >= _nextRainTime && Time.time >= _nextAttackTime && Skills_Manager.use.Active_skills_Warrior[2] == true)
            {

                if (CrossPlatformInputManager.GetButtonDown("Skill_3"))
                {
                    _rainCastSound.Play();
                    Anim.SetTrigger("Casting");
                    Invoke("Shoot_Rain", 0.3f);
                    _nextAttackTime = Time.time + 1f / _attackRate;
                    _nextRainTime = Time.time + 1f / Rain_rate;
                }
            }
            if (Time.time >= _nextSuperAtackTime && Time.time >= _nextAttackTime && Skills_Manager.use.Active_skills_Warrior[0] == true)
            {
                if (CrossPlatformInputManager.GetButtonDown("Skill_1"))
                {
                    _superSwordAttackSound.Play();
                    Anim.SetTrigger("Super_Attack");
                    _meleeAttack.GetComponent<Melee_Attack>().Damage = _damage;
                    _meleeAttack.GetComponent<Melee_Attack>().Super = true;
                    Invoke("SupperAttack", 0.3f);
                    _nextAttackTime = Time.time + 1f / _attackRate;
                    _nextSuperAtackTime = Time.time + 1f / SupperAttack_rate;
                }
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
    } 

  private void AudioSet()
    {
        _swordAttackSound = gameObject.AddComponent<AudioSource>();
        _swordAttackSound.playOnAwake = false;
        _swordAttackSound.clip = _audio[0];
        _swordAttackSound.volume = 0.65f;

        _fireballCastSound = gameObject.AddComponent<AudioSource>();
        _fireballCastSound.playOnAwake = false;
        _fireballCastSound.clip = _audio[1];   

         _superSwordAttackSound = gameObject.AddComponent<AudioSource>();
        _superSwordAttackSound.playOnAwake = false;
        _superSwordAttackSound.clip = _audio[2];
        _superSwordAttackSound.volume = 0.7f;

        _rainCastSound = gameObject.AddComponent<AudioSource>();
        _rainCastSound.playOnAwake = false;
        _rainCastSound.clip = _audio[3];

    }
}
