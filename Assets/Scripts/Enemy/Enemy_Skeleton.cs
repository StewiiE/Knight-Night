using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class Enemy_Skeleton : MonoBehaviour, IDamageable
	{
		public float currentHealth;
		float maxHealth = 100f;

		private PlayerStats thePlayerStats;
		public int expToGive;

		Rigidbody rb;
		Animator animator;
		Skeleton_Controller skeleton_Controller;

		GameObject player;
		Player playerScript;

		bool isDead = false;

		[SerializeField]
		GameManager gameManager;

		public GameObject skeletonPieces;

		// Use this for initialization
		void Start()
		{
			currentHealth = 25f;

			thePlayerStats = FindObjectOfType<PlayerStats>();

			rb = this.gameObject.GetComponent<Rigidbody>();
			animator = GetComponent<Animator>();

			player = PlayerManager.instance.player;
			playerScript = player.GetComponent<Player>();

			skeleton_Controller = GetComponent<Skeleton_Controller>();

			gameManager = FindObjectOfType<GameManager>();
		}

		// Update is called once per frame
		void Update()
		{
			if (currentHealth <= 0)
			{
				if (!isDead)
				{
					Death();
					isDead = true;
				}
			}
		}

		public void TakeDamage(float damage)
		{
			Debug.Log("Take Damage");
			currentHealth = currentHealth -= damage;
		}

		public void Death()
		{
			playerScript.Enemies.Remove(this.transform);

			gameManager.enemiesRemaining--;

			if (playerScript.isLevelingUp == false)
			{
				thePlayerStats.AddExperience(expToGive);
			}

			GameObject SkeletonPieces = Instantiate(skeletonPieces, transform.root.transform.position, Quaternion.identity) as GameObject;

			playerScript.kills++;

			Destroy(this.gameObject);
		}
	}
}
