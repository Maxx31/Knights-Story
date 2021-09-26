using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    [SerializeField]
    private Button level2B;
    [SerializeField]
    private Button level3B;
    private int levelComplete;
    AudioSource click;
    void Start()
    {
        click = gameObject.GetComponent<AudioSource>();
        //TEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEMMP
        PlayerPrefs.SetInt("LevelComplete" , 2);
        //TEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEMMP
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
        
        level2B.interactable = false;
        level3B.interactable = false;

        levelComplete = Mathf.Min(levelComplete, 2);
        switch (levelComplete)
        {
            case 1:
                level2B.interactable = true;
                break;
            case 2:
                level2B.interactable = true;
                level3B.interactable = true; 
                break;

        }
    }

    public void LoatTo(int level)
    {
        click.Play();
        SceneManager.LoadScene(level);
    }

    public void Reset()
    {
        click.Play();
        for (int i = 0; i < Singleton_Skills_Manager.use.Str_Passive_skills_Warrior.Length; i++)
        {
            PlayerPrefs.SetInt(Singleton_Skills_Manager.use.Str_Passive_skills_Warrior[i], -1);
        }

        for (int i = 0; i < Singleton_Skills_Manager.use.Str_Active_PassiveSkills.Length; i++)
        {
            PlayerPrefs.SetInt(Singleton_Skills_Manager.use.Str_Active_PassiveSkills[i], -1);
        }

        for (int i = 0; i < Singleton_Skills_Manager.use.Str_Active_skills_Warrior.Length; i++)
        {
            PlayerPrefs.SetInt(Singleton_Skills_Manager.use.Str_Active_skills_Warrior[i], 0);
        }

        for (int i = 0; i < Singleton_Skills_Manager.use.Passive_skills_Warrior.Length; i++)
        {
            Singleton_Skills_Manager.use.Passive_skills_Warrior[i] = PlayerPrefs.GetInt(Singleton_Skills_Manager.use.Str_Passive_skills_Warrior[i]);
        }

        for (int i = 0; i < Singleton_Skills_Manager.use.Active_PassiveSkills.Length; i++)
        {
            Singleton_Skills_Manager.use.Active_PassiveSkills[i] = PlayerPrefs.GetInt(Singleton_Skills_Manager.use.Str_Active_PassiveSkills[i]);
        }

        for (int i = 0; i < Singleton_Skills_Manager.use.Active_skills_Warrior.Length; i++)
        {
            Singleton_Skills_Manager.use.Active_skills_Warrior[i] = intToBool(PlayerPrefs.GetInt(Singleton_Skills_Manager.use.Str_Active_skills_Warrior[i]));
        }
        level2B.interactable = false;
        level3B.interactable = false;
        PlayerPrefs.SetInt("LevelComplete", 0);
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
