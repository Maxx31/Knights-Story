using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills_Manager : MonoBehaviour
{
   public int[] Passive_skills_Warrior = new int[10]; //Первое умение - доп. урон, Второе умение - доп. скорость, третье умение - критический урон
    public bool[] Is_Enable_Passive_skills_Warrior = new bool[10];
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

  
       // Passive_skills_Warrior[1] = 3;

        if (_use == null)
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
