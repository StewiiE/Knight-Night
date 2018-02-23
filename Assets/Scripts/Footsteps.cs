using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Footsteps : MonoBehaviour
{
	public void Step()
	{
		GetComponent<AudioSource>().Play();
		Debug.Log("asdasd");
	}
}
