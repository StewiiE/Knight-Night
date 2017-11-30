using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaul : MonoBehaviour
{
    public float damage = 50f;

    BoxCollider weaponCollider;

	// Use this for initialization
	void Start ()
    {
        weaponCollider = GetComponent<BoxCollider>();
        weaponCollider.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!weaponCollider.enabled)
            {
                StartCoroutine(canDamage());
            }
        }

        Debug.Log(weaponCollider.enabled);
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
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(1);
        weaponCollider.enabled = false;
    }
}
