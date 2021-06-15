using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Skill : MonoBehaviour
{
    [SerializeField]
    private int num;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Skills_Manager.use.Active_skills_Warrior[num] = true;
        Skills_Manager.use.Active_SKill_Set(num);
        Destroy(this.gameObject);
    }
}
