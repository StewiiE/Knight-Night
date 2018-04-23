using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	[RequireComponent(typeof(AudioSource))]
	public class Footsteps : MonoBehaviour
	{
		Player player;
		Animator anim;


		private void Start()
		{
			player = GetComponent<Player>();
			anim = GetComponent<Animator>();
		}

		public void Step()
		{
			if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 || anim.GetCurrentAnimatorStateInfo(0).IsTag("SprintAttack"))
			{
				GetComponent<AudioSource>().Play();
			}
		}
	}
}

