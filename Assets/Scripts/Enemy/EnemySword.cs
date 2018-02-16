﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    GameObject player;
    Player playerScript;

    public bool hitPlayer = false;

	// Use this for initialization
	void Start ()
    {
        player = PlayerManager.instance.player;
        playerScript = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
       // Debug.Log("Hit player = " + hitPlayer);
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            hitPlayer = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        hitPlayer = false;
    }
}