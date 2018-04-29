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

		public GameObject bloodEffect;

		Transform maulEffectPos;

		// Use this for initialization
		void Start()
		{
			player = FindObjectOfType<Player>();

			paladinImpactSound = transform.Find("MaulAudio/PaladinImpact").gameObject;
			archerImpactSound = transform.Find("MaulAudio/ArcherImpact").gameObject;
			skeletonImpactSound = transform.Find("MaulAudio/SkeletonImpact").gameObject;

			maulEffectPos = transform.Find("MaulHitEffects").transform;
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
						Instantiate(bloodEffect, maulEffectPos.transform.position, maulEffectPos.transform.rotation);
					}
					else if(other.gameObject.tag == "Archer")
					{
						IDamageable enemy = other.GetComponent<IDamageable>();
						enemy.TakeDamage(damage);
						archerImpactSound.GetComponent<AudioSource>().Play();
						Instantiate(bloodEffect, maulEffectPos.transform.position, maulEffectPos.transform.rotation);
					}
					else if(other.gameObject.tag == "Skeleton")
					{
						IDamageable enemy = other.GetComponent<IDamageable>();
						enemy.TakeDamage(damage);
						skeletonImpactSound.GetComponent<AudioSource>().Play();
					}
					else if(other.gameObject.tag == "Destructible")
					{
						IDamageable item = other.GetComponent<IDamageable>();
						item.TakeDamage(damage);
					}
				}
			}
		}
	}
}

