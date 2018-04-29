using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHealthPickUp : MonoBehaviour
{

	public GameObject healthDrop;
	float dropRate = 0.33f;

	// Use this for initialization
	void Start()
	{
		if (Random.Range(0f, 1f) <= dropRate)
		{
			Instantiate(healthDrop, transform.position, Quaternion.identity);
		}
	}
}
