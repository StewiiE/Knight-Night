using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class ArrowProjectile : MonoBehaviour
	{
		Rigidbody rb;
		GameObject trail;

		PlayerStats playerStats;
		GameObject player;
		Player playerScript;

		public float speed = 1f;
		public float range = 2f;

		AudioSource hitSound;

		void Start()
		{
			rb = GetComponent<Rigidbody>();
			trail = transform.Find("Trail").gameObject;
			playerStats = FindObjectOfType<PlayerStats>();
			player = PlayerManager.instance.player;
			playerScript = player.GetComponent<Player>();
			hitSound = GetComponent<AudioSource>();

			rb.AddForce(transform.forward * speed, ForceMode.Impulse);
		}

		private void OnCollisionEnter(Collision other)
		{
			this.GetComponent<Collider>().enabled = false;

			if(other.GetType() != typeof(SphereCollider))
			{
				rb.isKinematic = true;
				this.enabled = false;
				trail.SetActive(false);
				transform.position += transform.forward * 1.25f;

				if (other.gameObject.tag == "Player")
				{
					transform.parent = other.transform.Find("hips");
					playerScript.DoHit();
					if (playerScript.isBlocking == false)
					{
						hitSound.Play();
						playerStats.currentHealth -= 10;
					}
					else if (playerScript.isBlocking == true)
					{
						// Do deflection
					}
				}
				else
				{
					transform.parent = other.transform;
				}
			}
		}
	}
}

