using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Level_Controller : MonoBehaviour
{
    public static Level_Controller Instanse = null;
    private int sceneIndex;
    private int levelComplete;
    void Start()
    {
        if(Instanse == null)
        {
            Instanse = this;
        }

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
    }


    public void isEndGame()
    {
        if(sceneIndex == 3)
        {
            Invoke("LoadMainMenu" , 1f);
        }
        else
        {
            if (levelComplete < sceneIndex)
            {
                PlayerPrefs.SetInt("LevelComplete", sceneIndex);
            }
            Invoke("NextLevel", 1f);
        }
    }
    
    void NextLevel()
    {
        SceneManager.LoadScene(4);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
