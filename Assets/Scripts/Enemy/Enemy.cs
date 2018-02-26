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
	Animator animator;

    GameObject player;
    Player playerScript;

	// Use this for initialization
	void Start ()
    {
        currentHealth = 100f;

        thePlayerStats = FindObjectOfType<PlayerStats>();

        rb = this.gameObject.GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();

        player = PlayerManager.instance.player;
        playerScript = player.GetComponent<Player>();
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
		EnemyController enemyController = GetComponent<EnemyController>();
		currentHealth = currentHealth -= damage;
		enemyController.canMove = false;
		animator.Play("KnockDown");
	}

    public void Death()
    {
        RagdollDeath();

        Destroy(this.gameObject);

        if(playerScript.isLevelingUp == false)
        {
            thePlayerStats.AddExperience(expToGive);
        }
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
}
