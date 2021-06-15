using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField]
    private float damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Main_Hero>() != null)
        {
            collision.gameObject.GetComponent<Main_Hero>().Take_Damage(damage);
        }
    }
}
