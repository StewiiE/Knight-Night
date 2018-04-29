using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class HealthPickup : MonoBehaviour
	{
		GameObject player;
		Player playerScript;

		private PlayerStats playerStats;

		public GameObject pickupEffect;

		// Use this for initialization
		void Start()
		{
			player = PlayerManager.instance.player;
			playerScript = player.GetComponent<Player>();
			playerStats = FindObjectOfType<PlayerStats>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other is CapsuleCollider)
			{
				if (other.gameObject.tag == "Player")
				{
					playerStats.currentHealth += playerStats.maxHealth;
					Instantiate(pickupEffect, transform.position, Quaternion.identity);
					Destroy(this.gameObject);
				}
			}
		}
	}
}
