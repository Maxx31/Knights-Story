using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_paser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Block")
        {
            transform.parent.GetComponent<Enemy>().Jump();
        }
    }
}
