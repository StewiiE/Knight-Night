using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class Abilities : MonoBehaviour
	{
		public float explosivePower = 10.0f;
		public float radius = 10.0f;
		public float upForce = 10.0f;

		public void Explode()
		{
			Vector3 explosionPos = gameObject.transform.position;
			Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

			foreach (Collider hit in colliders)
			{
				if (hit.gameObject != this.gameObject)
				{
					Rigidbody rb = hit.GetComponent<Rigidbody>();

					if (rb != null)
					{
						rb.AddExplosionForce(explosivePower, explosionPos, radius, upForce, ForceMode.Impulse);

						if (rb.tag == "Enemy")
						{
							Enemy_Paladin enemyScript = rb.gameObject.GetComponent<Enemy_Paladin>();
							enemyScript.TakeDamage(100f);
						}
					}
				}
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
	}
}

