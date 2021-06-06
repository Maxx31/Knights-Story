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
    void Start()
    {
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
        level2B.interactable = false;
        level3B.interactable = false;

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
        SceneManager.LoadScene(level);
    }

    public void Reset()
    {
        level2B.interactable = false;
        level3B.interactable = false;
        PlayerPrefs.DeleteAll();
    }
}
