using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    public float speed;

    public int position_Of_Patrol;

    public Transform point;

    public bool Moving_Right;

    Transform player;
    public float Stopping_Distanse;
    bool chill = false;
    bool angry = false;
    bool go_back = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, point.position) < position_Of_Patrol && angry ==false)
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
        if(transform.position.x > point.position.x + position_Of_Patrol)
        {
            Moving_Right = false;
        }
        else if(transform.position.x < point.position.x - position_Of_Patrol)
        {
            Moving_Right = true;
        }

        if (Moving_Right)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime , transform.position.y);
        }else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
         
    }
    

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position , player.position, speed * Time.deltaTime);
    }
    void Go_Back()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }
}
