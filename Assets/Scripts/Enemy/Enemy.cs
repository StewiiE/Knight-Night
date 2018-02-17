using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float currentHealth;
    float maxHealth = 100f;

    private PlayerStats thePlayerStats;
    public int expToGive;

    public GameObject ragdollPrefab;
    Rigidbody ragdollRB;

    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        currentHealth = 100f;

        thePlayerStats = FindObjectOfType<PlayerStats>();

        rb = this.gameObject.GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth -= damage;
    }

    public void Death()
    {
        RagdollDeath();

        Destroy(this.gameObject);

        thePlayerStats.AddExperience(expToGive);
    }

    public void RagdollDeath()
    {
        GameObject ragdoll = Instantiate(ragdollPrefab, transform.root.transform.position, Quaternion.identity) as GameObject;

        Transform ragdollMaster = ragdoll.transform.Find("Hips");
        Transform paladinMaster = transform.root.Find("Hips");

        Transform[] ragdollJoints = ragdollMaster.GetComponentsInChildren<Transform>();
        Transform[] currentJoints = paladinMaster.GetComponentsInChildren<Transform>();

        for(int i = 0; i < ragdollJoints.Length; i++)
        {
            for(int q = 0; q < currentJoints.Length; q++)
            {
                if(currentJoints[q].name.CompareTo(ragdollJoints[i].name) == 0)
                {
                    ragdollJoints[i].position = currentJoints[q].position;
                    ragdollJoints[i].rotation = currentJoints[q].rotation;
                    break;
                }
            }
        }

        for(int i = 0; i < ragdollJoints.Length; i++)
        {
            if(ragdollJoints[i].GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = ragdollJoints[i].GetComponent<Rigidbody>();
                rb.AddRelativeForce(-ragdollMaster.transform.forward * 4000);
            }
        }
    }

    public void AddForceToEnemy()
    {
        rb.AddForce(-transform.forward * 4000);
    }
}
