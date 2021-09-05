using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health_Bar : MonoBehaviour
{
    public Slider slider;
    public Vector3 Offset;

    [SerializeField]
    private Color Low;
    [SerializeField]
    private Color High;

    public void SetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);

        slider.value = health;
        slider.maxValue = maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);

    }
    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
