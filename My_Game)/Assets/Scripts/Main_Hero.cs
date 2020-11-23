using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Main_Hero : MonoBehaviour
{
    int temp = 0;
    private float dirX;
    Rigidbody2D rb;
    public float Move_Speed;
    private bool Facing_Right;
    private Vector3 Local_Scale;
    private Animator anim;
    public float HP;
    private float Armor_Rate;
    // Start is called before the first frame update
    void Start()
    {
        Armor_Rate = 55;
        Local_Scale = transform.localScale;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Move_Speed = 15f;
        HP = 100;
        float Armor_Boost = 3f;
        if (Skills_Manager.use.Is_Enable_Passive_skills_Warrior[6] == true)
        {
            for (int i = 1; i < Skills_Manager.use.Passive_skills_Warrior[6]; i++)
            {
                Armor_Boost = ((Armor_Boost) * 2) + 1;

            }
            Armor_Rate += Armor_Boost;
        }
        if (Skills_Manager.use.Passive_skills_Warrior[1] > 0)
        {
            if (Skills_Manager.use.Is_Enable_Passive_skills_Warrior[0] == true)
            {
                switch (Skills_Manager.use.Passive_skills_Warrior[1])
                {
                    case 1:
                        Move_Speed += 2f;
                        break;
                    case 2:
                        Move_Speed += 4f;
                        break;
                    case 3:
                        Move_Speed += 7f;
                        break;
                }
            }
        }

    }




    // Update is called once per frame
    void Update()
    {
        
        dirX = CrossPlatformInputManager.GetAxis("Horizontal") * Move_Speed;

        if (CrossPlatformInputManager.GetButtonDown("Jump") && rb.velocity.y == 0)
            rb.AddForce(Vector2.up * 700f);

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

    public void Take_Damage(float damage)
    {
        bool Dodged = false;
        int Chance = 9;

        if (Skills_Manager.use.Passive_skills_Warrior[3] > 0 && Skills_Manager.use.Is_Enable_Passive_skills_Warrior[3] == true)
        {

            for (int i = 1; i < Skills_Manager.use.Passive_skills_Warrior[3]; i++)
            {
                Chance += Chance - (4 - i * 2);
            }
            if (Random.Range(1, 100) <= Chance)
            {
                Dodged = true;
            }

        }
        if (Dodged == false)
        {
            damage -= (Armor_Rate * damage) / 100; //Влияние Армора на получаемый урон
            HP -= damage;
            Debug.Log("Current health:" + HP);
        }
        else
        {
            Debug.Log("Dodged");
        }
        
    }
}
