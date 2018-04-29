using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class Destructible : MonoBehaviour, IDamageable
	{
		public GameObject destroyedObject;

		public void TakeDamage(float damage)
		{
			Instantiate(destroyedObject, this.transform.position, this.transform.rotation);

			Destroy(this.gameObject);
		}
	}
}
