using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_Level : MonoBehaviour
{
    private AudioSource _teleportSound;


    private void Awake()
    {
        _teleportSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
        _teleportSound.Play();
        Level_Controller.Instanse.isEndGame();
        }
    }
}
