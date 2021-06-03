using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills_Manager : MonoBehaviour
{
   public int[] Passive_skills_Warrior = new int[10];
    /*
    1 - Extra Damage
    2 - Extra Move speed
    3 - Сritical damage
    4 - Evasion
    5 - Chests Open
    6 - Armor penetration
    7 - Extra Armor
    8 - Double jump
    9 - FLAG(double jump, extra movespeed, extra armor)
     */





    public bool[] Is_Enable_Passive_skills_Warrior = new bool[10];

    public int[] Active_Button_Warrior = new int[3];

    public int[] Active_skills_Warrior = new int[10];

    private static Skills_Manager _use;
    public static Skills_Manager use
    {
        get
        {
                return _use;
            
        }
    }
    void Awake()
    {

        Passive_skills_Warrior[7] = 1;
        Is_Enable_Passive_skills_Warrior[7] = true;

        if (_use == null) //Singleton
        {
            Debug.Log("_Use = null");
            _use = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Debug.Log("_Use != null");
            Destroy(this);

        }
    }

}
