using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class UpdateTargets : MonoBehaviour
	{
		GameObject player;
		Player playerScript;

		void Start()
		{
			player = PlayerManager.instance.player;
			playerScript = player.GetComponent<Player>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.tag == "Paladin")
			{
				GameObject enemy = other.gameObject;
				if (!playerScript.Enemies.Contains(enemy.transform))
				{
					playerScript.Enemies.Add(enemy.transform);
				}
			}
			else if (other.gameObject.tag == "Archer")
			{
				GameObject enemy = other.gameObject;
				if (!playerScript.Enemies.Contains(enemy.transform))
				{
					playerScript.Enemies.Add(enemy.transform);
				}
			}
			else if (other.gameObject.tag == "Skeleton")
			{
				GameObject enemy = other.gameObject;
				if (!playerScript.Enemies.Contains(enemy.transform))
				{
					playerScript.Enemies.Add(enemy.transform);
				}
			}
		}
		private void OnTriggerExit(Collider other)
		{
			if (playerScript.Enemies.Contains(other.transform))
			{
				playerScript.Enemies.Remove(other.transform);
			}
		}
	}
}

