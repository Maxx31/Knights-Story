using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death_Block : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            SceneManager.LoadScene(4);
        }
        else if(collision.gameObject.GetComponent<Enemy>() != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
