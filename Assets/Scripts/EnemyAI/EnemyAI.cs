using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float detectionRange = 10f; // Range to detect the player
    public float attackRange = 2f; // Range to attack the player
    public float moveSpeed = 2f; // Zombie movement speed
    public float attackInterval = 1f; // Time between attacks
    public int attackDamage = 10; // Damage to deal to the player

    private Animator anim;
    private bool isAttacking = false;
    public ParticleSystem bloodParticle;
    void Awake()
    {
        stopWalkAudio = false;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        anim = GetComponent<Animator>();
    }

    void Update()
    {


        if (player != null)
        {
            if (player.GetComponent<PlayerHealth>().getHealth() > 0)
            {
                HandleAI();
            }
        }
        
        
    }

    void HandleAI()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // Attack State
            if (Time.time >= nextRaycastTime)
            {
                PerformRaycast();
                nextRaycastTime = Time.time + raycastInterval; // Schedule the next raycast
            }

            anim.SetBool("isMoving", false);
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // Chase State
            anim.SetBool("isMoving", true);
            MoveTowardsPlayer();
        }
        else
        {
            // Idle State
            anim.SetBool("isMoving", false);
        }
        if(!stopWalkAudio) 
        AudioManager.instance.zombieWalkPlay();
        else
        {
            AudioManager.instance.zombieWalk.Stop();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

        // Flip sprite based on movement direction
        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public float clickCooldown = 1f; // Delay between clicks
    public float raycastRange = 10f; // Range of the raycast
     float raycastInterval = 4f; // Interval between raycasts
    public LayerMask targetLayer; // Layer to detect objects (set Player's layer here)

    private float nextRaycastTime = 0f; // Time for the next raycast



    void PerformRaycast()
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector2 rayOrigin = (Vector2)transform.position + direction * 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, raycastRange, targetLayer);

        if (!died)
            anim.Play("attack");


        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            StartCoroutine(shakeDelay());
            AudioManager.instance.BitePlay();

            player.GetComponent<PlayerHealth>().TakeDamage(10);
        }
        else
        {
            Debug.Log("No Player detected.");
        }
    }

    IEnumerator shakeDelay()
    {
        yield return new WaitForSeconds(0.2f);
        
        CameraFollow.instance.shakeDuration = .3f;
    }
    IEnumerator BloodDelay()
    {
        yield return new WaitForSeconds(0.2f);
        bloodParticle.Play();
    }
    public float health = 50f; // Initial health of the zombie
    bool died=false;
    public void TakeDamage(float damage)
    {
        health -= damage;
        nextRaycastTime = Time.time + raycastInterval;
        anim.Play("hurt");
        StartCoroutine(BloodDelay());
        if (health <= 0)
        {
            Die();
        }
    }
    bool stopWalkAudio;
    void Die()
    {
        anim.Play("died");
        died =true;
        Debug.Log("Zombie died!");
        int randomCoins = Random.Range(3, 8); // Random coins between 3 and 8
        InGameCollectionUI.totalCoinCollectedinOneGame += randomCoins; // Increment total coins collected
        InGameCollectionUI.totalKillinOneGame += 1; // Increment total kills
        GameDataManager.AddCoins(randomCoins);
        GameDataManager.AddZombieKill(1);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        stopWalkAudio=true;
        AudioManager.instance.zombieDiePlay();
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        isAttacking = false;
        Destroy(gameObject,3); // Destroy the zombie
    }
}
