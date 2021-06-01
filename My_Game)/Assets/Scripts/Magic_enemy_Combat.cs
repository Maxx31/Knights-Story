using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_enemy_Combat : MonoBehaviour
{

    private Enemy_Purple_Ball Mage_Ball;

    public float Attack_rate;
    float Next_Attact_Time = 0f;
    private Patrol patrol;
    private bool In_range = false;
    public Animator anim;

    //public Transform Main;

    private void Start()
    {
        patrol = GetComponentInParent<Patrol>();
        Mage_Ball = Resources.Load<Enemy_Purple_Ball>("Mage_Ball");
    }
    private void Update()
    {
        if (Time.time >= Next_Attact_Time && In_range == true && transform.parent.GetComponent<Enemy>().Dead == false)
        {

            anim.SetTrigger("Attack");
            Invoke("Shoot_Fireball", 0.5f);
            //  Attack();
            Next_Attact_Time = Time.time + 1f / Attack_rate;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            In_range = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            In_range = false;
        }
    }

    private void Shoot_Fireball()
    {
        Vector3 position = transform.parent.position;

        if (patrol.Moving_Right == false)
        {
            position.x -= 1.3f;//Настроить откуда вылетает
        }
        else
        {
            position.x += 1.3f;//Настроить откуда вылетает
        }
        //продолжаем концерт
        position.y -= 0.1f;
        Enemy_Purple_Ball new_Purple_Ball = Instantiate(Mage_Ball, position, Mage_Ball.transform.rotation) as Enemy_Purple_Ball;
        if (patrol.Moving_Right == false)
        {

            new_Purple_Ball.transform.localScale = new Vector3(-1f, 1f, 1f);
            new_Purple_Ball.Direction = new_Purple_Ball.transform.right * (-1);
        }
        else
        {
            new_Purple_Ball.transform.localScale = new Vector3(1f, 1f, 1f);
            new_Purple_Ball.Direction = new_Purple_Ball.transform.right;
        }

    }
}
