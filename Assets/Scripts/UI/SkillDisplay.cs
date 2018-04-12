using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace S019745F
{
	public class SkillDisplay : MonoBehaviour
	{
		public Skills skill;
		// UI Vars
		public Text skillName;
		public Text skillDescription;
		public Image skillIcon;
		public Text skillLevel;
		public Text skillExpNeeded;
		public Text skillAttribute;
		public Text skillAttrAmount;

		[SerializeField]
		private PlayerStats playerStats;

		void Start()
		{
			playerStats = this.GetComponentInParent<PlayerStats>().playerStats;

			if (skill)
				skill.SetValues(this.gameObject, playerStats);
			EnableSkills();
		}

		public void EnableSkills()
		{
			// If the player has the skill already, show as enabled
			if(playerStats && skill && skill.EnableSkill(playerStats))
			{
				TurnOnSkillIcon();
			}
			else if(playerStats && skill && skill.CheckSkills(playerStats))
			{
				this.GetComponent<Button>().interactable = true;
				this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
			}
			else
			{
				TurnOffSkillIcon();
			}
		}

		private void OnEnable()
		{
			EnableSkills();
		}

		public void GetSkill()
		{
			if(skill.GetSkill(playerStats))
			{
				TurnOnSkillIcon();
			}
		}

		private void TurnOnSkillIcon()
		{
			this.GetComponent<Button>().interactable = false;
			this.transform.Find("IconParent").Find("Available").gameObject.SetActive(false);
			this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
		}

		private void TurnOffSkillIcon()
		{
			this.GetComponent<Button>().interactable = false;
			this.transform.Find("IconParent").Find("Available").gameObject.SetActive(true);
			this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(true);
		}
		private void OnDisable()
		{
			
		}
	}
}
