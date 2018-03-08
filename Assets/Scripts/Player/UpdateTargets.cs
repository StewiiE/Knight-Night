using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTargets : MonoBehaviour
 {
	GameObject player;
	Player playerScript;

	void Start () 
	{
		player = PlayerManager.instance.player;
		playerScript = player.GetComponent<Player>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Enemy")
		{
			GameObject enemy = other.gameObject;
			if(!playerScript.Enemies.Contains(enemy.transform))
			{
				playerScript.Enemies.Add(enemy.transform);
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if(playerScript.Enemies.Contains(other.transform))
		{
			playerScript.Enemies.Remove(other.transform);
		}
	}
}
