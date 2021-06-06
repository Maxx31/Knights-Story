using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_Level : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Level_Controller.Instanse.isEndGame();
    }
}
