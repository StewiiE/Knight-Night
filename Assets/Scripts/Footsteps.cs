using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Footsteps : MonoBehaviour
{
	Player player;


	private void Start()
	{
		player = GetComponent<Player>();
	}

	public void Step()
	{
		if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
		{
			GetComponent<AudioSource>().Play();
		}
	}
}
