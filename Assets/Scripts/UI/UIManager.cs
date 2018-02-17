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

    bool onOff;

    Transform optionsPanel;
    Animator optionsAnim;

    Transform pausePanel;
    Animator pauseAnim;

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

        optionsPanel = this.transform.Find("OptionsPanel").transform;
        optionsAnim = optionsPanel.GetComponent<Animator>();

        pausePanel = this.transform.Find("PausePanel").transform;
        pauseAnim = pausePanel.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        levelText.text = "" + playerStats.currentLevel;
        healthText.text = "HP: " + playerStats.currentHealth + "/" + playerStats.maxHealth;
        healthbar.fillAmount = playerStats.currentHealth / 100f;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
          //  PausePanel();
        }
	}

    public void PausePanel()
    {
        onOff = !onOff;

        if (onOff)
        {
            optionsAnim.SetTrigger("Open");
            pauseAnim.SetTrigger("Open");

            Time.timeScale = 0f;

            Cursor.visible = true;
        }
        else
        {
            optionsAnim.SetTrigger("Close");
            pauseAnim.SetTrigger("Close");

            Time.timeScale = 1f;

            Cursor.visible = false;
        }
    }
}
