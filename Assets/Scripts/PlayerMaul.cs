using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaul : MonoBehaviour
{
    public float damage = 50f;

    BoxCollider weaponCollider;

    public float canDamageTime;

    float force = 30f;

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
                StartCoroutine(CanDamage());
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            enemyScript.TakeDamage(damage);

            enemyRb.AddForce(this.gameObject.transform.forward * force, ForceMode.Impulse);

            Debug.Log("Hit enemy");   
        }
    }

    IEnumerator CanDamage()
    {
        if(player.attack == true)
        {
            weaponCollider.enabled = true;
            yield return new WaitForSeconds(canDamageTime);
            weaponCollider.enabled = false;
        }
    }
}
