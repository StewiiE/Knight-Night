using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaul : MonoBehaviour
{
    public float damage = 50f;

    BoxCollider weaponCollider;

    public float canDamageTime;

    Player player;

	// Use this for initialization
	void Start ()
    {
        weaponCollider = GetComponent<BoxCollider>();
        weaponCollider.enabled = false;

        canDamageTime = 1f;

        player = FindObjectOfType<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (!weaponCollider.enabled)
            {
                StartCoroutine(canDamage());
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
            enemyScript.TakeDamage(damage);
            Debug.Log("Hit enemy");   
        }
    }

    IEnumerator canDamage()
    {
        if(player.attack == true)
        {
            weaponCollider.enabled = true;
            yield return new WaitForSeconds(canDamageTime);
            weaponCollider.enabled = false;
        }
    }
}
