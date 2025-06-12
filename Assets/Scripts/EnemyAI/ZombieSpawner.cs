using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // Assign the zombie prefab in the Inspector
    public Transform[] spawnPoints; // Define spawn points in the scene
    public float spawnInterval = 5f; // Time between spawns
    public int maxZombies = 5; // Maximum number of zombies allowed in the scene
    public float spawnRadius = 5f; // Radius to detect the player

    private int currentZombieCount = 0; // Tracks the current number of zombies
    private bool isPlayerInRange = false; // Tracks if the player is in range
    private GameObject player; // Reference to the player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player by tag
    }

    void Update()
    {
        // Continuously check if the player is within the spawn radius
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= spawnRadius && !isPlayerInRange)
            {
                isPlayerInRange = true;
                StartSpawning();
            }
            else if (distanceToPlayer > spawnRadius && isPlayerInRange)
            {
                isPlayerInRange = false;
                StopSpawning();
            }
        }
    }

    void StartSpawning()
    {
        // Start spawning zombies
        InvokeRepeating(nameof(SpawnZombie), spawnInterval, spawnInterval);
    }

    void StopSpawning()
    {
        // Stop spawning zombies
        CancelInvoke(nameof(SpawnZombie));
    }

    void SpawnZombie()
    {
        // Check if maximum zombies are already spawned
        if (currentZombieCount >= maxZombies)
        {
            return;
        }

        // Choose a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Spawn the zombie
        Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);

        // Increase the current zombie count
        currentZombieCount++;
    }

    public void ZombieDestroyed()
    {
        // Decrease the zombie count when a zombie is destroyed
        currentZombieCount--;
    }

   
}
