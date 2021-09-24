using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Check : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            Debug.Log("IN");
            GetComponentInParent<Boss>().Obstacle = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            Debug.Log("out");
            GetComponentInParent<Boss>().Obstacle = false;
        }
    }
}
