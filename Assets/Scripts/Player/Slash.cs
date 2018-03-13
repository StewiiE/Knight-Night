using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class Slash : MonoBehaviour
	{

		Player player;
		TrailRenderer weaponTrail;

		// Use this for initialization
		void Start()
		{
			player = FindObjectOfType<Player>();
			weaponTrail = GetComponent<TrailRenderer>();
		}

		// Update is called once per frame
		void Update()
		{
			if (player.isSlashing == true)
			{
				weaponTrail.enabled = true;
				StartCoroutine(SlashEffectTime());
			}
			else if (player.isSlashing == false)
			{
				weaponTrail.enabled = false;
			}
		}

		IEnumerator SlashEffectTime()
		{
			if (player.isSlashing == true)
			{
				yield return new WaitForSeconds(0.6f);
				player.isSlashing = false;
			}
		}
	}
}

