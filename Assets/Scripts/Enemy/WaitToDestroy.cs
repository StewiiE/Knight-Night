using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitToDestroy : MonoBehaviour
{
    public float amountOfTime = 4f;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Destroy());
	}

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(amountOfTime);
        Destroy(this.gameObject);
    }
}
