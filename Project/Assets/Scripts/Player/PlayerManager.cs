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

    public GameObject startPanel;
    public GameObject scorePanel;

    private Enemies enemyScript;

    // Start is run on game start.
    private void Start()
    {
        Time.timeScale = 0;
        player.GetComponent<Renderer>().enabled = false;
        scorePanel.SetActive(false);
        startPanel.SetActive(true);
        // Sets the score to give to 100, which can then be modified by missing or hitting shots.
        Shoot.scoreToGive = 100f;

        
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        player.GetComponent<Renderer>().enabled = true;
        scorePanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void RestartGame()
    {
        Shoot.score = 0;
        Time.timeScale = 1;
        player.GetComponent<Renderer>().enabled = true;
        gameOverPanel.SetActive(false);

        Vector3 restartPosition = new Vector3(0, 0, 0);
        player.transform.position = (restartPosition);
        player.transform.rotation = Quaternion.Euler(restartPosition.x, restartPosition.y, restartPosition.z);

        player.velocity = Vector3.zero;
        player.angularVelocity = 0;

        enemyScript = FindObjectOfType<Enemies>();
        enemyScript.StopParticles();

        gameOver = false;
        scoreText.SetActive(true);
        Shoot.scoreToGive = 100f;
        
        loops = 0;

        DestroyAll();
    }

    void DestroyAll()
    {
        GameObject [] foundBullets = GameObject.FindGameObjectsWithTag("Bullet");

	    if(foundBullets.Length != 0)
	    {
		    foreach( GameObject bulletToDestroy in foundBullets)
		    {
		    	Destroy(bulletToDestroy);
		    }
	    }
        
        GameObject [] foundEnemies = GameObject.FindGameObjectsWithTag("Asteroid");

	    if(foundEnemies.Length != 0)
	    {
		    foreach( GameObject asteroidToDestroy in foundEnemies)
		    {
		    	Destroy(asteroidToDestroy);
		    }
	    }

        GameObject [] foundEnemiesSmall = GameObject.FindGameObjectsWithTag("AsteroidSmall");

	    if(foundEnemiesSmall.Length != 0)
	    {
		    foreach( GameObject smallAsteroidToDestroy in foundEnemiesSmall)
		    {
		    	Destroy(smallAsteroidToDestroy);
		    }
	    }

        GameObject [] foundEnemiesTiny = GameObject.FindGameObjectsWithTag("AsteroidTiny");

	    if(foundEnemiesSmall.Length != 0)
	    {
		    foreach( GameObject tinyAsteroidToDestroy in foundEnemiesTiny)
		    {
		    	Destroy(tinyAsteroidToDestroy);
		    }
	    }
    }

    // Update is called every frame, so these functions are run and checked every frame (60 times a second usually).
    // Checks if the player is firing, and if they're allowed to move at the end of the game.

    bool gameOver; // True or false, is the game over? aka. has the player died?

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            DestroyAll();
        }

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
        Debug.Log("Run");
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
