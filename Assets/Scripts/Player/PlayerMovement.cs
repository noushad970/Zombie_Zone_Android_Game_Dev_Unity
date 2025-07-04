using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40;
    float horizontalMove = 0f;
    float inputHorizontalMove = 0f;
    bool crouch=false;
    public Animator anim;
    [Header("Jump System")]
    bool jump = false;
    bool playingJumpingAnimation;
    public static bool isJumping,isPressingShieldButton;
    [SerializeField] private float jumpAnimDelay;
    [Header("Attack System")]
    [HideInInspector]
    public bool isAttack;
    [SerializeField] private float attackAnimDelay;

    [Header("Player UI Buttons")]
    [SerializeField] private UIButtonStateTracker leftButton, rightButton, jumpButton, attackButton,boostButton,shieldButton;

    [SerializeField] private AudioSource attackAudio, JumpAudio, walkAudio, RunAudio;
    private void Start()
    {
        
        //jumpButton.onClick.AddListener(jumpSystem);
        //attackButton.onClick.AddListener(AttackSystem);
    }
    void Update()
    {
        if (!isAttack)
        {
            jumpSystem();
            moveSystem();
        }
        AttackSystem();
        walkAndRunSound();
        isJumping = playingJumpingAnimation;
        if (leftButton.isPressed)
        {

            inputHorizontalMove = -1f;

        }
        else if(rightButton.isPressed)
        {
            
            inputHorizontalMove = 1;
        }
        else
        {
            if (walkAudio!=null)
            {
                walkAudio.Stop();
            }
            inputHorizontalMove = 0f;
        }
        if (shieldButton.isPressed)
        {
            isPressingShieldButton = true;
            anim.Play("shield");
        }
        else
        {
            isPressingShieldButton = false;
            anim.Play("shield");
        }

    }
    private void walkAndRunSound()
    {
        if(!jump && !playingJumpingAnimation && runSpeed == 10)
        {
            if (!walkAudio.isPlaying && walkAudio != null && horizontalMove!=0)
            {
                walkAudio.Play();
            }
        }
        if (!jump && !playingJumpingAnimation && runSpeed == 40 && horizontalMove != 0)
        {
            if (!RunAudio.isPlaying && RunAudio != null)
            {
                RunAudio.Play();
            }
        }
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
    private void OnEnable()
    {
       gameObject.GetComponent<PlayerMovement>().enabled= true;
    }
    private void jumpSystem()
    {
        //Input.GetButtonDown("Jump") &&
        if (jumpButton.isPressed && !isAttack && !isPressingShieldButton)
        {

            if (!playingJumpingAnimation)
            {
                if (!JumpAudio.isPlaying || JumpAudio != null)
                {
                    JumpAudio.Play();
                }
                jump = true;
                playingJumpingAnimation = true;
                anim.Play("jump");
            }
            if (playingJumpingAnimation)
                StartCoroutine(jumpWait());
        }
    }
    IEnumerator jumpWait()
    {
        yield return new WaitForSeconds(jumpAnimDelay);
        if (playingJumpingAnimation == true)
            playingJumpingAnimation = false;
    }
    private void moveSystem()
    {
        //Input.GetAxisRaw("Horizontal")*
        horizontalMove = inputHorizontalMove* runSpeed;
        //if (Input.GetButtonDown("Crouch"))
        //{
        //    crouch = true;
        //}else if (Input.GetButtonUp("Crouch"))
        //{
        //    crouch = false;
        //}

        ///other
        //Input.GetKey(KeyCode.RightShift)
        if (boostButton.isPressed && !isPressingShieldButton)
        {
            runSpeed = 40f;
            
        }
        else
        {
            
            runSpeed = 10f;
        }
        if (horizontalMove != 0 && runSpeed == 10 && playingJumpingAnimation == false && !isPressingShieldButton)
        {
            
            anim.SetFloat("Speed", 0.5f);
        }
        else if (horizontalMove != 0 && runSpeed == 40 && playingJumpingAnimation == false && !isPressingShieldButton)
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
        //Input.GetMouseButtonDown(0) &&
        if ( attackButton.isPressed && !isAttack && !isPressingShieldButton)
        {
            isAttack = true; 
            if(!attackAudio.isPlaying && attackAudio != null)
            {
                attackAudio.Play();
            }
            StartCoroutine(DetectNearbyZombies());
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
    IEnumerator DetectNearbyZombies()
    {
        yield return new WaitForSeconds(0.3f);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 2.5f);//here 2.5f is the radius of the circle to detect nearby zombies
        int count = 1;
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Zombie") && count==1)
            {
                Debug.Log($"Detected object with tag : {hit.name}");
                count++;
                // Add your logic here (e.g., damage, interact)
                hit.GetComponent<EnemyAI>().TakeDamage(50);
            }
            if (hit.CompareTag("Butcher") && count == 1)
            {
                Debug.Log($"Detected object with tag : {hit.name}");
                count++;
                // Add your logic here (e.g., damage, interact)
                AudioManager.instance.BossButcherHurtPlay();
                hit.GetComponent<EnemyAI>().TakeDamage(50);
                CameraFollow.instance.shakeDuration = .3f;
            }
        }
    }

}
