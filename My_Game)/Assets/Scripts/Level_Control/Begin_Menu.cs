using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Begin_Menu: MonoBehaviour
{
    public void LoatTo(int level)
    {
        SceneManager.LoadScene(level);
    }
}
