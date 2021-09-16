using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Starter : MonoBehaviour
{
    [SerializeField]
    private GameObject _boss;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Main_Hero>() != null)
        {
            Instantiate(_boss);
            Destroy(this.gameObject);
        }
    }

    
}
