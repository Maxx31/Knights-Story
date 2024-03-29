using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Singleton_Skills_Manager : MonoBehaviour
{
    /*
    0 - Extra Damage 
    1 - Extra Move speed
    2 - �ritical damage
    3 - Evasion
    4 - Maximym HP increased
    5 - Armor penetration
    6 - Extra Armor
    7 - Double jump
    8 - FLAG(double jump, extra movespeed, extra armor)
     */

    public int[] Passive_skills_Warrior = new int[10];

    public string[] Str_Passive_skills_Warrior = new string[10];

    public int[] Active_PassiveSkills = new int[3];

    public string[] Str_Active_PassiveSkills = new string[3];

    public bool[] Is_Enable_Passive_skills_Warrior = new bool[10];
    public string[] Str_Is_Enable_Passive_skills_Warrior = new string[10];

    public bool[] Active_skills_Warrior = new bool[3];
    public string[] Str_Active_skills_Warrior = new string[3];

    public float AudioVolume;
    public string Str_AudioVolume;

    private static Singleton_Skills_Manager _use;
    public static Singleton_Skills_Manager use
    {
        get
        {
            return _use;

        }
    }
    private void Awake()
    {
        if(PlayerPrefs.GetInt("FirstTime") == 0) //First Time open
        {
            PlayerPrefs.SetInt("FirstTime", 1);
            for (int i = 0; i < Str_Passive_skills_Warrior.Length; i++)
            {
                PlayerPrefs.SetInt(Str_Passive_skills_Warrior[i] , -1);
            }

            for (int i = 0; i < Str_Active_PassiveSkills.Length; i++)
            {
               PlayerPrefs.SetInt(Str_Active_PassiveSkills[i] , -1);
            }

            for (int i = 0; i < Str_Active_skills_Warrior.Length; i++)
            {
               PlayerPrefs.SetInt(Str_Active_skills_Warrior[i] , boolToInt(false));
            }

            PlayerPrefs.SetFloat(Str_AudioVolume, 100f);

            for (int i = 0; i < Passive_skills_Warrior.Length; i++)
            {
                Passive_skills_Warrior[i] = PlayerPrefs.GetInt(Str_Passive_skills_Warrior[i]);
            }
            for (int i = 0; i < Active_PassiveSkills.Length; i++)
            {
                Active_PassiveSkills[i] = PlayerPrefs.GetInt(Str_Active_PassiveSkills[i]);
            }
            for (int i = 0; i < Active_skills_Warrior.Length; i++)
            {
                Active_skills_Warrior[i] = intToBool(PlayerPrefs.GetInt(Str_Active_skills_Warrior[i]));
            }

            AudioVolume = PlayerPrefs.GetFloat(Str_AudioVolume);
        }



        if (_use == null) 
        {
            for (int i = 0; i < Passive_skills_Warrior.Length; i++)
            {
                Passive_skills_Warrior[i] = i; //PlayerPrefs.GetInt(Str_Passive_skills_Warrior[i]);
            }
            for(int i = 0;i < Active_PassiveSkills.Length; i++)
            {
                Active_PassiveSkills[i] = -1; //PlayerPrefs.GetInt(Str_Active_PassiveSkills[i]);
            }
            for(int i = 0;  i< Active_skills_Warrior.Length; i++) //��������������������������������
            {
                Active_skills_Warrior[i] = true; //intToBool( PlayerPrefs.GetInt( Str_Active_skills_Warrior[i]));
            }

            AudioVolume = PlayerPrefs.GetFloat(Str_AudioVolume);

            _use = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}

