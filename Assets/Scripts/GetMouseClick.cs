using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMouseClick : MonoBehaviour
 {
	public GameObject player;
	Player playerScript;
	Animator playerAnim;

	public GameObject levelUpPrefab;
	public GameObject levelUpPrefab2;

	bool toggle;

	void Start () 
	{
		playerScript = player.GetComponent<Player>();
		playerAnim = player.GetComponent<Animator>();
	}
	
	void Update ()
	{
		if(Input.GetMouseButton(0))
		{

			StartCoroutine(waitForParticles());
			playerAnim.Play("LevelUp", 0, 0.0f);
		}
		
		if(Input.GetKey(KeyCode.Space))
		{
			toggle = !toggle;
		}
		if (toggle)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	IEnumerator waitForParticles()
	{
		yield return new WaitForSeconds(0.25f);
		GameObject particle1 = Instantiate(levelUpPrefab, player.transform.position, Quaternion.identity) as GameObject;
		GameObject particle2 = Instantiate(levelUpPrefab2, player.transform.position - new Vector3(0, 2, 0), Quaternion.identity) as GameObject;

		particle1.transform.parent = player.transform;
		particle2.transform.parent = player.transform;

	}
}
