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

    private bool is_taken = false;


    private void Start()
    {
        _manager = manager.GetComponent<Passive_Skills_Manager>();
        for (int i = 0; i < Singleton_Skills_Manager.use.Passive_skills_Warrior.Length; i++)
        {
            if (Singleton_Skills_Manager.use.Passive_skills_Warrior[i] == ccount) is_taken = true;
        }

        for (int i = 0; i < Singleton_Skills_Manager.use.Active_PassiveSkills.Length; i++)
        {
            if (Singleton_Skills_Manager.use.Active_PassiveSkills[i] == ccount) is_taken = true;
        }
        if (is_taken == true)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (is_taken == false)
            {
                anim.SetTrigger("TextShow");
                _manager.Add_Skill(ccount);
                Destroy(this.gameObject);
            }
            is_taken = true;
        }
    }
}
