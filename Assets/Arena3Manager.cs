using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace S019745F
{
	public class Arena3Manager : MonoBehaviour
	{
		bool doOnce = true;
		public bool checkEndWaves = false;

		public GameObject arenaPopup;

		Arena3Spawner arena3Spawner;

		public Arena2Manager arena2Manager;

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

		private void Start()
		{
			arena3Spawner = GetComponent<Arena3Spawner>();

			checkEndWaves = false;
		}

		private void LateUpdate()
		{
			if (arena2Manager.canShowNextStats == true)
			{
				if (arena3Spawner.canShowStats == true)
				{
					currentWave = arena3Spawner.nextWave + 1;

					waveStats.SetActive(true);

					newWaveCountdown = arena3Spawner.waveCountdown;

					if (newWaveCountdown < 0)
					{
						newWaveCountdown = 0;
					}

					waveNumber.text = "Wave " + currentWave.ToString();
					waveCountdown.text = newWaveCountdown.ToString("F2");
					enemyType.text = arena3Spawner.waves[arena3Spawner.nextWave].enemy.name.ToString();
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
				}
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (doOnce)
			{
				if (other.gameObject.tag == "Player")
				{
					arenaText.text = "Final Arena";
					arenaPopup.SetActive(true);
					StartCoroutine(removePopup());
					doOnce = false;
					Debug.Log("Start Arena!");
					arena3Spawner.startWave = true;
					arena3Spawner.canShowStats = true;
				}
			}
		}

		IEnumerator removePopup()
		{
			yield return new WaitForSeconds(2f);
			arenaPopup.SetActive(false);
			yield break;
		}
	}
}
