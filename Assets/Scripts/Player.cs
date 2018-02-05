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
    public float damping = 10f;

    // Smooth speed
    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;

    public float attackTime;

    private PlayerStats playerStats;

    bool gotHit = false;

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


        // Don't move if attacking
        if (isAttacking == false)
        {
            if (gotHit == false)
            {
                bool running = Input.GetKey(KeyCode.LeftShift);
                float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
                currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

                transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

                float animationSpeedPercent = ((running) ? 1 : 0.5f) * inputDir.magnitude;
                anim.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
            }
        }

        // Quit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        // Attack button
        if (Input.GetButtonDown("Attack"))
        {
            Attack();
        }

        // Rotate player to camera rotation when attacking
        if (isAttacking == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, cameraT.rotation, Time.deltaTime * damping);
            transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f));
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            playerStats.currentHealth -= 10;
        }

        #region Got Hit

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
        {
            Debug.Log("Playing GetHit");
            gotHit = true;
        }
        else
        {
            gotHit = false;
        }
        #endregion

        #region is Attacking

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("standing_melee_attack_downward"))
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
            anim.Play("GetHit", 0, 0.0f);
        }
    }
}
