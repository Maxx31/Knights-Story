using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Main_Hero : MonoBehaviour
{
    public float Move_Speed;
    public bool Facing_Right;
    public Slider slider;
    public Vector3 Offset;
    public float Max_health;
    [SerializeField]
    private Color Low;
    [SerializeField]
    private Color High;

    private bool double_Jump = true;
    private float armor_Rate;
    private float dirX;
    private Rigidbody2D rb;
    private Vector3 Local_Scale;
    private Animator anim;
    private float hp;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
         hp = Max_health;
        SetHealth(Max_health, Max_health);
    }
    void Start()
    {
        armor_Rate = 55;
        Local_Scale = transform.localScale;
        Move_Speed = 15f;
      
    }
    void Update()
    {
        dirX = CrossPlatformInputManager.GetAxis("Horizontal") * Move_Speed;

        if (rb.velocity.y == 0)
        {
            double_Jump = true;

        }
        if (CrossPlatformInputManager.GetButtonDown("Jump") && (Skills_Manager.use.Is_Enable_Passive_skills_Warrior[7] == true ||  Skills_Manager.use.Is_Enable_Passive_skills_Warrior[8] == true )&& rb.velocity.y != 0 && double_Jump == true)
        {
            double_Jump = false;
            rb.AddForce(Vector2.up * 560f);
        }
        if (CrossPlatformInputManager.GetButtonDown("Jump") && rb.velocity.y == 0)
        {      
            rb.AddForce(Vector2.up * 700f);

        }

        if(Mathf.Abs(dirX) > 0 && rb.velocity.y == 0)
        {
            anim.SetBool("Is_Running", true);
        }
        else
        {
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
            Facing_Right = true;
        }
        else if (dirX < 0)
        {
            Facing_Right = false;
        }

        if(( ( Facing_Right) && (Local_Scale.x < 0)) || (!Facing_Right) && (Local_Scale.x > 0)){
            Local_Scale.x *= -1;
        }
        transform.localScale = Local_Scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = collision.gameObject.transform;
        }
        if (collision.gameObject.CompareTag("Mace"))
        {
           // Vector3 dir = (Vector3)collision.contacts[0].point - transform.position;
           // dir = -dir.normalized;
           // rb.AddForce(dir * 100, ForceMode2D.Impulse);
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
        //Debug.Log("Damage = " + damage);

      //  if (hp - damage <= 0) return;

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
            hp -= damage;
        }
        else
        {
            Debug.Log("Dodged");
        }

        SetHealth(hp, Max_health);
    }

    public void AddHealth(float health_to_add)
    {
        hp = Mathf.Min((health_to_add + hp), Max_health);
        SetHealth(hp, Max_health);
        Debug.Log(hp);
    }
    public void SetHealth(float health, float maxHealth)
    {

        slider.gameObject.SetActive(health <= maxHealth);
        slider.maxValue = maxHealth;
        slider.value = health ;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);

    }



}
