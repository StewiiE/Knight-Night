using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace S019745F
{
	public class Enemy_Archer_Controller : MonoBehaviour
	{
		public float lookRadius = 35f;

		// Archer vars
		Transform target;
		NavMeshAgent agent;
		Animator animator;

		// Player vars
		GameObject player;
		Player playerScript;

		bool canDamage;

		private PlayerStats playerStats;

		float animSpeedPercent;
		public bool canMove = true;

		public GameObject arrow;
		public GameObject arrowProjectile;

		void Start()
		{
			target = PlayerManager.instance.player.transform;
			agent = GetComponent<NavMeshAgent>();
			animator = GetComponent<Animator>();

			player = PlayerManager.instance.player;
			playerScript = player.GetComponent<Player>();

			playerStats = FindObjectOfType<PlayerStats>();
		}

		void Update()
		{
			float distance = Vector3.Distance(target.position, transform.position);
			if (distance <= lookRadius)
			{
				if (canMove == true)
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

			if (playerScript.enabled == false)
			{
				animator.SetBool("Attack", false);
			}

			animSpeedPercent = agent.velocity.magnitude / agent.speed;

			animator.SetFloat("speedPercent", animSpeedPercent, 0.5f, Time.deltaTime);
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

		void DrawArrow()
		{
			arrow.SetActive(true);
		}

		void FireArrow()
		{
			arrow.SetActive(false);

			// Spawn Arrow
			GameObject go = Instantiate(arrowProjectile, arrow.transform.position, arrow.transform.rotation) as GameObject;
		}
	}
}
