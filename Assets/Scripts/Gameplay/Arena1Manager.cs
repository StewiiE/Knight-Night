using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace S019745F
{
	public class Arena1Manager : MonoBehaviour
	{
		bool doOnce = true;
		public bool checkEndWaves = false;

		public GameObject arenaPopup;

		Arena1Spawner arena1Spawner;

		public GameObject waveStats;

		// Wave Vars
		public Text waveNumber;
		public Text waveCountdown;
		public Text enemyType;
		public Text enemiesRemaining;
		public Text arenaText;

		float newWaveCountdown;
		int currentWave;
		float waitTime = 2f;

		public GameManager gameManager;

		public bool canShowNextStats = false;

		private void Start()
		{
			arena1Spawner = GetComponent<Arena1Spawner>();

			checkEndWaves = false;
		}

		private void LateUpdate()
		{
			if (arena1Spawner.canShowStats == true)
			{
				currentWave = arena1Spawner.nextWave + 1;

				waveStats.SetActive(true);

				newWaveCountdown = arena1Spawner.waveCountdown;

				if (newWaveCountdown < 0)
				{
					newWaveCountdown = 0;
				}

				waveNumber.text = "Wave " + currentWave.ToString();
				waveCountdown.text = newWaveCountdown.ToString("F2");
				enemyType.text = arena1Spawner.waves[arena1Spawner.nextWave].enemy.name.ToString();
				enemiesRemaining.text = gameManager.enemiesRemaining.ToString();
			}
			else
			{
				waveStats.SetActive(false);
			}

			if (checkEndWaves == true)
			{
				Debug.Log(checkEndWaves);
				gameManager.arenasCompleted++;
				arenaText.text = "Stage Complete!";
				arenaPopup.SetActive(true);
				StartCoroutine(removePopup());
				checkEndWaves = false;
				canShowNextStats = true;
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (doOnce)
			{
				if (other.gameObject.tag == "Player")
				{
					arenaPopup.SetActive(true);
					StartCoroutine(removePopup());
					arena1Spawner.canShowStats = true;
					Debug.Log("Start Arena!");
					arena1Spawner.startWave = true;
					doOnce = false;
				}
			}
		}

		IEnumerator removePopup()
		{
			Debug.Log("sascscs");
			yield return new WaitForSeconds(2f);
			arenaPopup.SetActive(false);
			yield break;
		}
	}
}
