using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
	[HideInInspector]
	public bool Obstacle;

	public delegate void EndLevel();
	public event EndLevel EndLevelAction; //This event delete wall after boss die

	[SerializeField, Tooltip("Low hp color")]
	private Color _low;
	[SerializeField, Tooltip("High hp color")]
	private Color _high;
	[SerializeField]
	private float _maxHealth;


	private float currentHealth;
	private Slider slider;
	private Transform player;
	private bool isDead = false;
	private bool isFlipped = false;

	private Animator anim;
	private void Start()
	{
		Obstacle = false;
		anim = GetComponent<Animator>();
		slider = GameObject.Find("Boss Health bar").GetComponent<Slider>();
		slider.transform.localScale = new Vector3(2f, 4, 2);
		player = GameObject.FindGameObjectWithTag("Player").transform;
		currentHealth = _maxHealth;
		SetHealth(currentHealth, _maxHealth);
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "BossPlatform")
		{
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Main_Hero>() != null)
        {
			collision.gameObject.GetComponent<Main_Hero>().Mace_Damage(0.7f);
		}
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
		slider.gameObject.SetActive(health <= maxHealth);
		slider.maxValue = maxHealth;
		slider.value = health;
		slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(_low, _high, slider.normalizedValue);
	}

	public void TakeDamage(float damage)
    {
		if (!isDead)
		{
			if (currentHealth - damage <= _maxHealth / 1.8)
			{
				anim.SetBool("Rage", true);
			}

			if (currentHealth - damage <= 0)
			{
				EndLevelAction?.Invoke();
				isDead = true;
				anim.SetTrigger("Die");
			}

			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Rage"))
			{
				currentHealth -= damage;

				SetHealth(currentHealth, _maxHealth);
			}
		}
    }
}
