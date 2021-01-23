using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    public float speed;

    public int position_Of_Patrol;
    public bool is_Bomj = false;
    public Transform point;
    private Vector3 Local_Scale;
    public bool Moving_Right;
    Rigidbody2D rb;
    Transform player;
    public Transform GFX;
    public float Stopping_Distanse;
    bool chill = false;
    bool angry = false;
    bool go_back = false;
    private float dirX;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Local_Scale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
            
        if (Vector2.Distance(transform.position, point.position) < position_Of_Patrol && angry == false)
        {
            chill = true;
        }

        if(Vector2.Distance(transform.position , player.position) < Stopping_Distanse)
        {
            angry = true;
            chill = false;
            go_back = false;
        }

        if (Vector2.Distance(transform.position, player.position) > Stopping_Distanse)
        {
            go_back = true;
            angry = false;
        }

        if(chill == true)
        {
            Chill();
        }else if(angry == true)
        {
            Angry();
        }else if( go_back == true)
        {
            Go_Back();
        }
    }

    void Chill()
    {
   
        if (transform.position.x > point.position.x + position_Of_Patrol)
        {
            Moving_Right = false;
        }
        else if (transform.position.x < point.position.x - position_Of_Patrol)
        {
            Moving_Right = true;
        }

        if (Moving_Right)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }
    

    void Angry()
    {
        if (transform.position.x - player.position.x < 0)
        {
            Moving_Right = true;
        }
        else
        {
            Moving_Right = false;
        }
        transform.position = Vector2.MoveTowards(transform.position , player.position, speed * Time.deltaTime);
    }
    void Go_Back()
    {
      
        if((transform.position.x - point.position.x) > 0)
        {
            Moving_Right = false;
        }
        else
        {
            Moving_Right = true;
        }
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {

        if (Moving_Right == false)
        {
            if (is_Bomj == true)
            {
                GFX.localScale = new Vector3(0.167544f, 0.167544f, 1f);
            }
            else
            {
                GFX.localScale = new Vector3(1f,1f, 1f);
            }
        }
        else if (Moving_Right == true)
        {
            if (is_Bomj == true)
            {
               GFX.localScale = new Vector3(-0.167544f, 0.167544f, 1f);
            }
            else
            {
               GFX.localScale = new Vector3( -1f, 1f, 1f);
            }
        }


    }
}
