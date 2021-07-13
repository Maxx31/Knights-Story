using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Purple_Ball : MonoBehaviour
{
    private float speed = 10f;

    private Vector3 direction;

    public Vector3 Direction { set { direction = value; } }

    private float liveTime;
    private ParticleSystem part;
    private bool _isUsed = false;
    private bool _isParticlePlayed = false;
    private void Awake()
    {
        part = GetComponentInChildren<ParticleSystem>();
        liveTime = 2.5f;
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
        if (collision.GetComponent<Main_Hero>() != null) //If our collision - main hero
        {
            if (_isUsed == false)
            {
                collision.GetComponent<Main_Hero>().Take_Damage(35);
                _isUsed = true;
            }
        }
        if (collision.tag != "Useless" && _isParticlePlayed == false) //Useless is all the back, so our ball don't destray when hitting it.
        {

            speed = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);

            transform.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 1.1f, transform.localScale.z);// Making Ball a bit bigger
            if (part != null)
            {
                part.Play();
            }
            _isParticlePlayed = true;
            Destroy(gameObject, 0.5f);
        }
    }
}
