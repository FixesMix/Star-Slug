using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 30f; // Maximum health of the player
    public float currentHealth; // Current health of the player
    public bool isAlive = true; // Flag to track player's alive state

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health
        isAlive = true; // Set the player as alive at the start
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            // Decrease player's health over time (for demonstration purposes)
            DecreaseHealthOverTime();
        }
    }

    // Function to decrease player's health over time
    void DecreaseHealthOverTime()
    {
        // Adjust this value as needed for the rate of health decrease
        float healthDecreaseRate = 10f; // Health decrease rate per second

        // Decrease player's health based on the decrease rate and time elapsed
        currentHealth -= healthDecreaseRate * Time.deltaTime;

        // Check if player's health is zero or less
        if (currentHealth <= 0)
        {
            currentHealth = 0; // Ensure health doesn't go below zero
            Die(); // Call function to handle player's death
        }
    }

   

    // Function to handle player's death
    void Die()
    {
        isAlive = false; // Set isAlive flag to false
        // Add code here to trigger death animation, play sound, or perform other actions
        Debug.Log("Player has died."); // For debugging purposes
    }
}