using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    // Minimum and maximum spawn location values
    float xMinT = -4f;
    float xMaxT = 4f;
    float yMinT = 3.3f;
    float yMaxT = 3.5f;
    float xMinB = -4f;
    float xMaxB = 4f;
    float yMinB = -3.3f;
    float yMaxB = -3.5f;
    float xMinR = 6.09f;
    float xMaxR = 6.12f;
    float yMinR = 2f;
    float yMaxR = -2f;
    float xMinL = -5.5f;
    float xMaxL = -5.6f;
    float yMinL = -2f;
    float yMaxL = 2f;

    // Variables
    public float minWait = 6; // Set the minimum spawn wait time to 6 seconds
    public float maxWait = 8; // Set the maximum spawn wait time to 8 seconds
    public float speed = 5; // The speed of the asteroids
    public static float side; // What side to spawn the asteroid on
    Vector2 spawnPoint; // Where to spawn the asteroid
    public GameObject asteroidOne; // The asteroid object
    public GameObject particleObject; // The particle object
    public ParticleSystem explosionParticles; // The particle system for the explosion particles
    
    // External script refrences    
    private Enemies enemyScript;
    private Shoot shootScript;
    private PlayerManager playerScript;

    // Start is run when the game starts
    void Start()
    {
        shootScript = FindObjectOfType<Shoot>(); // Initalize the shoot script reference.
        side = Random.Range(1, 5); // Pick a side to spawn the asteroid on
        StartCoroutine("SpawnEnemyTimer"); // Start the enemy spawning routine
    }

    // A function that allows for wait times
    IEnumerator SpawnEnemyTimer() 
    {
        yield return new WaitForSeconds(0.5f); // Wait half a second
        SpawnEnemy(); // Spawn an inital enemy before and then move to the wait time

        while (true) // While (true) will run forever, because true is always true.
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait)); // Wait for a random time between the maximum wait and minimum wait.
            side = Random.Range(1, 5); // Pick a random side
            SpawnEnemy(); // Run the function to spawn an enemy
        }
    }

    // Spawns an enemy, is run at a random time between 6 and 8 seconds.
    void SpawnEnemy()
    {
        if (side == 1) // Checks what side was randomly selected
        {
            // Select a random location along that side to spawn the asteroid
            Vector2 spawnPointTop = new Vector2(Random.Range(xMinT, xMaxT), Random.Range(yMinT, yMaxT));
            // Create a large asteroid object and apply a random rotation inward
            // The force applied is from the EnemyAddForce script
            Instantiate(asteroidOne, spawnPointTop, Quaternion.Euler(new Vector3(0, 0, Random.Range(135, 240))));
        }
        else if (side == 2)
        {
            Vector2 spawnPointBottom = new Vector2(Random.Range(xMinB, xMaxB), Random.Range(yMinB, yMaxB));
            Instantiate(asteroidOne, spawnPointBottom, Quaternion.Euler(new Vector3(0, 0, Random.Range(40, -30))));
        }
        else if (side == 3)
        {
            Vector2 spawnPointRight = new Vector2(Random.Range(xMinR, xMaxR), Random.Range(yMinR, yMaxR));
            Instantiate(asteroidOne, spawnPointRight, Quaternion.Euler(new Vector3(0, 0, Random.Range(120, 35))));
        }
        else if (side == 4)
        {
            Vector2 spawnPointLeft = new Vector2(Random.Range(xMinL, xMaxL), Random.Range(yMinL, yMaxL));
            Instantiate(asteroidOne, spawnPointLeft, Quaternion.Euler(new Vector3(0, 0, Random.Range(-120, -35))));
        }
    }
    
    // Displays the particles when destroying an enemy.
    // It is run from the Shoot script in the "OnHit" functions.
    public void DisplayParticles()
    {
        particleObject.transform.position = Shoot.asteroidPosition; // Set the location of the particles to the asteroid that was destroyed
        explosionParticles.Play(); // Display the particle
    }
    
    // Stop the particles
    public void StopParticles()
    {
        explosionParticles.Clear(); // Clear the particles from the screen
    }
}
