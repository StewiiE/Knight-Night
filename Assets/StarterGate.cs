using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterGate : MonoBehaviour
{
	public bool gateTriggered = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other is CapsuleCollider)
		{
			if (other.gameObject.tag == "Player")
			{
				gateTriggered = true;
			}
		}
	}
}
