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
        highscore = PlayerPrefs.GetFloat("Highscore", 0);

        StartCoroutine("WaitToPlaySoundtrack");
        inMainMenu = true;
        Time.timeScale = 0;
        player.GetComponent<Renderer>().enabled = false;
        scorePanel.SetActive(false);
        startPanel.SetActive(true);
        // Sets the score to give to 100, which can then be modified by missing or hitting shots.
        Shoot.scoreToGive = 100f;

        
    }

    IEnumerator WaitToPlaySoundtrack()
    {
            yield return new WaitForSecondsRealtime(5f);
            FindObjectOfType<AudioManager>().Play("Soundtrack");
    }

    public void StartGame()
    {
        isBlinking = false;
        lives = 3;

        FindObjectOfType<AudioManager>().Play("Coin");

        inGame = true;
        inMainMenu = false;

        Time.timeScale = 1;
        player.GetComponent<Renderer>().enabled = true;
        scorePanel.SetActive(true);
        startPanel.SetActive(false);
        fireSprite.GetComponent<Renderer>().enabled = false;
    }

    public void RestartGame()
    {
        lives = 3;

        FindObjectOfType<AudioManager>().Play("Coin");

        isBlinking = false;

        life1.enabled = true;
        life2.enabled = true;
        life3.enabled = true;

        inGame = true;
        inMainMenu = false;

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

    bool paused = false;

    bool gameOver; // True or false, is the game over? aka. has the player died?
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false){
                PauseGame();
            }
            else if (paused == true)
            {
                UnpauseGame();
            }
        }

        if (paused == false)
        {
            Fire();
        }

        if (gameOver == true) // Check if the game is over
        {
            return; // Do nothing
        }
        else if (paused == false)
        {
            Movement(); // Run the movement checks
        }
        
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(isBlinking == false && paused == false)
            {
                FindObjectOfType<AudioManager>().Play("Thrust");
            }
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            FindObjectOfType<AudioManager>().StopPlaying("Thrust");
        }


    }

    // Player movement

    public float thrust; // How much force is used when you press up arrow.
    public float topSpeed; // The maximum speed that the player can go.

    void Movement()
    {
        if (isBlinking == false && inMainMenu == false && gameOver == false && paused == false)
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
                if (isBlinking == false)
                {
                    player.AddForce(transform.up * thrust);
                    fireSprite.GetComponent<Renderer>().enabled = true;
                }
                else
                {
                    return;
                }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            fireSprite.GetComponent<Renderer>().enabled = false;
        }

        if (player.velocity.magnitude > topSpeed)
            player.velocity = player.velocity.normalized * topSpeed;
        }

        
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

    public int lives;

    public Image life1;
    public Image life2;
    public Image life3;

    public GameObject scoreText;
    public GameObject gameOverPanel;
    public Text endScoreText;
    public Text endHighscoreText;
    public Text highscoreMenu;
    public GameObject endScoreTextOBJ;
    public GameObject endHighscoreTextOBJ;

    public GameObject playerObject;
    public GameObject fireSprite;

    GameObject asteroidHit;
    GameObject killerAsteroid;

    void LoseLife()
    {
        isBlinking = false;
        lives = (lives - 1);
        DestroyAll();
        Time.timeScale = 1;
        loops = 0;

        if (lives == 2)
        {
           life1.enabled = false;
        } 
        else if (lives == 1)
        {
            life2.enabled = false;
        }
        else if (lives == 0)
        {
            life3.enabled = false;
        }

    }

    void EndGame()
    {
        gameOver = true;
        inGame = false;
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Asteroid" || col.gameObject.tag == "AsteroidSmall" || col.gameObject.tag == "AsteroidTiny" || col.gameObject.tag == "Bullet")
        {
            player.velocity = Vector2.zero;
            killerAsteroid = col.gameObject;
            Time.timeScale = 0;
            FindObjectOfType<AudioManager>().StopPlaying("Thrust");
            StartCoroutine("BlinkObj");
        }

        
    }

    int loops;
    bool isBlinking = false;

    IEnumerator BlinkObj()
    {
        isBlinking = true;

        if(lives > 0)
        {

            fireSprite.GetComponent<Renderer>().enabled = false;
            player.GetComponent<Renderer>().enabled = false;
            killerAsteroid.GetComponent<Renderer>().enabled = false;

            while (loops < 3)
            {
                yield return new WaitForSecondsRealtime(0.5f);
                player.GetComponent<Renderer>().enabled = true;
                killerAsteroid.GetComponent<Renderer>().enabled = true;
                yield return new WaitForSecondsRealtime(0.5f);
                player.GetComponent<Renderer>().enabled = false;
                killerAsteroid.GetComponent<Renderer>().enabled = false;

                loops++;
            }

            Vector3 restartPosition = new Vector3(0, 0, 0);
            player.transform.position = (restartPosition);
            player.transform.rotation = Quaternion.Euler(restartPosition.x, restartPosition.y, restartPosition.z);

            player.GetComponent<Renderer>().enabled = true;

            LoseLife();
            Debug.Log("Lives" + lives);
        }
        else
        {
            fireSprite.GetComponent<Renderer>().enabled = false;
            player.GetComponent<Renderer>().enabled = false;
            killerAsteroid.GetComponent<Renderer>().enabled = false;

            while (loops < 5)
            {
                yield return new WaitForSecondsRealtime(0.5f);
                player.GetComponent<Renderer>().enabled = true;
                killerAsteroid.GetComponent<Renderer>().enabled = true;
                yield return new WaitForSecondsRealtime(0.5f);
                player.GetComponent<Renderer>().enabled = false;
                killerAsteroid.GetComponent<Renderer>().enabled = false;

                loops++;
            }

            scoreText.SetActive(false);

            endScoreTextOBJ.SetActive(true);
            endScoreText.text = "Score: " + Shoot.score;

            HighscoreCheck();
            endHighscoreTextOBJ.SetActive(true);

            gameOverPanel.SetActive(true);

            isBlinking = false;

            gameOver = true;

            inGameOverMenu = true;

        }

    }

    float highscore;
    void HighscoreCheck()
    {
        if(Shoot.score >= highscore)
        {
            highscore = Shoot.score;
            PlayerPrefs.SetFloat("Highscore", highscore);
        }
        endHighscoreText.text = "Highscore: " + highscore;
        highscoreMenu.text = "Highscore: " + highscore;
    }


    // SHOOTING
    bool inGameOverMenu = false;
    bool inMainMenu = true;
    bool inGame = false;
    void Fire()
    {
        
        if (isBlinking == false && inMainMenu == false && gameOver == false)
        {
        
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (inGame == true)
                {
                    FindObjectOfType<AudioManager>().Play("Shoot");
                    Instantiate(bulletPrefab, shootLoc.position, shootLoc.rotation);
                }
            }
        
        }
        if (Input.GetKeyDown(KeyCode.Space))
            {
                if (inMainMenu == true)
                {
                    
                    StartGame();

                }
                if(inGameOverMenu == true)
                {
                    gameOverPanel.SetActive(false);
                    RestartGame();
                    inGameOverMenu = false;
                }
            }
        
    }


    // BACK TO MENU BUTTON

    public void ReturnToMenu()
    {
        UnpauseGame();

        life1.enabled = true;
        life2.enabled = true;
        life3.enabled = true;

        inGameOverMenu = false;
        inMainMenu = true;

        gameOverPanel.SetActive(false);
        startPanel.SetActive(true);

        Shoot.score = 0;
        

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



// PAUSE

    public GameObject pausedPanel;

    public GameObject three;
    public GameObject two;
    public GameObject one;

    void PauseGame(){
        pausedPanel.SetActive(true);
        Time.timeScale = 0;
        paused = true;
        fireSprite.GetComponent<Renderer>().enabled = false;
        FindObjectOfType<AudioManager>().StopPlaying("Thrust");
    }

    void UnpauseGame(){
        pausedPanel.SetActive(false);
        StartCoroutine("ResumeGame");
        
    }

    IEnumerator ResumeGame()
        {
                three.SetActive(true);
                yield return new WaitForSecondsRealtime(1f);
                three.SetActive(false);
                two.SetActive(true);
                yield return new WaitForSecondsRealtime(1f);
                two.SetActive(false);
                one.SetActive(true);
                yield return new WaitForSecondsRealtime(1f);
                one.SetActive(false);

                Time.timeScale = 1;
                paused = false;
        }

}