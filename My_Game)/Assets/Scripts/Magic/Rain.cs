using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    private float speed = 10f;

    private float is_Right;

    public float Is_Right { set { is_Right = value; } }
    Rigidbody2D rb;

    private ParticleSystem part;

    private void Awake()
    {
         part = GetComponentInChildren<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, 4f);
        rb.velocity = new Vector2(is_Right, rb.velocity.y);
    }
    private void Update()
    {

    
    }


    private void FixedUpdate()
    {
     //   rb.velocity = new Vector2(is_Right, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
            collision.GetComponent<Enemy>().Take_Damage(30);
        if (collision.tag != "Useless")
        {      
            speed = 0;
            // transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
            // transform.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 1.1f, transform.localScale.z);
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;

            if (part != null)
            {
                Debug.Log("Namana");
                part.Play();
            }
            Destroy(gameObject , 0.42f);
        }
    }
}
