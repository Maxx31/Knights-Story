using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Starter : MonoBehaviour
{
    [SerializeField]
    private GameObject _boss;

    [SerializeField]
    private Boss_Wall _bossWall;
    
    [SerializeField]
    private GameObject _beginWall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Main_Hero>() != null)
        {
            _beginWall.GetComponent<SpriteRenderer>().size = new Vector2(4.961783f,140);
            _beginWall.GetComponent<BoxCollider2D>().size = new Vector2(4.961783f, 140);
            Instantiate(_boss);
            _bossWall.Initialize();
            Destroy(this.gameObject);
        }
    }

    
}
