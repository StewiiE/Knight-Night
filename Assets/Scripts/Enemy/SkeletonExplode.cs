using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonExplode : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		foreach (Transform part in transform)
		{
			if(part.GetComponent<Rigidbody>() != null)
			{
				Rigidbody rb = part.GetComponent<Rigidbody>();
				rb.AddRelativeForce(-transform.up * 400);
			}
		}

		StartCoroutine(WaitToDestroy());
	}

	IEnumerator WaitToDestroy()
	{
		yield return new WaitForSeconds(5f);
		Destroy(gameObject);
	}
}
