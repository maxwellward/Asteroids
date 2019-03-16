using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float speed;
    public GameObject bulletPrefab;
    public Transform shootLoc;

    
    public Rigidbody2D player; // A refrence to the player object.

    // Start is run on game start.
    private void Start()
    {
        // Sets the score to give to 100, which can then be modified by missing or hitting shots.
        Shoot.scoreToGive = 100f;
    }

    // Update is called every frame, so these functions are run and checked every frame (60 times a second usually).
    // Checks if the player is firing, and if they're allowed to move at the end of the game.

    bool gameOver; // True or false, is the game over? aka. has the player died?

    void Update()
    {
        Fire();
        if (gameOver == true) // Check if the game is over
        {
            return; // Do nothing
        }
        else
        {
            Movement(); // Run the movement checks
        }
        
    }

    // Player movement

    public float thrust; // How much force is used when you press up arrow.
    public float topSpeed; // The maximum speed that the player can go.

    void Movement()
    {
        // Right turn
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -3, Space.World);
        }
        // Left turn
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, 3, Space.World);
        }
        // Forward Thrust
        if (Input.GetKey(KeyCode.UpArrow))
        {
            player.AddForce(transform.up * thrust);
        }

        if (player.velocity.magnitude > topSpeed)
            player.velocity = player.velocity.normalized * topSpeed;
    }

// Teleport player when they leave the arena
    void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.tag == "TeleportPlayer")
        {
            //StartCoroutine("RemoveCollidersPlayer");
            transform.position = gameObject.transform.position * -1;
        }
   
    }


    // PLAYER DEATH

    public GameObject scoreText;
    public GameObject gameOverPanel;
    public Text endScoreText;
    public GameObject endScoreTextOBJ;

    public GameObject playerObject;

    GameObject asteroidHit;

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Asteroid" || col.gameObject.tag == "AsteroidSmall" || col.gameObject.tag == "AsteroidTiny" || col.gameObject.tag == "Bullet")
        {
            Time.timeScale = 0;
            gameOver = true;
            StartCoroutine("BlinkPlayer");
        }
    }


    int loops;

    IEnumerator BlinkPlayer()
    {
        player.GetComponent<Renderer>().enabled = false;

        while (loops < 5)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            player.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSecondsRealtime(0.5f);
            player.GetComponent<Renderer>().enabled = false;

            loops++;
        }

        Destroy(this.gameObject);

        scoreText.SetActive(false);

        endScoreTextOBJ.SetActive(true);
        endScoreText.text = "Score: " + Shoot.score;

        gameOverPanel.SetActive(true);

    }

    // SHOOTING
    void Fire()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, shootLoc.position, shootLoc.rotation);
        }

    }
}
