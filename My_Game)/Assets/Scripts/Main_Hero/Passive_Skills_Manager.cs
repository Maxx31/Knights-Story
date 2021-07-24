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


    private AudioSource _skillTakenSound;
    private AudioSource _buttonClickSound;
    [SerializeField, Header("1 Skill Take sound, 2 - Button Click sound")]
    private AudioClip[] _audio;
    private int total = 0;
    private int current_num = -1;

    private void Start()
    {
        if(Skills_Manager.use == null)
        {
            Debug.Log("Bad");
        }
        AudioSet();
        for (int i = 0; i< passive_skills_count; i++)
        {
            if(Singleton_Skills_Manager.use.Passive_skills_Warrior[i] != -1)
            {
                all[i] = Singleton_Skills_Manager.use.Passive_skills_Warrior[i];
                all_buttons[i].GetComponent<Image>().sprite = all_images[Singleton_Skills_Manager.use.Passive_skills_Warrior[i]];

                all_buttons[i].GetComponent<Image>().color = new Color(255, 255, 255);
            }
            else
            {
                all_buttons[i].GetComponent<Image>().sprite = _default;
                all[i] = Singleton_Skills_Manager.use.Passive_skills_Warrior[i];
            }
            Skills_Manager.use.Passive_skills_Warrior[i] = Singleton_Skills_Manager.use.Passive_skills_Warrior[i];
        }

       for(int i = 0; i< 3; i++)
        {
            if (Singleton_Skills_Manager.use.Active_PassiveSkills[i] != -1)
            {
                active[i] = Singleton_Skills_Manager.use.Active_PassiveSkills[i];
                Skills_Manager.use.Is_Enable_Passive_skills_Warrior[active[i]] = true;
                if (active[i] == 1)
                {
                    main_Hero.GetComponent<Main_Hero>().Move_Speed += 2.5f; //Check
                }
                else if (active[i] == 4)
                {
                    main_Hero.GetComponent<Main_Hero>().Max_health += 15f;
                }
                Active_Buttons[i].GetComponent<Image>().sprite = all_images[Singleton_Skills_Manager.use.Active_PassiveSkills[i]];
            }
            else
            {
                Active_Buttons[i].GetComponent<Image>().sprite = _default;
                active[i] = Singleton_Skills_Manager.use.Active_PassiveSkills[i];
            }
        }
    }
    public void Add_Skill(int ccount)
    {
        Debug.Log("Added");
        for (int i = 0; i < passive_skills_count; i++)
        {
            if (all[i] == -1)
            {
                _skillTakenSound.Play();
                all_buttons[i].GetComponent<Image>().sprite = all_images[ccount];
                all[i] = ccount;
                Singleton_Skills_Manager.use.Passive_skills_Warrior[i] = ccount;
                PlayerPrefs.SetInt(Singleton_Skills_Manager.use.Str_Passive_skills_Warrior[i], ccount);
                break;
            }
        }

    }
    public void All_Button(int ccount)
    {
        _buttonClickSound.Play();
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
        _buttonClickSound.Play();
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
                main_Hero.GetComponent<Main_Hero>().Move_Speed -= 2.5f;
            }
            else if (active[ccount] == 4)
            {
                main_Hero.GetComponent<Main_Hero>().Max_health -= 15f;
            }
        }

        int temp = active[ccount];
        active[ccount] = all[current_num];
        all[current_num] = temp;

        Singleton_Skills_Manager.use.Active_PassiveSkills[ccount] = active[ccount];
        Singleton_Skills_Manager.use.Passive_skills_Warrior[current_num] = all[current_num];

        PlayerPrefs.SetInt(Singleton_Skills_Manager.use.Str_Active_PassiveSkills[ccount], active[ccount]);
        PlayerPrefs.SetInt(Singleton_Skills_Manager.use.Str_Passive_skills_Warrior[current_num], all[current_num]);


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
        Singleton_Skills_Manager.use.Is_Enable_Passive_skills_Warrior[active[ccount]] = true;

        PlayerPrefs.SetInt(Singleton_Skills_Manager.use.Str_Is_Enable_Passive_skills_Warrior[active[ccount]],1);
        if (active[ccount] == 1)
        {
            main_Hero.GetComponent<Main_Hero>().Move_Speed += 2.5f; //Check
        }
        else if(active[ccount] == 4)
        {
            main_Hero.GetComponent<Main_Hero>().Max_health += 15f;
        }
    }

    public void Exit_Menu()
    {
        _buttonClickSound.Play();
        Time.timeScale = 1f;
        chosen_button_description.text = "";
        chosen_button.GetComponent<Image>().sprite = _default;
       if(current_num != -1)
        {
            all_buttons[current_num].GetComponent<Image>().color = new Color(255, 255, 255);
        }
        passive_menu.SetActive(false);

    }

    public void Exit_To_MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void Call_Menu()
    {
        _buttonClickSound.Play();
        Time.timeScale = 0f;
        passive_menu.SetActive(true);
        int total = 0;
        for(int i = 0; i< 3; i++)
        {
            if (active[i] != -1) total++;
        }
        if (total == 0)
        {
            Tutorial_Arrows.SetActive(true);
        }
        else
        {
            Tutorial_Arrows.SetActive(false);
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

    private void AudioSet()
    {
        _skillTakenSound = gameObject.AddComponent<AudioSource>();
        _skillTakenSound.playOnAwake = false;
        _skillTakenSound.clip = _audio[0];

        _buttonClickSound = gameObject.AddComponent<AudioSource>();
        _buttonClickSound.playOnAwake = false;
        _buttonClickSound.clip = _audio[1];

    }
}