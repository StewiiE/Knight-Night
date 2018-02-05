using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    GameObject player;
    Player playerScript;

	// Use this for initialization
	void Start ()
    {
        player = PlayerManager.instance.player;
        playerScript = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");

            playerScript.DoHit();
        }
    }
}
