using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private int health = 100; // Player's health
    private int currentHealth;
    private PlayerMovement playerMovement; // Reference to PlayerMovement script
    public Animator anim; // Reference to Animator for animations

    public static int playerCurrentHealth;
    public static bool isPlayerDead = false; // Static variable to check if player is dead
    private void Start()
    {
        currentHealth = health; // Initialize current health
        playerMovement = GetComponent<PlayerMovement>(); // Get the PlayerMovement component
    }

    private void Update()
    {
        playerCurrentHealth= currentHealth; // Update static player current health
    }
    private void OnEnable()
    {
        currentHealth = health; // Reset current health when enabled
        anim.Play("Idle"); // Play idle animation
        isPlayerDead = false;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce current health by damage amount
        if (currentHealth <= 0)
        {
            Die(); // Call Die method if health is zero or below
            isPlayerDead= true; // Set player dead status
        }
            Debug.Log("Current Health: " + currentHealth); // Log current health
    }
    private void Die()
    {
        Debug.Log("Player has died!"); // Log death message
        playerMovement.enabled = false; // Disable player movement
        anim.Play("playerDied"); // Play death animation
        //Game Over with UI can be handled here
    }
    public int getHealth() { 
    
        return currentHealth; // Return current health
    }
}
