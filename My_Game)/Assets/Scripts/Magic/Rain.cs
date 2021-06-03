using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public float Is_Right { set { is_Right = value; } }

    private float is_Right;

    private Rigidbody2D rb;

    private ParticleSystem part;

    private float liveTime;
    private void Awake()
    {
         part = GetComponentInChildren<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
        liveTime = 4f;
    }

    private void Start()
    {
        Destroy(gameObject, liveTime);
        rb.velocity = new Vector2(is_Right, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
            collision.GetComponent<Enemy>().Take_Damage(30);
        if (collision.tag != "Useless")
        {      
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;

            if (part != null)
            {
                part.Play();
            }
            Destroy(gameObject , 0.42f);
        }
    }
}
