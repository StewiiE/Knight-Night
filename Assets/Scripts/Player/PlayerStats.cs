using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class PlayerStats : MonoBehaviour
	{
		public GameObject player;
		public PlayerStats playerStats;
		public CanvasGroup deathPanel;

		[Header("HUD")]
	//	public List<GameObject> HUDList = new List<GameObject>();

		[Header("Respawn HUD")]
	//	public List<GameObject> RespawnHUDList = new List<GameObject>();

		[Header("Levels")]
		public int currentLevel;
		public int currentExp;
		public int expPoints;
		public int[] toLevelUp;

		[Header("Stats")]
		public float currentHealth;
		public float maxHealth;
		public float currentMana;
		public float maxMana;
		float regenRate;

		Player playerScript;
		Abilities abilitiesScript;
		Animator playerAnimator;

		private float animSpeedMultiplier;

		int strength;
		int speed;
		int endurance;
		int vitality;
		int perception;

		[Header("Particles")]
		public GameObject levelUpPrefab;
		public GameObject levelUpPrefab2;

		[Header("Attributes")]
		public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();

		[Header("Player Skills Enabled")]
		public List<Skills> PlayerSkills = new List<Skills>();

		bool showDeathScreen = false;

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

			animSpeedMultiplier = playerAnimator.GetFloat("movementMultiplier");

			// Attributes setup
			strength = Attributes[0].amount;
			speed = Attributes[1].amount;
			endurance = Attributes[2].amount;
			vitality = Attributes[3].amount;
			perception = Attributes[4].amount;
		}

		// Update is called once per frame
		void Update()
		{
			if (currentExp >= toLevelUp[currentLevel])
			{
				currentLevel++;

				// Do level up stuff

				playerAnimator.Play("LevelUp");

				GameObject particle1 = Instantiate(levelUpPrefab, player.transform.position, Quaternion.identity) as GameObject;
				GameObject particle2 = Instantiate(levelUpPrefab2, player.transform.position, Quaternion.identity) as GameObject;

				particle1.transform.parent = player.transform;
				particle2.transform.parent = player.transform;

				abilitiesScript.Explode();
			}

			if (currentHealth <= 0)
			{
				playerScript.enabled = false;
				Death();
			}
			else
			{
				/*foreach (GameObject item in RespawnHUDList)
				{
					item.SetActive(true);
					Debug.Log("sartyr");
				}

				playerScript.enabled = true;

				showDeathScreen = false;

				deathPanel.alpha = 0f;
				deathPanel.interactable = false;
				deathPanel.blocksRaycasts = false;
				Time.timeScale = 1f;
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked; */
			}

			if (currentHealth > maxHealth)
			{
				currentHealth = maxHealth;
			}

			if(player != null)
			{
				if (playerScript.enabled == true)
				{
					if (currentHealth < maxHealth)
					{
						currentHealth += regenRate * Time.deltaTime;
					}

					if (currentMana < maxMana)
					{
						currentMana += regenRate * Time.deltaTime;
					}
				}

				// Movement Multiplier
				playerAnimator.SetFloat("movementMultiplier", 1 + (speed / 100.0f));
			}
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

			/*foreach (GameObject item in HUDList)
			{
				item.SetActive(false);
			} */

			showDeathScreen = true;

			StartCoroutine(WaitToShowPanel());
		}

		IEnumerator WaitToShowPanel()
		{
			yield return new WaitForSeconds(2.5f);

			if(showDeathScreen == true)
			{
				deathPanel.alpha = 1f;
				deathPanel.interactable = true;
				deathPanel.blocksRaycasts = true;
				Time.timeScale = 0f;
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
			}
		}
	}
}


