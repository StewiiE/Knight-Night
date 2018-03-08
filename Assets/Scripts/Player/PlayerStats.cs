using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameObject player;
    public int currentLevel;
    public int currentExp;
    public int[] toLevelUp;

    public float currentHealth;
    public float maxHealth;

	public float currentMana;
	public float maxMana;
	float regenRate;

    Player playerScript;
    Abilities abilitiesScript;

    Animator playerAnimator;

    public GameObject levelUpPrefab;
    public GameObject levelUpPrefab2;

	// Use this for initialization
	void Start ()
    {
		currentLevel = 1;
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
	void Update ()
    {
		if(currentExp >= toLevelUp[currentLevel])
        {
            currentLevel++;

            // Do level up stuff

            GameObject particle1 =  Instantiate(levelUpPrefab, player.transform.position, Quaternion.identity) as GameObject;
            GameObject particle2 =  Instantiate(levelUpPrefab2, player.transform.position, Quaternion.identity) as GameObject;

            particle1.transform.parent = player.transform;
            particle2.transform.parent = player.transform;

            abilitiesScript.Explode();

            playerAnimator.Play("LevelUp", 0, 0.0f);
        }

        if(currentHealth <= 0)
        {
            playerScript.enabled = false;
            Death();
        }

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

		if(currentHealth < maxHealth)
		{
			currentHealth += regenRate * Time.deltaTime;
		}

		if(currentMana < maxMana)
		{
			currentMana += regenRate * Time.deltaTime;
		}
	}

    public void AddExperience(int experienceToAdd)
    {
        currentExp += experienceToAdd;
    }

    public void Death()
    {
        playerAnimator.SetBool("isDead", true);
        playerAnimator.Play("Death");
    }
}
