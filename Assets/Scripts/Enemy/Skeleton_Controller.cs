using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace S019745F
{
	public class Skeleton_Controller : MonoBehaviour
	{
		public float lookRadius = 7.5f;

		Transform target;
		NavMeshAgent agent;
		Animator animator;

		GameObject player;
		Player playerScript;

		bool canDamage;

		private PlayerStats playerStats;

		float animSpeedPercent;

		public GameObject audioHolder;
		AudioSource hitPlayer;

		public GameObject blockHolder;
		AudioSource playerBlock;

		public GameObject bloodEffect;
		public GameObject sparksEffect;

		public Transform effectsPos;

		// Use this for initialization
		void Start()
		{
			target = PlayerManager.instance.player.transform;
			agent = GetComponent<NavMeshAgent>();
			animator = GetComponent<Animator>();

			player = PlayerManager.instance.player;
			playerScript = player.GetComponent<Player>();

			playerStats = FindObjectOfType<PlayerStats>();

			hitPlayer = audioHolder.GetComponent<AudioSource>();
			playerBlock = blockHolder.GetComponent<AudioSource>();
		}

		// Update is called once per frame
		void Update()
		{
			float distance = Vector3.Distance(target.position, transform.position);
			if (distance <= lookRadius)
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

			if (playerScript.enabled == false)
			{
				animator.SetBool("Attack", false);
			}

			animSpeedPercent = agent.velocity.magnitude / agent.speed;

			animator.SetFloat("speedPercent", animSpeedPercent, 0.5f, Time.deltaTime);

			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Impact"))
			{
				animator.SetBool("Impact", false);
			}

		}

		void FaceTarget()
		{
			Vector3 direction = (target.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
		}

		public void AttackEnd()
		{
			if (canDamage == true)
			{
				//playerScript.DoHit();
				if (playerScript.isBlocking == false)
				{
					playerStats.currentHealth -= 5;
					hitPlayer.Play();
					Instantiate(bloodEffect, effectsPos.transform.position, Quaternion.identity);
				}
				else if (playerScript.isBlocking == true)
				{
					animator.SetBool("Impact", true);
					playerBlock.Play();
					Instantiate(sparksEffect, effectsPos.transform.position, Quaternion.identity);
				}
			}
		}
	}

}
