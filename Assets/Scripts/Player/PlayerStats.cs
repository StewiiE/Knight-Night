using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class PlayerStats : MonoBehaviour
	{
		public GameObject player;
		public PlayerStats playerStats;

		[Header("Levels")]
		public int currentLevel;
		public int currentExp;
		public int expPoints;
		public int[] toLevelUp;
		public int xpDiff;

		[Header("Stats")]
		public float currentHealth;
		public float maxHealth;
		public float currentMana;
		public float maxMana;
		float regenRate;

		Player playerScript;
		Abilities abilitiesScript;

		Animator playerAnimator;

		[Header("Particles")]
		public GameObject levelUpPrefab;
		public GameObject levelUpPrefab2;

		[Header("Attributes")]
		public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();

		[Header("Player Skills Enabled")]
		public List<Skills> PlayerSkills = new List<Skills>();

		// Use this for initialization
		void Start()
		{
			if (currentLevel == 0)
			{
				currentLevel = 1;
			}
			maxHealth = 100.0f;
			maxMana = 100.0f;
			regenRate = 1.0f;
			currentMana += maxMana;
			currentHealth += maxHealth;
			playerScript = player.GetComponent<Player>();
			playerAnimator = player.GetComponent<Animator>();
			abilitiesScript = player.GetComponent<Abilities>();
		}

		// Update is called once per frame
		void Update()
		{
			if (currentExp >= toLevelUp[currentLevel])
			{
				currentLevel++;

				// Do level up stuff

				GameObject particle1 = Instantiate(levelUpPrefab, player.transform.position, Quaternion.identity) as GameObject;
				GameObject particle2 = Instantiate(levelUpPrefab2, player.transform.position, Quaternion.identity) as GameObject;

				particle1.transform.parent = player.transform;
				particle2.transform.parent = player.transform;

				abilitiesScript.Explode();

				playerAnimator.Play("LevelUp", 0, 0.0f);
			}

			if (currentHealth <= 0)
			{
				playerScript.enabled = false;
				Death();
			}

			if (currentHealth > maxHealth)
			{
				currentHealth = maxHealth;
			}

			if (currentHealth < maxHealth)
			{
				currentHealth += regenRate * Time.deltaTime;
			}

			if (currentMana < maxMana)
			{
				currentMana += regenRate * Time.deltaTime;
			}

		//	xpDiff = toLevelUp[currentLevel] - currentExp;
		}

		public void AddExperience(int experienceToAdd)
		{
			currentExp += experienceToAdd;
			expPoints += experienceToAdd;
		}

		public void Death()
		{
			playerAnimator.SetBool("isDead", true);
			playerAnimator.Play("Death");
		}
	}
}


