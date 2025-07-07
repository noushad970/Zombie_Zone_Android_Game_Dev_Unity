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

    public Animator anim;
    private bool isAttacking = false;
    public ParticleSystem bloodParticle;
    public Transform rayOriginReference; // Optional reference for raycast origin
    public ParticleSystem bossAttackPracticle;
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
    private void Start()
    {

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
                StartCoroutine(waitaLittle()); // Wait a little before the next raycast
                nextRaycastTime = Time.time + raycastInterval; // Schedule the next raycast
            }

            anim.SetBool("isMoving", false);
        }
        else if (distanceToPlayer <= detectionRange && !died)
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
        if(!stopWalkAudio && BossSpawner.isBossSpawnedOnce) 
        AudioManager.instance.bossButcherWalkPlay();
        if (!stopWalkAudio && !BossSpawner.isBossSpawnedOnce)
            AudioManager.instance.zombieWalkPlay();
        else
        {
            AudioManager.instance.zombieWalk.Stop();
        }
    }
    IEnumerator waitaLittle()
    {
        yield return new WaitForSeconds(0.5f);

        if (!died)
            PerformRaycast();
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

    public float raycastRange = 10f; // Range of the raycast
     float raycastInterval = 4f; // Interval between raycasts
    public LayerMask targetLayer; // Layer to detect objects (set Player's layer here)

    private float nextRaycastTime = 0f; // Time for the next raycast
    private Vector2 rayOrigins, directions;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(rayOrigins, rayOrigins + directions * 2);
    }
    void PerformRaycast()
    {
        // 1. Horizontal direction (right if scale.x > 0, else left)
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        // 2. Choose origin: reference object if set, otherwise this transform
        Vector2 origin = rayOriginReference != null
                         ? (Vector2)rayOriginReference.position
                         : (Vector2)transform.position;

        //    Optional vertical offset (1 unit up); tweak or remove as needed
        origin += Vector2.up * 1f;

        // 3. Store for debugging
        rayOrigins = origin;
        directions = direction;

        // 4. Cast the ray
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, raycastRange, targetLayer);

        // 5. Visualize
        Debug.DrawLine(origin, origin + direction * raycastRange, Color.red, 0f, false);

        // 6. Anim & logic
        if (!died && BossSpawner.isBossSpawnedOnce)
        {
            anim.Play("attack");
            StartCoroutine(playBossAttackParticle()); // Play particle effect after a delay
        }
        else if (!died && !BossSpawner.isBossSpawnedOnce)
        {
            anim.Play("attack");
        }

        if (hit.collider != null && hit.collider.CompareTag("Player") && BossSpawner.isBossSpawnedOnce && !PlayerMovement.isJumping)
        {
            StartCoroutine(ShakeDelayWithPlayerDamageByBoss());
            AudioManager.instance.BossButcherAttackPlay();
        }
        else if (hit.collider != null && hit.collider.CompareTag("Player") && !BossSpawner.isBossSpawnedOnce && !PlayerMovement.isJumping)
        {
            StartCoroutine(ShakeDelayWithPlayerDamage());
            AudioManager.instance.BitePlay();
        }
        else if(BossSpawner.isBossSpawnedOnce)
        {
            AudioManager.instance.BossButcherAttackPlay();
            Debug.Log("No Player detected.");
        }
        else
        {
            AudioManager.instance.BitePlay();
            Debug.Log("No Player detected.");
        }
    }
    IEnumerator playBossAttackParticle()
    {
        yield return new WaitForSeconds(0.3f);
        if(bossAttackPracticle != null) 
        bossAttackPracticle.Play();
    }

    IEnumerator ShakeDelayWithPlayerDamage()
    {
        yield return new WaitForSeconds(0.2f);
        if (PlayerMovement.isPressingShieldButton)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(2);
        }
        else
        {
            CameraFollow.instance.shakeDuration = .5f;
            player.GetComponent<PlayerHealth>().TakeDamage(10);
        }
    }
    IEnumerator ShakeDelayWithPlayerDamageByBoss()
    {
        yield return new WaitForSeconds(0.2f);
        if (PlayerMovement.isPressingShieldButton)
        {
            CameraFollow.instance.shakeDuration = 0.5f;
            player.GetComponent<PlayerHealth>().TakeDamage(10);
        }
        else
        {
            CameraFollow.instance.shakeDuration = 1f;
            player.GetComponent<PlayerHealth>().TakeDamage(25);
        }
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
        
        if (health <= 0 && BossSpawner.isBossSpawnedOnce)
        {
            DieBoss();
        }else if (health <= 0 && !BossSpawner.isBossSpawnedOnce)
        {
            DieZombie();
        }
    }
    bool stopWalkAudio;
    void DieZombie()
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
    void DieBoss()
    {
        anim.Play("died");
        died = true;
        Debug.Log("Boss died!");
        int randomCoins = 1000; // Random coins between 3 and 8
        InGameCollectionUI.totalCoinCollectedinOneGame += 1000; // Increment total coins to 1000 for boss kill
        InGameCollectionUI.totalKillinOneGame += 1; // Increment total kills
        GameDataManager.AddCoins(randomCoins);
        StartCoroutine(waitForBossToDie()); // Wait for boss to die
        GameDataManager.AddZombieKill(1);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        stopWalkAudio = true;
        AudioManager.instance.BossButcherDiePlay();
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        isAttacking = false;
        Destroy(gameObject, 3); // Destroy the zombie
    }
    IEnumerator stopBossForAwhile()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }
    IEnumerator waitForBossToDie()
    {
        yield return new WaitForSeconds(2.5f);
        BossSpawner.isBossDefeated = true; // Set the flag to indicate the boss is defeated
        BossSpawner.isBossSpawnedOnce = false; // Reset the flag for boss spawn
    }
}
