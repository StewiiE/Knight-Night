using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class SecondGate : MonoBehaviour
	{
		public bool gateTriggered = false;
		public Arena1Manager arena1Manager;

		private void OnTriggerEnter(Collider other)
		{
			if (other is CapsuleCollider)
			{
				if (arena1Manager.canShowNextStats == true)
				{
					if (other.gameObject.tag == "Player")
					{
						gateTriggered = true;
					}
				}
			}
		}
	}
}
