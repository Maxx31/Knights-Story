using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive_Skill : MonoBehaviour
{
    [SerializeField]
    private GameObject manager;
    [SerializeField]
    private int ccount;

    [SerializeField]
    private Animator anim;

    private Passive_Skills_Manager _manager;

    private void Start()
    {
        _manager = manager.GetComponent<Passive_Skills_Manager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("TextShow");
            _manager.Add_Skill(ccount);
            Destroy(this.gameObject);
        }
    }
}
