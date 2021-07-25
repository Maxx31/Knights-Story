using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Attack : MonoBehaviour
{
    public bool Super = false;
    public float Damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            float tDamage = Damage;

            if (Skills_Manager.use.Is_Enable_Passive_skills_Warrior[0] == true)
            {
                tDamage *= 1.2f;
            }
            if (Super == true)
            {
                tDamage *= 1.45f;
            }

            if (Skills_Manager.use.Is_Enable_Passive_skills_Warrior[2] == true)
            {
                if (Random.Range(1, 16) < 4)
                {
                    tDamage *= 2;
                }
            }
            if(Super == true)
            StartCoroutine(Attack_Call(collision, tDamage));
            else
            collision.GetComponent<Enemy>().Take_Damage(tDamage);
        }
    }

    IEnumerator Attack_Call(Collider2D enemy, float damage)
    {
       yield return new WaitForSeconds(0.2f);
        enemy.GetComponent<Enemy>().Take_Damage(damage);
    }
}
