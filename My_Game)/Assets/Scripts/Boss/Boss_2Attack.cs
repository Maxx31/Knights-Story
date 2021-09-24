using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2Attack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           collision.GetComponent<Main_Hero>().Take_Damage(35f);
        }
    }
}
