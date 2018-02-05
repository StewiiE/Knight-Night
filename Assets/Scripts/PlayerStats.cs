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

    Animator playerAnimator;

	// Use this for initialization
	void Start ()
    {
        maxHealth = 100;
        currentHealth += maxHealth;
        playerScript = player.GetComponent<Player>();
        playerAnimator = player.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(currentExp >= toLevelUp[currentLevel])
        {
            currentLevel++;
        }

        if(currentHealth <= 0)
        {
            playerScript.enabled = false;
            Death();
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
