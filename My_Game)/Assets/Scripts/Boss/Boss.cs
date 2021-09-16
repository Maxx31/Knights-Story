using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{

	[SerializeField, Tooltip("Low hp color")]
	private Color _low;
	[SerializeField, Tooltip("High hp color")]
	private Color _high;
	[SerializeField]
	private Slider _slider;
	[SerializeField]
	private float _maxHealth;


	private float currentHealth;

	private Transform player;

	private bool isFlipped = false;
	private void Start()
	{
		_slider = GameObject.Find("Boss Health bar").GetComponent<Slider>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		currentHealth = _maxHealth;
		_slider.gameObject.SetActive(true);
	}


	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

	private void SetHealth(float health, float maxHealth)
	{

		_slider.gameObject.SetActive(health <= maxHealth);
		_slider.maxValue = maxHealth;
		_slider.value = health;
		_slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(_low, _high, _slider.normalizedValue);

	}

	public void TakeDamage(float damage)
    {
		currentHealth -= damage;

		SetHealth(currentHealth, _maxHealth);

    }
}
