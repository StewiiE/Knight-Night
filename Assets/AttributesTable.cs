using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace S019745F
{
	public class AttributesTable : MonoBehaviour
	{
		public GameObject attributesPanel;
		public PlayerStats playerStats;

		Transform strengthStat;
		Transform speedStat;
		Transform vitalityStat;
		Transform enduranceStat;
		Transform perceptionStat;
		Text strengthAmount;
		Text speedAmount;
		Text vitalityAmount;
		Text enduranceAmount;
		Text perceptionAmount;

		void Start()
		{
			strengthStat = transform.Find("CharacterAttributes/Strength").transform;
			strengthAmount = strengthStat.Find("StrengthAmount").transform.GetComponent<Text>();
			speedStat = transform.Find("CharacterAttributes/Speed").transform;
			speedAmount = speedStat.Find("SpeedAmount").transform.GetComponent<Text>();
			vitalityStat = transform.Find("CharacterAttributes/Vitality").transform;
			vitalityAmount = vitalityStat.Find("VitalityAmount").transform.GetComponent<Text>();
			enduranceStat = transform.Find("CharacterAttributes/Endurance").transform;
			enduranceAmount = enduranceStat.Find("EnduranceAmount").transform.GetComponent<Text>();
			perceptionStat = transform.Find("CharacterAttributes/Perception").transform;
			perceptionAmount = perceptionStat.Find("PerceptionAmount").transform.GetComponent<Text>();
		}

		void Update()
		{
			strengthAmount.text = playerStats.Attributes[0].amount.ToString();
			speedAmount.text = playerStats.Attributes[1].amount.ToString();
			enduranceAmount.text = playerStats.Attributes[2].amount.ToString();
			vitalityAmount.text = playerStats.Attributes[3].amount.ToString();
			perceptionAmount.text = playerStats.Attributes[4].amount.ToString();
		}
	}
}

