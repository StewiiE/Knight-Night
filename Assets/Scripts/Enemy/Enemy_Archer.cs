using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class Enemy_Archer : MonoBehaviour, IDamageable
	{
		public float currentHealth;
		float maxHealth = 100f;

		private PlayerStats thePlayerStats;
		public int expToGive;

		Rigidbody rb;
		Animator animator;
		Enemy_Archer_Controller controller;

		GameObject player;
		Player playerScript;

		void Start()
		{
			currentHealth = 100f;

			thePlayerStats = FindObjectOfType<PlayerStats>();

			rb = this.gameObject.GetComponent<Rigidbody>();
			animator = GetComponent<Animator>();
			controller = GetComponent<Enemy_Archer_Controller>();

			player = PlayerManager.instance.player;
			playerScript = player.GetComponent<Player>();
		}

		void Update()
		{
			if (currentHealth <= 0)
			{
				Death();
			}
		}

		public void TakeDamage(float damage)
		{
			currentHealth = currentHealth -= damage;
			controller.canMove = false;
		}

		void Death()
		{
			Destroy(this.gameObject);

			if (playerScript.isLevelingUp == false)
			{
				thePlayerStats.AddExperience(expToGive);
			}
		}
	}
}
