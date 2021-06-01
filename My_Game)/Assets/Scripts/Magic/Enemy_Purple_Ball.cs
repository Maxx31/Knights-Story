using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Purple_Ball : MonoBehaviour
{
    private float speed = 10f;

    private Vector3 direction;

    public Vector3 Direction { set { direction = value; } }


    private ParticleSystem part;
    private void Awake()
    {
        part = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {

        Destroy(gameObject, 2.5f);
        // part.Play();
    }
    private void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Main_Hero>() != null)
            collision.GetComponent<Main_Hero>().Take_Damage(50);
        if (collision.tag != "Useless")
        {
            speed = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 1.1f, transform.localScale.z);
            if (part != null)
                part.Play();
            Destroy(gameObject, 0.5f);
        }
    }
}
