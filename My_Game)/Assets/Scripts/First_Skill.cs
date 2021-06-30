using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class First_Skill : MonoBehaviour
{
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private GameObject manager;

    [SerializeField]
    private SpriteRenderer sprite_to_enable;
    [SerializeField]
    private Text text_to_enable;
    private float timeToAppear = 3f;
    private float timeWhenDisappear;
    private Passive_Skills_Manager _manager;
    private bool is_taken = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < 9; i++)
        {
            if (Singleton_Skills_Manager.use.Passive_skills_Warrior[i] == 0) is_taken = true;
        }

        for (int i = 0; i < 3; i++)
        {
            if (Singleton_Skills_Manager.use.Active_PassiveSkills[i] == 0) is_taken = true;
        }
        if (collision.CompareTag("Player") && is_taken == false)
        {
            {
                arrow.SetActive(true);
                timeWhenDisappear = Time.time + timeToAppear;
                _manager.Add_Skill(0);

                sprite_to_enable.sprite = null;
                text_to_enable.text = "";
            }
            is_taken = true;
        }
    }

    private void Start()
    {
        _manager = manager.GetComponent<Passive_Skills_Manager>();
    }
    private void Update()
    {
        if(Time.time >= timeWhenDisappear && arrow.activeSelf)
        {
            arrow.SetActive(false);
            Destroy(this.gameObject);
        }
        
    }
}
