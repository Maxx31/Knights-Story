using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{

    private Animator anim;

    [SerializeField]
    private GameObject _mainHero;
    private void Start()
    {
        _mainHero.GetComponent<Main_Hero>().HitAction += ScreenFade;
        anim = GetComponent<Animator>();
    }

    public void ScreenFade(bool Red = true)
    {
        if (Red)
        {
            anim.SetTrigger("Red"); 
        }
        else
        {
            anim.SetTrigger("Green");
        }
    }
}
