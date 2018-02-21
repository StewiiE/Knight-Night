using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameObject player;
    public int currentLevel;
    public int currentExp;
    public int[] toLevelUp;

    public int currentHealth;
    public int maxHealth;

    Player playerScript;
    Abilities abilitiesScript;

    Animator playerAnimator;

    public GameObject levelUpPrefab;
    public GameObject levelUpPrefab2;

	// Use this for initialization
	void Start ()
    {
        maxHealth = 100;
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
