using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private int health = 100; // Player's health
    private int currentHealth;
    private PlayerMovement playerMovement; // Reference to PlayerMovement script
    public Animator anim; // Reference to Animator for animations
    private void Start()
    {
        currentHealth = health; // Initialize current health
        playerMovement = GetComponent<PlayerMovement>(); // Get the PlayerMovement component
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce current health by damage amount
        if (currentHealth <= 0)
        {
            Die(); // Call Die method if health is zero or below
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
