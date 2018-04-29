using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace S019745F
{
	public class DeathScreenManager : MonoBehaviour
	{
		GameObject player;
		Player playerScript;
		private PlayerStats playerStats;

		public Text scores;
		public GameManager gameManager;

		int score;

		// Use this for initialization
		void Start()
		{
			player = PlayerManager.instance.player;
			playerScript = player.GetComponent<Player>();
			playerStats = FindObjectOfType<PlayerStats>();
		}

		// Update is called once per frame
		void Update()
		{
			score = playerScript.kills * 13 / 3;

			scores.text = score + "\n" + playerScript.kills + "\n" + gameManager.arenasCompleted;
		}
	}
}
