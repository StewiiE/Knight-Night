using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaul : MonoBehaviour
{
    public float damage = 100f;

    float force = 50f;

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
                Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
                enemyScript.TakeDamage(damage);

                enemyRb.AddForce(player.gameObject.transform.forward * force, ForceMode.Impulse);

                enemyRb.AddExplosionForce(force * 10f, player.transform.position, 100f);

                Debug.Log("Hit enemy");
            }
        }
    }
}
