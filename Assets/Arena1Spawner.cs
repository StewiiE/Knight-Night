using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena1Spawner : MonoBehaviour
{
	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	[System.Serializable]
	public class Wave
	{
		public string name;
		public GameObject enemy;                // The enemy prefab to be spawned.
		public int amount;
		public float rate;
	}
	public Wave[] waves;
	public int nextWave = 0;

	public Transform[] spawnPoints;

	public float timeBetweenWaves = 5f;
	public float waveCountdown;

	private float searchCountdown = 1f;

	public SpawnState state = SpawnState.COUNTING;

	public bool startWave = false;

	int currentLevel = 1;

	public bool canShowStats = false;

	public int enemiesAlive = 0;

	public GameObject exitGate;

	void Start()
	{
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points selected");
		}

		waveCountdown = timeBetweenWaves;
	}

	private void Update()
	{
		if (state == SpawnState.WAITING)
		{
			if (!EnemyIsAlive())
			{
				WaveCompleted();
			}
			else
			{
				return;
			}
		}

		if (startWave == true)
		{
			if (waveCountdown <= 0)
			{
				if (state != SpawnState.SPAWNING)
				{
					StartCoroutine(SpawnWave(waves[nextWave]));
				}
			}
			else
			{
				waveCountdown -= Time.deltaTime;
			}
		}
	}

	void WaveCompleted()
	{
		// Start new round/level
		Debug.Log("Wave Complete");

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		if (nextWave + 1 > waves.Length - 1)
		{
			exitGate.SetActive(false);

			// Waves Complete!

			canShowStats = false;
		}
		else
		{
			nextWave++;
		}
	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (enemiesAlive == 0)
			{
				return false;
			}
		}
		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		state = SpawnState.SPAWNING;

		for (int i = 0; i < _wave.amount; i++)
		{
			Spawn(_wave.enemy);
			yield return new WaitForSeconds(_wave.rate);
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void Spawn(GameObject _enemy)
	{
		Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
		Instantiate(_enemy, spawnPoint.position, Quaternion.identity);
		enemiesAlive++;
	}
}
