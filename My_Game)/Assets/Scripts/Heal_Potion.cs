using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Potion : MonoBehaviour
{
    [SerializeField]
    private float Heal = 15f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Main_Hero>() != null)
        {
            collision.GetComponent<Main_Hero>().AddHealth(Heal);
        }
        Destroy(this.gameObject);
    }


}
