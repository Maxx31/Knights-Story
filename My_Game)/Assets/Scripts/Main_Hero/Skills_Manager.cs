using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skills_Manager : MonoBehaviour
{
   public int[] Passive_skills_Warrior = new int[10];
    /*
    0 - Extra Damage 
    1 - Extra Move speed
    2 - Сritical damage
    3 - Evasion
    4 - Maximym HP increased
    5 - Armor penetration
    6 - Extra Armor
    7 - Double jump
    8 - FLAG(double jump, extra movespeed, extra armor)
     */
    public bool[] Is_Enable_Passive_skills_Warrior = new bool[10];

    public int[] Active_Button_Warrior = new int[3];

    public bool[] Active_skills_Warrior = new bool[3];

    [SerializeField]
    private List<Button> all_active_buttons;
    [SerializeField]
    private List<Sprite> all_active_images;

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

    public void Active_SKill_Set(int ccount)
    {
        all_active_buttons[ccount].GetComponent<Image>().sprite = all_active_images[ccount];
    }
}
