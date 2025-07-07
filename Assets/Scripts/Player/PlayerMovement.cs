using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public GameObject referenceForTag;
    public float runSpeed = 40;
    public float attackDelayVal = 0.5f;
    public float jumpDelayVal = 0.5f;
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

    [Header("References")]
    [Tooltip("Empty object where arrows appear (e.g., 'ShootPoint').")]
    [SerializeField] private Transform shootPoint;

    [Tooltip("Arrow prefab with Rigidbody2D & ArrowRotate.")]
    [SerializeField] private GameObject arrowPrefab;

    [Header("Shot Settings")]
    [SerializeField] private float shootForce = 30f;   // impulse strength
    [Header("Bullet Settings")]
    [SerializeField] private GameObject bulletPrefab;  // assign in Inspector
    [SerializeField] private Transform shootPointBullet;    // reference empty object
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private ParticleSystem shellRemoveParticle;
    [Header("SpellAttack Settings")]
    [SerializeField] private GameObject spellPrefab;  // assign in Inspector
    [SerializeField] private Transform shootPointSpell;    // reference empty object
    [SerializeField] private float spellSpeed = 15f;

    private void Start()
    {

        //jumpButton.onClick.AddListener(jumpSystem);
        //attackButton.onClick.AddListener(AttackSystem);
        //GameDataManager.AddCoins(50000);
    }
    void Update()
    {
        Debug.Log("Is Attack Pressed:"+ attackButton.isPressed + " isAttacking: "+isAttack);
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
        //if (shieldButton.isPressed)
        //{
        //    isPressingShieldButton = true;
        //    anim.Play("shield");
        ////}
        //else
        //{
        //    isPressingShieldButton = false;
        //    anim.Play("shield");
        //}
      

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
        if (jumpButton.isPressed && !isAttack)
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
                StartCoroutine(disableButton(jumpButton, jumpDelayVal));//extra
            }
            if (playingJumpingAnimation)
            {
                StartCoroutine(jumpWait());
            }
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
    private IEnumerator FireArrow()
    {
        yield return new WaitForSeconds(.5f);   // small delay before firing

        // 1. Spawn the arrow
        GameObject arrowObj = Instantiate(
            arrowPrefab,
            shootPoint.position,
            shootPoint.rotation);

        // 2. Work out which way the player is facing
        //    (Assumes this script lives on the player or a child of the player.)
        Vector2 dir = transform.localScale.x < 0 ? Vector2.left : Vector2.right;

        // 3. Align the arrow graphic with that direction
        arrowObj.transform.right = dir;

        // 4. Add the impulse so the arrow travels in that direction
        Rigidbody2D rb = arrowObj.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * shootForce, ForceMode2D.Impulse);
    }

    private IEnumerator FireBullet()
    {
        yield return new WaitForSeconds(0.3f);           // ⬅️ adjust or remove delay
        
        // 1. Spawn the bullet
        GameObject bulletObj = Instantiate(
            bulletPrefab,
            shootPointBullet.position,
            shootPointBullet.rotation);                         // initial rotation
        shellRemoveParticle.Play(); // play shell remove particle effect
        // 2. Decide facing: left if localScale.x < 0, else right
        Vector2 dir = transform.localScale.x < 0 ? Vector2.left : Vector2.right;

        //    Make the bullet’s graphic point that way
        bulletObj.transform.right = dir;

        // 3. Give the bullet straight‑line velocity   (remove if BulletFire sets it)
        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;                             // ensure no drop
        rb.linearVelocity = dir * bulletSpeed;              // ⬅️ delete if redundant
    }
    private IEnumerator FireSpell()
    {
        yield return new WaitForSeconds(0.3f);           // ⬅️ adjust or remove delay

        // 1. Spawn the bullet
        GameObject spellObj = Instantiate(
            spellPrefab,
            shootPointSpell.position,
            shootPointSpell.rotation);                         // initial rotation
        // 2. Decide facing: left if localScale.x < 0, else right
        Vector2 dir = transform.localScale.x < 0 ? Vector2.left : Vector2.right;

        //    Make the bullet’s graphic point that way
        spellObj.transform.right = dir;

        // 3. Give the bullet straight‑line velocity   (remove if BulletFire sets it)
        Rigidbody2D rb = spellObj.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;                             // ensure no drop
        rb.linearVelocity = dir * bulletSpeed;              // ⬅️ delete if redundant
    }
    IEnumerator disableButton(UIButtonStateTracker button,float delay)
    {
        button.gameObject.SetActive(false);
        button.isPressed = false;
        yield return new WaitForSeconds(delay);
        button.gameObject.SetActive(true);
    }
    private void AttackSystem()
    {

        //Input.GetMouseButtonDown(0) &&
        if ( attackButton.isPressed && !isAttack)
        {
            StartCoroutine(disableButton(attackButton, attackDelayVal));
            if (referenceForTag.CompareTag("Archer"))
            {
                isAttack = true;
                if (!attackAudio.isPlaying && attackAudio != null)
                {
                    attackAudio.Play();
                }
                StartCoroutine(FireArrow());
            } 
            else if (referenceForTag.CompareTag("Pirate"))
            {

                isAttack = true;
                if (!attackAudio.isPlaying && attackAudio != null)
                {
                    attackAudio.Play();
                }

                StartCoroutine(FireBullet());

            }else if(referenceForTag.CompareTag("Girl2") || referenceForTag.CompareTag("Robot1"))
            {

                isAttack = true;
                if (!attackAudio.isPlaying && attackAudio != null)
                {
                    attackAudio.Play();
                }

                StartCoroutine(FireSpell());
            }
            else
            {
                isAttack = true;
                if (!attackAudio.isPlaying && attackAudio != null)
                {
                    attackAudio.Play();
                }
                StartCoroutine(DetectNearbyZombies());
            }
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
