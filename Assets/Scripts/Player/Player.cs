using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S019745F
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Animator))]
	public class Player : MonoBehaviour
	{
		#region Variables

		Animator anim;
		Rigidbody rb;
		Transform cameraT;

		[SerializeField]
		private Canvas skillsPanel;
		[SerializeField]
		private Canvas attributesPanel;
		private bool m_SeeCanvas;

		// Attack vars
		[Header("Attack")]
		private bool isAttacking;
		public bool attack = true;
		public float attackTime;

		// Player speed
		[Header("Speed")]
		public float walkSpeed = 2;
		public float runSpeed = 4;
		float currentSpeed;
		public float attackRotSpeed = 2;
		// Smooth speed
		public float speedSmoothTime = 0.1f;
		float speedSmoothVelocity;

		// Player rotation
		[Header("Rotation")]
		public float turnSmoothTime = 0.2f;
		float turnSmoothVelocity;
		public float damping = 1f;
		float turnSpeed = 10f;

		private PlayerStats playerStats;

		bool gotHit = false;

		#region Anim Vars
		// Animation Vars
		[Header("Animation Vars")]
		public bool animIsAttacking = false;
		private bool isRolling = false;
		public bool isBlocking = false;
		public bool isSlashing = false;
		public bool isLevelingUp = false;
		public bool canAddLevelUpForce = false;
		private bool lockedOn;
		#endregion

		GameObject maulSwingGO;

		int curTarget;
		bool changeTarget;
		bool switchCoroutineRunning = false;

		[Header("Enemies")]
		public List<Transform> Enemies = new List<Transform>();

		#endregion

		// Use this for initialization
		void Start()
		{
			anim = GetComponent<Animator>();
			rb = GetComponent<Rigidbody>();
			cameraT = Camera.main.transform;
			playerStats = FindObjectOfType<PlayerStats>();

			maulSwingGO = this.transform.Find("hips/spine/chest/R_shoulder/R_arm/R_elbow/R_wrist/Maul1/MaulAudio/Swing").gameObject;

			attackTime = 2f;
		}

		private void FixedUpdate()
		{
			HandleTargetsLogic();
		}

		// Update is called once per frame
		void Update()
		{
			Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			Vector2 inputDir = input.normalized;

			if (inputDir != Vector2.zero)
			{
				float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
				transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
			}

			#region Movement
			// Movement
			if (isAttacking == false)
			{
				if (gotHit == false)
				{
					if (isRolling == false)
					{
						if (isBlocking == false)
						{
							if (isLevelingUp == false)
							{
								/* bool running = Input.GetKey(KeyCode.LeftShift);
								 float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
								 currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

								 transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

								 float animationSpeedPercent = ((running) ? 1 : 0.5f) * inputDir.magnitude;
								 anim.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
								 */

								// New Movement
								Moving();
								Running();
								Turning();

								if(anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
								{
									Debug.Log("Just attacked");
								}

							}
						}
					}
				}
			}
			#endregion


			// Attack button
			if (Input.GetButtonDown("Attack"))
			{
				if (isRolling == false)
				{
					Attack();
				}
			}

			if(Input.GetKeyDown("tab"))
			{
				if(skillsPanel)
				{
					m_SeeCanvas = !m_SeeCanvas;
					skillsPanel.gameObject.SetActive(m_SeeCanvas); // Toggle canvas
					if (attributesPanel)
						attributesPanel.gameObject.SetActive(m_SeeCanvas); 

					if(m_SeeCanvas == true)
					{
						Time.timeScale = 0f;
						Cursor.visible = true;
						Cursor.lockState = CursorLockMode.None;
					}
					else
					{
						Time.timeScale = 1f;
						Cursor.visible = false;
						Cursor.lockState = CursorLockMode.Locked;
					}
				}
			}

			#region Rotate Camera On Attack
			// Rotate player to camera rotation when attacking
			if (isAttacking == true)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, cameraT.rotation, Time.deltaTime * damping);
				transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f));
			}
			#endregion

			#region Got Hit

			if (anim.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
			{
				gotHit = true;
			}
			else
			{
				gotHit = false;
			}
			#endregion

			#region is Attacking

			if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
			{
				attack = false;
				isAttacking = true;
			}
			else
			{
				attack = true;
				isAttacking = false;
			}
			#endregion

			#region is Rolling

			if (anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
			{
				isRolling = true;
			}
			else
			{
				isRolling = false;
			}

			#endregion

			#region is Leveling Up
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("LevelUp"))
			{
				isLevelingUp = true;
			}
			else
			{
				isLevelingUp = false;
			}
			#endregion

			#region Do Roll
			if (Input.GetKeyDown(KeyCode.G))
			{
				if (isRolling == false)
				{
					if (isSlashing == false)
					{
						if (playerStats.currentMana >= 25)
						{
							lockedOn = false;
							// rb.AddRelativeForce(Vector3.forward * 3000);
							anim.Play("Roll", 0, 0.0f);
							playerStats.currentMana -= 25;
						}
					}
				}
			}
			#endregion

			#region Block
			if (Input.GetKey(KeyCode.Q))
			{
				anim.SetBool("isBlocking", true);
				isBlocking = true;
			}
			else
			{
				anim.SetBool("isBlocking", false);
				isBlocking = false;
			}
			#endregion

			LockOn();
		}

		void Moving()
		{
			anim.SetFloat("Forward", Input.GetAxis("Vertical") * 1f);
		}
		void Running()
		{
			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				anim.SetBool("Run", true);
			}
			if (Input.GetKeyUp(KeyCode.LeftShift))
			{
				anim.SetBool("Run", false);
			}
		}

		void Turning()
		{
			anim.SetFloat("Turning", Input.GetAxis("Horizontal"));
		}

		// Attack
		public void Attack()
		{
			if (!isAttacking)
			{
				int randomInt = Random.Range(0, 4);
				anim.SetFloat("AttackInt", randomInt);
				Debug.Log(randomInt);
				StartCoroutine(AttackAnim());
			}
		}

		// Setting attack animations
		IEnumerator AttackAnim()
		{
			if (attack == true)
			{
				anim.SetBool("isAttacking", true);
				yield return new WaitForSeconds(0.1f);
				anim.SetBool("isAttacking", false);
			}
		}

		public void DoHit()
		{
			if (anim.GetBool("isDead") == false)
			{
				if (isBlocking == false)
				{
					anim.Play("GetHit", 0, 0.0f);
				}
				else if (isBlocking == true)
				{
					anim.Play("BlockReact", 0, 0.0f);
				}
			}
		}

		#region Animation Events
		public void AttackStart()
		{
			animIsAttacking = true;
			StartCoroutine(AttackEnded());
		}

		public void AttackEnd()
		{
			animIsAttacking = false;
		}

		public void StartSlash()
		{
			isSlashing = true;
		}

		public void EndSlash()
		{
			isSlashing = false;
		}

		public void SwingAudio()
		{
			maulSwingGO.GetComponent<AudioSource>().Play();
		}

		#endregion

		IEnumerator AttackEnded()
		{
			yield return new WaitForSeconds(1f);
			animIsAttacking = false;
		}

		void HandleTargetsLogic()
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				StartCoroutine(SelectTargetToggle());
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				if (switchCoroutineRunning == false)
				{
					StartCoroutine(SwitchTargetToggle());
				}
			}
		}

		void LockOn()
		{
			if (Enemies.Count >= 1)
			{
				if (lockedOn)
				{
					anim.SetBool("LockOn", true);
					Vector3 dir = Enemies[curTarget].position - transform.position;
					Quaternion lookRotation = Quaternion.LookRotation(dir);
					Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
					transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

					foreach (Transform enemy in Enemies)
					{
						if (enemy.Equals(Enemies[curTarget]))
						{
							GameObject curEnemy = enemy.gameObject;
							ToggleOutine toggleOutine = curEnemy.GetComponent<ToggleOutine>();
							toggleOutine.showOutline = true;
						}
						else
						{
							GameObject curEnemy = enemy.gameObject;
							ToggleOutine toggleOutine = curEnemy.GetComponent<ToggleOutine>();
							toggleOutine.showOutline = false;
						}
					}
				}
				else
				{
					anim.SetBool("LockOn", false);
					foreach (Transform enemy in Enemies)
					{
						GameObject curEnemy = enemy.gameObject;
						ToggleOutine toggleOutine = curEnemy.GetComponent<ToggleOutine>();
						toggleOutine.showOutline = false;
					}
				}
			}
		}

		IEnumerator SelectTargetToggle()
		{
			if (lockedOn)
			{
				yield return new WaitForSeconds(0.2f);
				lockedOn = false;
			}
			else if (lockedOn == false)
			{
				yield return new WaitForSeconds(0.2f);
				lockedOn = true;
			}
		}

		IEnumerator SwitchTargetToggle()
		{
			if (curTarget < Enemies.Count - 1)
			{
				switchCoroutineRunning = true;
				yield return new WaitForSeconds(0.2f);
				curTarget++;
				switchCoroutineRunning = false;
			}
			else
			{
				switchCoroutineRunning = true;
				yield return new WaitForSeconds(0.2f);
				curTarget = 0;
				switchCoroutineRunning = false;
			}
		}
	}
}

