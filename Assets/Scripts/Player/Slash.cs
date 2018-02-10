using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{

    Player player;
    TrailRenderer weaponTrail;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>();
        weaponTrail = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update ()
    {
		if(player.isSlashing == true)
        {
            weaponTrail.enabled = true;
        }
        else if(player.isSlashing == false)
        {
            weaponTrail.enabled = false;
        }
	}
}
