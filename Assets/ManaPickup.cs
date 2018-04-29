using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class ManaPickup : MonoBehaviour
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
			if (other is CapsuleCollider)
			{
				if (other.gameObject.tag == "Player")
				{
					playerStats.currentMana += playerStats.maxMana;
					Instantiate(pickupEffect, transform.position + new Vector3(0,3,0), Quaternion.identity);
					Destroy(this.gameObject);
				}
			}
		}
	}
}
