using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wall : MonoBehaviour
{
    private Boss boss;
    public void Initialize()
    {
        GameObject[] all = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < all.Length; i++)
        {
            if (all[i].GetComponent<Boss>() != null)
            {
                boss = all[i].GetComponent<Boss>();
            }
        }
        boss.EndLevelAction += DeleteWall;
    }

    public void DeleteWall()
    {
        Destroy(this.gameObject);
    }
}
