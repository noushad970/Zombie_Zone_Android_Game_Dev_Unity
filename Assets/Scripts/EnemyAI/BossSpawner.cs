using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] bossPrefab;
    public static bool isBossSpawned = false; // Static variable to track if the boss is spawned
    public static bool isBossDefeated = false; // Static variable to track if the boss is defeated
    public static bool isBossSpawnedOnce = false; // Static variable to track if the boss has been spawned at least once

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bossSpawned(); // Continuously check if the boss should be spawned
    }
    public void bossSpawned()
    {
        if (isBossSpawned)
        {
            isBossSpawned = false; 
            int randomIndex = Random.Range(0, bossPrefab.Length); // Get a random index for the boss prefab
            Instantiate(bossPrefab[randomIndex], transform.position, Quaternion.identity); // Instantiate the boss prefab at the spawner's position
            isBossSpawnedOnce= true; // Set the flag to indicate that the boss has been spawned at least once
        }
    }
}
