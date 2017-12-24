using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int currentLevel;
    public int currentExp;
    public int[] toLevelUp;

    public int currentHealth;
    public int maxHealth;

	// Use this for initialization
	void Start ()
    {
        maxHealth = 100;
        currentHealth += maxHealth;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(currentExp >= toLevelUp[currentLevel])
        {
            currentLevel++;
        }
	}

    public void AddExperience(int experienceToAdd)
    {
        currentExp += experienceToAdd;
    }
}
