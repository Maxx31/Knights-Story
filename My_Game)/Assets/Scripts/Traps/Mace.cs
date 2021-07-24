using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float force_repulsion;
    [SerializeField]
    private float knock_Duration;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Main_Hero>() != null)
        {
            StartCoroutine(collision.gameObject.GetComponent<Main_Hero>().KnockBack(knock_Duration, force_repulsion, this.transform));
            collision.gameObject.GetComponent<Main_Hero>().Take_Damage(damage);
        }
        else if(collision.gameObject.GetComponent<Enemy>() != null)
        {
            collision.gameObject.GetComponent<Enemy>().Take_Damage(damage);
        }
    }
}
