using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
public class Passive_Skills_Manager : MonoBehaviour
{
    private List<int> all = Enumerable.Repeat(-1, passive_skills_count).ToList();
    private List<int> active = Enumerable.Repeat(-1, 3).ToList();
    [SerializeField]
    private Sprite _default;
    [SerializeField]
    private const int passive_skills_count = 9;
    [SerializeField]
    private List<Button> all_buttons;
    [SerializeField]
    private List<Sprite> all_images;
    [SerializeField]
    private List<Button> Active_Buttons;
    [SerializeField]
    private Button chosen_button;
    [SerializeField]
    private Text chosen_button_description; 
    [SerializeField]
    private GameObject passive_menu; 
    [SerializeField]
    private GameObject main_Hero;

    [SerializeField]
    private GameObject Tutorial_Arrows;

    private int total = 0;
    private int current_num = -1;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
       
    }
    public void Add_Skill(int ccount)
    {
        for (int i = 0; i < passive_skills_count; i++)
        {
            if (all[i] == -1)
            {
                all_buttons[i].GetComponent<Image>().sprite = all_images[ccount];
                all[i] = ccount;
                break;
            }
        }

    }
    public void All_Button(int ccount)
    {
        if (all[ccount] == -1) return;
        if (current_num != -1)
        {
            all_buttons[current_num].GetComponent<Image>().color = new Color(255, 255, 255);
        }
        all_buttons[ccount].GetComponent<Image>().color = new Color(255, 250, 0);
        current_num = ccount;
        chosen_button.GetComponent<Image>().sprite = all_images[all[ccount]];
        SetDescription(all[ccount]);
       
    }
    public void Active_Button(int ccount)
    {
        if (current_num == -1)
        {
            if(active[ccount] != -1)
            {
                chosen_button.GetComponent<Image>().sprite = all_images[active[ccount]];
                SetDescription(active[ccount]);
            }
            return;
        }
        if (active[ccount] != -1)
        {
            Skills_Manager.use.Is_Enable_Passive_skills_Warrior[active[ccount]] = false;
            if(active[ccount] == 1)
            {
                main_Hero.GetComponent<Main_Hero>().Move_Speed -= 5f;
            }
            else if (active[ccount] == 4)
            {
                main_Hero.GetComponent<Main_Hero>().Max_health -= 15f;
            }
        }

        int temp = active[ccount];
        active[ccount] = all[current_num];
        all[current_num] = temp;
        Active_Buttons[ccount].GetComponent<Image>().sprite = all_images[active[ccount]];
        if (all[current_num] == -1)
        {
            all_buttons[current_num].GetComponent<Image>().sprite = _default;
        }
        else
        {
            all_buttons[current_num].GetComponent<Image>().sprite = all_images[all[current_num]];
        }

        all_buttons[current_num].GetComponent<Image>().color = new Color(255, 255, 255);
        current_num = -1;

        Skills_Manager.use.Is_Enable_Passive_skills_Warrior[active[ccount]] = true;
        if (active[ccount] == 1)
        {
            main_Hero.GetComponent<Main_Hero>().Move_Speed += 5f;
        }
        else if(active[ccount] == 4)
        {
            main_Hero.GetComponent<Main_Hero>().Max_health += 15f;
        }
    }

    public void Exit_Menu()
    {
        Time.timeScale = 1f;
        chosen_button_description.text = "";
        chosen_button.GetComponent<Image>().sprite = _default;
       if(current_num != -1)
        {
            all_buttons[current_num].GetComponent<Image>().color = new Color(255, 255, 255);
        }
        passive_menu.SetActive(false);

        if (total == 0)
        {
            Tutorial_Arrows.SetActive(true);
        }
    }

    public void Call_Menu()
    {
        Time.timeScale = 0f;
        passive_menu.SetActive(true);
        int total = 0;
        for(int i = 0; i< 3; i++)
        {
            if (active[i] != -1) total++;
        }
        if(total == 0)
        {
            Tutorial_Arrows.SetActive(true);
        }
    }

    public void SetDescription(int ccount)
    {
        switch (ccount)
        {
            case 0:
                chosen_button_description.text = "Increases normal \n attack damage";
                break;
            case 1:
                chosen_button_description.text = "Increases normal \n move speed";
                break;
            case 2:
                chosen_button_description.text = "Add critical  \n damage chance";
                break;
            case 3:
                chosen_button_description.text = "Add evasion \n chance ";
                break;
            case 4:
                chosen_button_description.text = "Increases maximym \n HP amount";
                break;
            case 5:
                chosen_button_description.text = "Increases armor \n penetration";
                break;
            case 6:
                chosen_button_description.text = "Add extra \n armor to you";
                break;
            case 7:
                chosen_button_description.text = "Add double \n jump ability";
                break;
            case 8:
                chosen_button_description.text = "Add double jump, extra movement \n and extra armor";
                break;
        }
    }

}