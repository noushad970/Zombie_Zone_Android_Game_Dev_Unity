using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    private int health = 100; // Player's health
    private int currentHealth;
    private PlayerMovement playerMovement; // Reference to PlayerMovement script
    public Animator anim; // Reference to Animator for animations

    public static int playerCurrentHealth;
    public static bool isPlayerDead = false,isGotoMainMenu=false,isRestartGame=false; // Static variable to check if player is dead

    public Image fillImage; // Assign in Inspector (must be Filled type)

    // Call this function and pass a value between 0 and 100
    public void UpdateFillBar(float amount)
    {
        // Clamp to prevent out-of-range issues
        amount = Mathf.Clamp(amount, 0f, 100f);

        // Convert 0–100 to 0–1
        float fillValue = amount / 100f;

        // Apply to the image
        fillImage.fillAmount = fillValue;
    }
    private void Start()
    {
        currentHealth = health; // Initialize current health
        playerMovement = GetComponent<PlayerMovement>(); // Get the PlayerMovement component
    }

    private void Update()
    {
        playerCurrentHealth= currentHealth; // Update static player current health
        UpdateFillBar(currentHealth); // Update the fill bar with current health
    }
    private void OnEnable()
    {
        InGameCollectionUI.totalCoinCollectedinOneGame = 0; // Reset total coins collected
        InGameCollectionUI.totalKillinOneGame = 0; // Reset total kills
        currentHealth = health; // Reset current health when enabled
        anim.Play("Idle"); // Play idle animation
        isPlayerDead = false;
        isGotoMainMenu = false; // Reset player dead status

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce current health by damage amount
        if (currentHealth <= 0)
        {
            Die(); // Call DieZombie method if health is zero or below
             }
            Debug.Log("Current Health: " + currentHealth); // Log current health
    }
    private void Die()
    {
        Debug.Log("Player has died!"); // Log death message
        playerMovement.enabled = false; // Disable player movement
        anim.Play("playerDied"); // Play death animation
        //Game Over with UI can be handled here
        StartCoroutine(playerDie()); // Start coroutine to handle player death
    }
    IEnumerator playerDie()
    {
        yield return new WaitForSeconds(3f);
        isPlayerDead = true; // Set player dead status

        playerMovement.enabled = true; // Disable player movement

    }
    public int getHealth() { 
    
        return currentHealth; // Return current health
    }
}
