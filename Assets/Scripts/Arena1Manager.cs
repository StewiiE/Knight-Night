using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena1Manager : MonoBehaviour
{
	bool doOnce = true;

	public GameObject arenaPopup;

	Arena1Spawner arena1Spawner;

	private void Start()
	{
		arena1Spawner = GetComponent<Arena1Spawner>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(doOnce)
		{
			if (other.gameObject.tag == "Player")
			{
				StartCoroutine(removePopup());
				doOnce = false;
				Debug.Log("Start Arena!");
				arena1Spawner.startWave = true;
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
