using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField , Tooltip("Damage per second")]
    private float damage;

    private AudioSource _sawSound;


    private void Awake()
    {
        _sawSound = GetComponent<AudioSource>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(_sawSound.isPlaying == false)
        {
            _sawSound.Play();
        }
        if (collision.gameObject.GetComponent<Main_Hero>() != null)
        {
            collision.gameObject.GetComponent<Main_Hero>().Mace_Damage(damage);
        }
        else if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            collision.gameObject.GetComponent<Enemy>().Take_Damage(damage);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _sawSound.Stop();
    }
}
