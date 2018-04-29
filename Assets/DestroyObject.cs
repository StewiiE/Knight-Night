using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

	private AudioSource soundFX;
	public float amountOfTime = 4f;
	public float force = 400f;
	public bool canAddForce = true;

	private void Start()
	{
		soundFX = GetComponent<AudioSource>();

		soundFX.Play();

		foreach (Transform part in transform)
		{
			if (part.GetComponent<Rigidbody>() != null)
			{
				Rigidbody rb = part.GetComponent<Rigidbody>();
				if(canAddForce == true)
				{
					rb.AddRelativeForce(-transform.up * force);
				}
			}
		}

		StartCoroutine(Destroy());
	}

	IEnumerator Destroy()
	{
		yield return new WaitForSeconds(amountOfTime);
		Destroy(this.gameObject);
	}
}
