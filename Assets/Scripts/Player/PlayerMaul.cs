using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class PlayerMaul : MonoBehaviour
	{
		public float damage = 25f;

		Player player;

		GameObject paladinImpactSound;
		GameObject archerImpactSound;
		GameObject skeletonImpactSound;

		// Use this for initialization
		void Start()
		{
			player = FindObjectOfType<Player>();

			paladinImpactSound = transform.Find("MaulAudio/PaladinImpact").gameObject;
			archerImpactSound = transform.Find("MaulAudio/ArcherImpact").gameObject;
			skeletonImpactSound = transform.Find("MaulAudio/SkeletonImpact").gameObject;
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.GetComponent<IDamageable>() != null)
			{
				if (player.animIsAttacking == true)
				{
					if(other.gameObject.tag == "Paladin")
					{
						IDamageable enemy = other.GetComponent<IDamageable>();
						enemy.TakeDamage(damage);
						paladinImpactSound.GetComponent<AudioSource>().Play();
					}
					else if(other.gameObject.tag == "Archer")
					{
						IDamageable enemy = other.GetComponent<IDamageable>();
						enemy.TakeDamage(damage);
						archerImpactSound.GetComponent<AudioSource>().Play();
					}
					else if(other.gameObject.tag == "Skeleton")
					{
						Debug.Log("Hit skeleton");
						IDamageable enemy = other.GetComponent<IDamageable>();
						enemy.TakeDamage(damage);
						skeletonImpactSound.GetComponent<AudioSource>().Play();
					}
				}
			}
		}
	}
}

