using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManaPickUp : MonoBehaviour
{
	public GameObject manaDrop;
	float dropRate = 0.33f;

	// Use this for initialization
	void Start()
	{
		if (Random.Range(0f, 1f) <= dropRate)
		{
			Instantiate(manaDrop, transform.position, Quaternion.identity);
		}
	}
}
