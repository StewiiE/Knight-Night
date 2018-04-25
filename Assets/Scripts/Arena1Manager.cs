using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Arena1Manager : MonoBehaviour
{
	bool doOnce = true;

	public GameObject arenaPopup;

	Arena1Spawner arena1Spawner;

	public CanvasGroup waveStats;

	// Wave Vars
	public Text waveNumber;
	public Text waveCountdown;
	public Text enemyType;
	public Text enemiesRemaining;

	float newWaveCountdown;
	int currentWave;

	private void Start()
	{
		arena1Spawner = GetComponent<Arena1Spawner>();
	}

	private void LateUpdate()
	{
		if (arena1Spawner.canShowStats == true)
		{
			currentWave = arena1Spawner.nextWave + 1;

			waveStats.alpha = 1;

			newWaveCountdown = arena1Spawner.waveCountdown;

			if (newWaveCountdown < 0)
			{
				newWaveCountdown = 0;
			}

			waveNumber.text = "Wave " + currentWave.ToString();
			waveCountdown.text = newWaveCountdown.ToString("F2");
			enemyType.text = arena1Spawner.waves[arena1Spawner.nextWave].enemy.name.ToString();
			enemiesRemaining.text = arena1Spawner.enemiesAlive.ToString();
		}
		else
		{
			waveStats.alpha = 0;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (doOnce)
		{
			if (other.gameObject.tag == "Player")
			{
				StartCoroutine(removePopup());
				doOnce = false;
				Debug.Log("Start Arena!");
				arena1Spawner.startWave = true;
				arena1Spawner.canShowStats = true;
			}
		}
	}

	IEnumerator removePopup()
	{
		arenaPopup.SetActive(true);
		yield return new WaitForSeconds(2f);
		arenaPopup.SetActive(false);
	}
}
