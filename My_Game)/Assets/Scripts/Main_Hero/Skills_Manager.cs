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
    2 - Critical damage
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
    [SerializeField]
    private Sprite Default;

    private static Skills_Manager _use;
    public static Skills_Manager use
    {
        get
        {
                return _use;
            
        }
    }

    private void Start()
    {
        _use = this;
        for (int i = 0; i< 3; i++)
        {
            if(Singleton_Skills_Manager.use.Active_skills_Warrior[i] == true)
            {
                Active_skills_Warrior[i] = true;
                all_active_buttons[i].GetComponent<Image>().sprite = all_active_images[i];
            }
            else
            {
                Active_skills_Warrior[i] = false;
                all_active_buttons[i].GetComponent<Image>().sprite = Default;
            }
        }
    }

    public void Active_SKill_Set(int ccount)
    {
        Singleton_Skills_Manager.use.Active_skills_Warrior[ccount] = true;
        PlayerPrefs.SetInt(Singleton_Skills_Manager.use.Str_Active_skills_Warrior[ccount], 1);
        all_active_buttons[ccount].GetComponent<Image>().sprite = all_active_images[ccount];
    }
}
