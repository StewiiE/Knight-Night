using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 7.5f;

    Transform target;
    NavMeshAgent agent;
    Animator animator;

    GameObject player;
    Player playerScript;

    public GameObject sword;
    EnemySword swordScript;

    bool canDamage;

    private PlayerStats playerStats;

    float animSpeedPercent;
	public bool canMove = true;

    // Use this for initialization
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        player = PlayerManager.instance.player;
        playerScript = player.GetComponent<Player>();

        swordScript = sword.GetComponent<EnemySword>();

        playerStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius)
        {
			if(canMove == true)
			{
				agent.SetDestination(target.position);

				if (distance <= agent.stoppingDistance)
				{
					canDamage = true;

					FaceTarget();

					animator.SetBool("Attack", true);
				}
				else
				{
					animator.SetBool("Attack", false);
					canDamage = false;
				}
			}
        }

        if(playerScript.enabled == false)
        {
            animator.SetBool("Attack", false);
        }

        animSpeedPercent = agent.velocity.magnitude / agent.speed;

        animator.SetFloat("speedPercent", animSpeedPercent, 0.5f, Time.deltaTime);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Impact"))
        {
            animator.SetBool("Impact", false);
        }

		if (animator.GetCurrentAnimatorStateInfo(0).IsName("KnockDown") || animator.GetCurrentAnimatorStateInfo(0).IsName("GetUp"))
		{
			canMove = false;
		}
		else
		{
			canMove = true;
		}
		Debug.Log(canMove);
	}

	void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }


    public void AttackEnd1()
    {
        if(canDamage == true)
        {
            playerScript.DoHit();
            if(playerScript.isBlocking == false)
            {
                playerStats.currentHealth -= 5;
            }
            else if(playerScript.isBlocking == true)
            {
                animator.SetBool("Impact", true);
            }
        }
    }

    public void AttackEnd2()
    {
        if (canDamage == true)
        {
            playerScript.DoHit();
            if (playerScript.isBlocking == false)
            {
                playerStats.currentHealth -= 5;
            }
            else if (playerScript.isBlocking == true)
            {
                animator.SetBool("Impact", true);
            }
        }
    }
}
