using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : MonoBehaviour
{
    bool chi = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Main_Hero>() != null)
        {
            Debug.Log("Entered");
            collision.gameObject.GetComponent<Main_Hero>().Move_Speed /= 1.8f;
            collision.gameObject.GetComponent<Main_Hero>().JumpPower /= 1.35f;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Main_Hero>() != null)
        {
            Debug.Log("Exit");
            collision.gameObject.GetComponent<Main_Hero>().Move_Speed *= 1.8f;
            collision.gameObject.GetComponent<Main_Hero>().JumpPower *= 1.35f;
        }
    }

}
