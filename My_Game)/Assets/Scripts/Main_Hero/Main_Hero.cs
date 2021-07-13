using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
public class Main_Hero : MonoBehaviour
{
    public float Move_Speed;
    public float JumpPower;
    public bool Facing_Right;
    public Slider slider;
    public Vector3 Offset;
    public float Max_health;


    [SerializeField]
    private Color Low;
    [SerializeField]
    private Color High;

    [SerializeField]
    private GameObject _camera;
    private Camera_Folow _cameraSkript;

    private bool double_Jump = true;
    private float armor_Rate;
    private float dirX;
    private Rigidbody2D rb;
    private Vector3 Local_Scale;
    private Animator anim;
    private float hp;

    private AudioSource _runSound;
    private AudioSource _jumpingSound;
    private AudioSource _damageTakeSound;
    private AudioSource _healthPotion;
    [SerializeField , Header("4 - Health Potion"), Header("1 - Run, 2 - Jump 3 - Damage Taken")]
    private AudioClip[] _audio;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
         hp = Max_health;
        SetHealth(Max_health, Max_health);
    }
    void Start()
    {
        _cameraSkript = _camera.GetComponent<Camera_Folow>();
        AudioLoad();
        armor_Rate = 55;
        Local_Scale = transform.localScale;
      
    }
    void Update()
    {
        dirX = CrossPlatformInputManager.GetAxis("Horizontal") * Move_Speed;

        if (rb.velocity.y == 0)
        {
            double_Jump = true;

        }
        if (CrossPlatformInputManager.GetButtonDown("Jump") && (Skills_Manager.use.Is_Enable_Passive_skills_Warrior[7] == true || Skills_Manager.use.Is_Enable_Passive_skills_Warrior[8] == true) && rb.velocity.y != 0 && double_Jump == true)
        {
            _jumpingSound.Play();
            if (double_Jump == false)
            {
                rb.AddForce(Vector2.up * 560f);
            }
            else
            {
                rb.AddForce(Vector2.up * (JumpPower / 1.5f) ); //For jump from sand
            }
            double_Jump = false;
        }
        if (CrossPlatformInputManager.GetButtonDown("Jump") && rb.velocity.y == 0)
        {
            Debug.Log("Dt");
            _jumpingSound.Play();
            rb.AddForce(Vector2.up * JumpPower);

        }

        if(Mathf.Abs(dirX) > 0 && rb.velocity.y == 0)
        {
            if(!_runSound.isPlaying)
            _runSound.Play();
            anim.SetBool("Is_Running", true);
        }
        else
        {
            _runSound.Stop();
            anim.SetBool("Is_Running", false);
        }

        if(rb.velocity.y == 0)
        {

            anim.SetBool("Is_Jumping", false);
            anim.SetBool("Is_Falling", false);
        }
        else if(rb.velocity.y > 0)
        {
            anim.SetBool("Is_Jumping", true);
        }
        else
        {
            anim.SetBool("Is_Jumping", false);
            anim.SetBool("Is_Falling", true);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, rb.velocity.y);
    }

    private void LateUpdate()
    {
        if(dirX > 0)
        {
            if (_cameraSkript._offset.x < 0)
            _cameraSkript._offset.x *= -1;
            Facing_Right = true;
        }
        else if (dirX < 0)
        {
            if (_cameraSkript._offset.x > 0)
               _cameraSkript._offset.x *= -1;
            Facing_Right = false;
        }

        if(( ( Facing_Right) && (Local_Scale.x < 0)) || (!Facing_Right) && (Local_Scale.x > 0)){
            Local_Scale.x *= -1;
        }
        transform.localScale = Local_Scale;
    }

    private void OnCollisionStay2D(Collision2D collision) // Bad idea, need fix
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }

    public IEnumerator KnockBack(float knockBack_duration, float knockPower, Transform obj)
    {
        float timer = 0;
        while(knockBack_duration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
           Debug.Log("X = " + direction.x + " Y = " + direction.y);
          // Debug.Log(direction.y);
            rb.AddForce(-direction * knockPower);
        }
        yield return 0;

    }


    public void Take_Damage(float damage)
    {
 
        float Armor_Boost = 0f;
        if (Skills_Manager.use.Is_Enable_Passive_skills_Warrior[6] == true)
        {
            Armor_Boost += 15f;
        }
        if (Skills_Manager.use.Is_Enable_Passive_skills_Warrior[8] == true)
        {
            Armor_Boost += 9f;
        }
        
        bool Dodged = false;

        if ( Skills_Manager.use.Is_Enable_Passive_skills_Warrior[3] == true)
        {
            int Chance = 20;
            if (Random.Range(1, 100) <= Chance)
            {
                Dodged = true;
            }
        }
        if (Dodged == false)
        {
            damage -= ( (armor_Rate + Armor_Boost) * damage) / 100; //Armor influence
            _damageTakeSound.Play();
            hp -= damage;
        }
        else
        {
            Debug.Log("Dodged");
        }
        if(hp <= 0)
        {
            die();
        }
        SetHealth(hp, Max_health);
    }

    public void AddHealth(float health_to_add)
    {
        _healthPotion.Play();
        hp = Mathf.Min((health_to_add + hp), Max_health);
        SetHealth(hp, Max_health);
    }
    public void SetHealth(float health, float maxHealth)
    {

        slider.gameObject.SetActive(health <= maxHealth);
        slider.maxValue = maxHealth;
        slider.value = health ;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);

    }

    private void die()
    {
        SceneManager.LoadScene(4);
    }

    private void AudioLoad()
    {
        _runSound = gameObject.AddComponent<AudioSource>();
        _runSound.playOnAwake = false;
        _runSound.clip = _audio[0];

        _jumpingSound = gameObject.AddComponent<AudioSource>();
        _jumpingSound.playOnAwake = false;
        _jumpingSound.clip = _audio[1];
        _jumpingSound.volume = 0.8f;

        _damageTakeSound = gameObject.AddComponent<AudioSource>();
        _damageTakeSound.playOnAwake = false;
        _damageTakeSound.clip = _audio[2];

        _healthPotion = gameObject.AddComponent<AudioSource>();
        _healthPotion.playOnAwake = false;
        _healthPotion.clip = _audio[3];
    }

}
