using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	[CreateAssetMenu(menuName = "RPG Generator/Player/Create Skill")]
	public class Skills : ScriptableObject
	{
		public string Description;
		public Sprite Icon;
		public int LevelNeeded;
		public int XPNeeded;

		public List<PlayerAttributes> AffectedAttributes = new List<PlayerAttributes>();

		// Public method to set the Skills UI
		public void SetValues(GameObject SkillDisplayObject, PlayerStats Player)
		{
			if (Player)
			{
				CheckSkills(Player);
			}

			// Check SO is used
			if (SkillDisplayObject)
			{
				SkillDisplay skillDisplay = SkillDisplayObject.GetComponent<SkillDisplay>();
				skillDisplay.skillName.text = name;
				if (skillDisplay.skillDescription)
					skillDisplay.skillDescription.text = Description;

				if (skillDisplay.skillIcon)
					skillDisplay.skillIcon.sprite = Icon;

				if (skillDisplay.skillLevel)
					skillDisplay.skillLevel.text = LevelNeeded.ToString();

				if (skillDisplay.skillExpNeeded)
					skillDisplay.skillExpNeeded.text = XPNeeded.ToString() + "XP";

				if (skillDisplay.skillAttribute)
					skillDisplay.skillAttribute.text = AffectedAttributes[0].attribute.ToString();

				if (skillDisplay.skillAttrAmount)
					skillDisplay.skillAttrAmount.text = "+" + AffectedAttributes[0].amount.ToString();
			}
		}

		// Check if player is able to get the skill
		public bool CheckSkills(PlayerStats player)
		{
			// check if player is right level
			if(player.currentLevel < LevelNeeded)
			{
				return false;
			}

			// Check if player has enough xp
			if(player.expPoints < XPNeeded)
			{
				return false;
			}

			// Otherwise they can enable this skill
			return true;
		}

		// Check if player has skill already
		public bool EnableSkill(PlayerStats player)
		{
			// Go through player's skills
			List<Skills>.Enumerator skills = player.PlayerSkills.GetEnumerator();
			while(skills.MoveNext())
			{
				var CurrSkill = skills.Current;
				if(CurrSkill.name == this.name)
				{
					return true;
				}
			}
			return false;
		}

		// Get new skill
		public bool GetSkill(PlayerStats player)
		{
			int i = 0;
			// List through the Skill's attributes
			List<PlayerAttributes>.Enumerator attributes = AffectedAttributes.GetEnumerator();
			while(attributes.MoveNext())
			{
				// List through the player's attributes and match skill with attribute
				List<PlayerAttributes>.Enumerator playerAttr = player.Attributes.GetEnumerator();
				while(playerAttr.MoveNext())
				{
					if(attributes.Current.attribute.name.ToString() == playerAttr.Current.attribute.name.ToString())
					{
						// Update players attributes
						playerAttr.Current.amount += attributes.Current.amount;
						// Mark attribute was updated
						i++;
					}
				}
				if(i>0)
				{
					// Reduce Exp Points from the player
					player.expPoints -= this.XPNeeded;
					// Add to the list of skills
					player.PlayerSkills.Add(this);
					return true;
				}
			}
			return false;
		}
	}
}

