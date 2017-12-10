using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float currentHealth;
    float maxHealth = 100f;

    private PlayerStats thePlayerStats;
    public int expToGive;

	// Use this for initialization
	void Start ()
    {
        currentHealth = 100f;

        thePlayerStats = FindObjectOfType<PlayerStats>();
	}

    // Update is called once per frame
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
        Debug.Log(currentHealth);
    }

    public void Death()
    {
        Destroy(this.gameObject);

        thePlayerStats.AddExperience(expToGive);
    }
}
