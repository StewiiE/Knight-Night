using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaul : MonoBehaviour
{
    public float damage = 100f;

    Player player;

	// Use this for initialization
	void Start ()
    {
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(player.animIsAttacking == true)
            {
                Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
                enemyScript.TakeDamage(damage);
            }
        }
    }
}
