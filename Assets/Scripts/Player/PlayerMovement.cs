using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40;
    float horizontalMove = 0f;
    
    bool crouch=false;
    public Animator anim;
    [Header("Jump System")]
    bool jump = false;
    bool playingJumpingAnimation;
    [SerializeField] private float jumpAnimDelay;
    [Header("Attack System")]
    [HideInInspector]
    public bool isAttack;
    [SerializeField] private float attackAnimDelay;
    void Update()
    {

        if (!isAttack)
        {
            jumpSystem();
            moveSystem();
        }
        AttackSystem();
        //Debug.Log("playingJumpingAnimation: " + playingJumpingAnimation);
        
    }

    public void OnLanding()
    {
        anim.SetBool("Jump", false);
    }
    
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

    }
    private void jumpSystem()
    {
        if (Input.GetButtonDown("Jump") && !playingJumpingAnimation)
        {
            jump = true;
            playingJumpingAnimation = true;
            anim.Play("jump");
        }
        if (playingJumpingAnimation)
            StartCoroutine(jumpWait());
    }
    IEnumerator jumpWait()
    {
        yield return new WaitForSeconds(jumpAnimDelay);
        if (playingJumpingAnimation == true)
            playingJumpingAnimation = false;
    }
    private void moveSystem()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        //if (Input.GetButtonDown("Crouch"))
        //{
        //    crouch = true;
        //}else if (Input.GetButtonUp("Crouch"))
        //{
        //    crouch = false;
        //}

        ///other
        if (Input.GetKey(KeyCode.RightShift))
        {
            runSpeed = 40f;
        }
        else
        {
            runSpeed = 10f;
        }
        if (horizontalMove != 0 && runSpeed == 10 && playingJumpingAnimation == false)
        {
            anim.SetFloat("Speed", 0.5f);
        }
        else if (horizontalMove != 0 && runSpeed == 40 && playingJumpingAnimation == false)
        {
            anim.SetFloat("Speed", 1.4f);
        }
        else
        {
            anim.SetFloat("Speed", 0f);
        }
    }
    private void AttackSystem()
    {
        if (Input.GetMouseButtonDown(0) && !isAttack)
        {
            isAttack = true;
        }
        if (isAttack)
        {
            anim.Play("swordAttackChar");
           
            StartCoroutine(attackDelay());
        }
    }
    IEnumerator attackDelay()
    {
        
        yield return new WaitForSeconds(attackAnimDelay);
        anim.SetBool("Attack", false);
        isAttack = false;
    }

    
}
