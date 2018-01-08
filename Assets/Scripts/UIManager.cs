using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text levelText;

    private PlayerStats playerStats;

    public Text healthText;
    public Image healthbar;

    private static bool UIExists;

	// Use this for initialization
	void Start ()
    {
		if(!UIExists)
        {
            UIExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerStats = GetComponent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        levelText.text = "" + playerStats.currentLevel;
        healthText.text = "HP: " + playerStats.currentHealth + "/" + playerStats.maxHealth;
        healthbar.fillAmount = playerStats.currentHealth / 100f;
	}
}
