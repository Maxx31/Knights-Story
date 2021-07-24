using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Skill : MonoBehaviour
{
    [SerializeField]
    private int num;

    [SerializeField]
    private Animator anim;


    private void Start()
    {
       
       // Debug.Log(Skills_Manager.use.Active_skills_Warrior[num]);
        if (Singleton_Skills_Manager.use.Active_skills_Warrior[num] == true)
        {
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            anim.SetTrigger("TextShow");
            Skills_Manager.use.Active_skills_Warrior[num] = true;
            Skills_Manager.use.Active_SKill_Set(num);
            Destroy(this.gameObject);
        }
    }
}
