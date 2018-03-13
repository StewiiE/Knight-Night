using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace S019745F
{
	[System.Serializable]
	public class PlayerAttributes
	{
		public Attributes attribute;
		public int amount;

		public PlayerAttributes(Attributes attribute, int amount)
		{
			this.attribute = attribute;
			this.amount = amount;
		}
	}
}
