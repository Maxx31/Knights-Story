using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRain : MonoBehaviour
{

    private Rigidbody2D rb;

    private ParticleSystem part;

    private float speed;
    private float liveTime;
    private float damage;
    private AudioSource _rainExplosion;
    private void Awake()
    {
        part = GetComponentInChildren<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
        liveTime = 4f;
        speed = 14f;
    }

    private void Start()
    {
        Debug.Log("Spawned");
        _rainExplosion = GetComponent<AudioSource>();
        Destroy(gameObject, liveTime);
        rb.velocity = new Vector2(1.5f, rb.velocity.y - speed);
        damage = 80f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Main_Hero>() != null)
            collision.GetComponent<Main_Hero>().Take_Damage(damage);
        if (collision.tag != "Useless" && collision.tag != "BossPlatform")
        {
         //   if (!_rainExplosion.isPlaying)
        //        _rainExplosion.Play();
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;

            if (part != null)
            {
                part.Play();
            }

            Destroy(gameObject, 0.25f);
        }
    }
}
