using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Main_Hero : MonoBehaviour
{

    private float dirX;
    Rigidbody2D rb;
    public float Move_Speed;
    private bool Facing_Right;
    private Vector3 Local_Scale;
    private Animator anim;
    public int HP;
    // Start is called before the first frame update
    void Start()
    {
        Local_Scale = transform.localScale;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Move_Speed = 5f;

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
}
