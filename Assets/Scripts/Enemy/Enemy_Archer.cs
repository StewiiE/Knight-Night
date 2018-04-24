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

		bool isDead = false;

		[SerializeField]
		Arena1Spawner spawnerRef;

		void Start()
		{
			currentHealth = 100f;

			thePlayerStats = FindObjectOfType<PlayerStats>();

			rb = this.gameObject.GetComponent<Rigidbody>();
			animator = GetComponent<Animator>();
			controller = GetComponent<Enemy_Archer_Controller>();

			player = PlayerManager.instance.player;
			playerScript = player.GetComponent<Player>();

			spawnerRef = FindObjectOfType<Arena1Spawner>();
		}

		void Update()
		{
			if (currentHealth <= 0)
			{
				if(!isDead)
				{
					Death();
					isDead = true;
				}
			}
		}

		public void TakeDamage(float damage)
		{
			currentHealth = currentHealth -= damage;
			controller.canMove = false;
			animator.Play("standing react small from headshot");
		}

		void Death()
		{
			controller.enabled = false;

			rb.freezeRotation = true;

			animator.Play("standing death backward 01");

			if (playerScript.isLevelingUp == false)
			{
				thePlayerStats.AddExperience(expToGive);
			}

			if (spawnerRef.enemyList.Contains(this.gameObject))
			{
				spawnerRef.RemoveEnemyFromList(this.gameObject);
			}

			StartCoroutine(waitToDestroy());
		}

		IEnumerator waitToDestroy()
		{
			yield return new WaitForSeconds(5f);
			Destroy(this.gameObject);
		}
	}
}
