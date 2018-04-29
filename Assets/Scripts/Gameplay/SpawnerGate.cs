using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGate : MonoBehaviour
{
	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag != "Player")
		{
			Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), other.collider);
		}
	}
}
