using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float speed ;

    private Vector3 direction;

    public Vector3 Direction { set { direction = value; } }

    private Animator anim;

    private ParticleSystem part;

    private float liveTime;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        part = GetComponentInChildren<ParticleSystem>();
        liveTime = 2.5f;
        speed = 10f;
    }

    private void Start()
    {
         Destroy(gameObject, liveTime);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().Take_Damage(50);
        }

        if (collision.tag != "Useless") {
            anim.SetTrigger("Explose");
            speed = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
            transform.localScale = new Vector3  (transform.localScale.x * 1.1f,   transform.localScale.y * 1.1f,   transform.localScale.z);
            if(part != null)
            part.Play();
            Destroy(gameObject, 0.5f);
        }
    }
}
