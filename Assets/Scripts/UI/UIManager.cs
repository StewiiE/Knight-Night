using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace S019745F
{
	public class UIManager : MonoBehaviour
	{
		public Text levelText;

		private PlayerStats playerStats;

		public Text healthText;
		public Image healthbar;
		public Image manaBar;
		public Slider xpBar;
		public Text xpText;

		private static bool UIExists;

		bool onOff;

		Transform optionsPanel;
		Animator optionsAnim;

		Transform pausePanel;
		Animator pauseAnim;

		// Use this for initialization
		void Start()
		{
			if (!UIExists)
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
		void Update()
		{
			levelText.text = "" + playerStats.currentLevel;
			healthText.text = "HP: " + playerStats.currentHealth.ToString("F0") + "/" + playerStats.maxHealth.ToString("F0");
			healthbar.fillAmount = playerStats.currentHealth / 100.0f;
			manaBar.fillAmount = playerStats.currentMana / 100.0f;
			xpBar.maxValue = playerStats.toLevelUp[playerStats.currentLevel];
			xpBar.value = playerStats.currentExp;
			xpText.text = "XP: " + playerStats.currentExp + "/" + playerStats.toLevelUp[playerStats.currentLevel];

			if (Input.GetKeyDown(KeyCode.Escape))
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
}
