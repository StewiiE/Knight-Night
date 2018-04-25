using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	public class Enemy_Paladin : MonoBehaviour, IDamageable
	{
		public float currentHealth;
		float maxHealth = 100f;

		private PlayerStats thePlayerStats;
		public int expToGive;

		public GameObject ragdollPrefab;
		Rigidbody ragdollRB;

		Rigidbody rb;
		Animator animator;
		Enemy_Paladin_Controller enemy_Paladin_Controller;

		GameObject player;
		Player playerScript;

		bool isDead = false;

		[SerializeField]
		Arena1Spawner spawnerRef;

		// Use this for initialization
		void Start()
		{
			currentHealth = 100f;

			thePlayerStats = FindObjectOfType<PlayerStats>();

			rb = this.gameObject.GetComponent<Rigidbody>();
			animator = GetComponent<Animator>();

			player = PlayerManager.instance.player;
			playerScript = player.GetComponent<Player>();

			enemy_Paladin_Controller = GetComponent<Enemy_Paladin_Controller>();

			spawnerRef = FindObjectOfType<Arena1Spawner>();
		}

		// Update is called once per frame
		void Update()
		{
			if (currentHealth <= 0)
			{
				if(!isDead)
				{
					Death();
					isDead = true;
				}
			}
		}

		public void TakeDamage(float damage)
		{
			currentHealth = currentHealth -= damage;
			enemy_Paladin_Controller.canMove = false;
			animator.Play("KnockDown");
			//rb.freezeRotation = true;
			rb.constraints = RigidbodyConstraints.FreezeAll;
			StartCoroutine(InvincibleTime());
		}

		public void Death()
		{
			rb.constraints = RigidbodyConstraints.None;

			RagdollDeath();

			playerScript.Enemies.Remove(this.transform);

			spawnerRef.enemiesAlive--;

			if (playerScript.isLevelingUp == false)
			{
				thePlayerStats.AddExperience(expToGive);
			}

			Destroy(this.gameObject);
		}

		public void RagdollDeath()
		{
			GameObject ragdoll = Instantiate(ragdollPrefab, transform.root.transform.position, Quaternion.identity) as GameObject;

			Transform ragdollMaster = ragdoll.transform.Find("Hips");
			Transform paladinMaster = transform.root.Find("Hips");

			Transform[] ragdollJoints = ragdollMaster.GetComponentsInChildren<Transform>();
			Transform[] currentJoints = paladinMaster.GetComponentsInChildren<Transform>();

			for (int i = 0; i < ragdollJoints.Length; i++)
			{
				for (int q = 0; q < currentJoints.Length; q++)
				{
					if (currentJoints[q].name.CompareTo(ragdollJoints[i].name) == 0)
					{
						ragdollJoints[i].position = currentJoints[q].position;
						ragdollJoints[i].rotation = currentJoints[q].rotation;
						break;
					}
				}
			}

			for (int i = 0; i < ragdollJoints.Length; i++)
			{
				if (ragdollJoints[i].GetComponent<Rigidbody>() != null)
				{
					Rigidbody rb = ragdollJoints[i].GetComponent<Rigidbody>();
					rb.AddRelativeForce(-ragdollMaster.transform.forward * 4000);
				}
			}
		}

		IEnumerator InvincibleTime()
		{
			this.GetComponent<CapsuleCollider>().enabled = false;
			yield return new WaitForSeconds(4f);
			this.GetComponent<CapsuleCollider>().enabled = true;
			rb.constraints = RigidbodyConstraints.None;
		}
	}
}

