using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    Transform cameraT;
    
    // Attack vars
    private bool isAttacking;
    public bool attack = true;

    // Player speed
    public float walkSpeed = 2;
    public float runSpeed = 4;
    float currentSpeed;
    public float attackRotSpeed = 2;

    // Player rotation
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;
    public float damping = 100f;

    // Smooth speed
    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;

    public float attackTime;

    private PlayerStats playerStats;

    bool gotHit = false;

    // Animation Vars
    public bool animIsAttacking = false;
    private bool isRolling = false;
    public bool isBlocking = false;
    public bool isSlashing = false;
    public bool isLevelingUp = false;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cameraT = Camera.main.transform;
        playerStats = FindObjectOfType<PlayerStats>();

        attackTime = 2f;
    }
	
	// Update is called once per frame
	void Update ()
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
                if(isRolling == false)
                {
                    if(isBlocking == false)
                    {
                        if(isLevelingUp == false)
                        {
                            bool running = Input.GetKey(KeyCode.LeftShift);
                            float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
                            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

                            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

                            float animationSpeedPercent = ((running) ? 1 : 0.5f) * inputDir.magnitude;
                            anim.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
                        }
                    }
                }
            }
        }
        #endregion

        // Attack button
        if (Input.GetButtonDown("Attack"))
        {
            if(isRolling == false)
            {
                Attack();
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

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
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
            if(isRolling == false)
            {
                if(isSlashing == false)
                {
                    rb.AddRelativeForce(Vector3.forward * 3000);
                    anim.Play("Roll", 0, 0.0f);
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
    }

    // Attack
    public void Attack()
    {
        if(!isAttacking)
           StartCoroutine(AttackAnim());
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
        if(anim.GetBool("isDead") == false)
        {
            if(isBlocking == false)
            {
                anim.Play("GetHit", 0, 0.0f);
            }
            else if(isBlocking == true)
            {
                anim.Play("BlockReact", 0, 0.0f);
            }
        }
    }

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
     //   isSlashing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isLevelingUp)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
                enemyScript.TakeDamage(20f);
                enemyScript.AddForceToEnemy();
            }
        }
    }

    IEnumerator AttackEnded()
    {
        yield return new WaitForSeconds(1f);
        animIsAttacking = false;
    }
}
