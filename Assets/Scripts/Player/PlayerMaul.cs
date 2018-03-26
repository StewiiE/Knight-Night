using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class PlayerMaul : MonoBehaviour
	{
		public float damage = 25f;

		Player player;

		GameObject bodyImpactGO;

		// Use this for initialization
		void Start()
		{
			player = FindObjectOfType<Player>();

			bodyImpactGO = transform.Find("MaulAudio/BodyImpact").gameObject;
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.tag == "Enemy")
			{
				if (player.animIsAttacking == true)
				{
					IDamageable enemy = other.GetComponent<IDamageable>();
					enemy.TakeDamage(damage);
					bodyImpactGO.GetComponent<AudioSource>().Play();
				}
			}
		}
	}
}

