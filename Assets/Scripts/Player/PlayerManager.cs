using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

	private UI userInterface;

	public float speed;
	public GameObject bulletPrefab;
	public Transform shootLoc;

	
	public Rigidbody2D player; // A refrence to the player object.

	public GameManager gameManagerScript;

	

	// Start is run on game start.
	private void Start()
	{
		userInterface = FindObjectOfType<UI>();        

		highscore = PlayerPrefs.GetFloat("Highscore");
		gameManagerScript.stats_shotsFired = PlayerPrefs.GetInt("stats_shotsFired");
		gameManagerScript.stats_gamesPlayed = PlayerPrefs.GetInt("stats_gamesPlayed");
		
		gameManagerScript.UpdateStats();

		StartCoroutine("WaitToPlaySoundtrack");
		gameManagerScript.inMainMenu = true;
		Time.timeScale = 0;
		player.GetComponent<Renderer>().enabled = false;
		userInterface.scorePanel.SetActive(false);
		userInterface.startPanel.SetActive(true);
		// Sets the score to give to 100, which can then be modified by missing or hitting shots.
		Shoot.scoreToGive = 100f;

		
	}

	IEnumerator WaitToPlaySoundtrack()
	{
			yield return new WaitForSecondsRealtime(5f);
			FindObjectOfType<AudioManager>().Play("Soundtrack");
	}


	public void DestroyAll()
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

	

	
	void Update()
	{



		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (gameManagerScript.paused == false && resumingGame == false){
				PauseGame();
			}
			else if (gameManagerScript.paused == true && resumingGame == false)
			{
				UnpauseGame();
			}
		}

		if (gameManagerScript.paused == false)
		{
			Fire();
		}

		if (gameManagerScript.gameOver == true) // Check if the game is over
		{
			return; // Do nothing
		}
		else if (gameManagerScript.paused == false)
		{
			Movement(); // Run the movement checks
		}
		
		
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if(gameManagerScript.isBlinking == false && gameManagerScript.paused == false)
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
	public float rotationalSpeed; // The speed at which the player rotates

	void Movement()
	{
		if (gameManagerScript.isBlinking == false && gameManagerScript.inMainMenu == false && gameManagerScript.gameOver == false && gameManagerScript.paused == false)
		{
			// Right turn
			if (Input.GetKey(KeyCode.RightArrow))
			{
				transform.Rotate(0, 0, -rotationalSpeed, Space.World);
			}
			// Left turn
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				transform.Rotate(0, 0, rotationalSpeed, Space.World);
			}
			// Forward Thrust
			if (Input.GetKey(KeyCode.UpArrow))
			{
				if (gameManagerScript.isBlinking == false)
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
		gameManagerScript.isBlinking = false;
		gameManagerScript.lives = gameManagerScript.lives - 1;
		DestroyAll();
		Time.timeScale = 1;
		gameManagerScript.loops = 0;

		if (gameManagerScript.lives == 2)
		{
			gameManagerScript.life1.enabled = false;
		} 
		else if (gameManagerScript.lives == 1)
		{
			gameManagerScript.life2.enabled = false;
		}
		else if (gameManagerScript.lives == 0)
		{
			gameManagerScript.life3.enabled = false;
		}

	}

	void OnCollisionEnter2D(Collision2D col)
	{

		if(gameManagerScript.gameOver == false)
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
		
	}

	
	

	IEnumerator BlinkObj()
	{
		gameManagerScript.isBlinking = true;

		if(gameManagerScript.lives > 0)
		{

			fireSprite.GetComponent<Renderer>().enabled = false;
			player.GetComponent<Renderer>().enabled = false;
			killerAsteroid.GetComponent<Renderer>().enabled = false;

			while (gameManagerScript.loops < 3)
			{
				yield return new WaitForSecondsRealtime(0.5f);
				player.GetComponent<Renderer>().enabled = true;
				killerAsteroid.GetComponent<Renderer>().enabled = true;
				yield return new WaitForSecondsRealtime(0.5f);
				player.GetComponent<Renderer>().enabled = false;
				killerAsteroid.GetComponent<Renderer>().enabled = false;

				gameManagerScript.loops++;
			}

			Vector3 restartPosition = new Vector3(0, 0, 0);
			player.transform.position = (restartPosition);
			player.transform.rotation = Quaternion.Euler(restartPosition.x, restartPosition.y, restartPosition.z);

			player.GetComponent<Renderer>().enabled = true;

			LoseLife();
			Debug.Log("Lives" + gameManagerScript.lives);
		}
		else
		{
			gameManagerScript.gameOver = true;

			fireSprite.GetComponent<Renderer>().enabled = false;
			player.GetComponent<Renderer>().enabled = false;
			killerAsteroid.GetComponent<Renderer>().enabled = false;

			while (gameManagerScript.loops < 5)
			{
				yield return new WaitForSecondsRealtime(0.5f);
				player.GetComponent<Renderer>().enabled = true;
				killerAsteroid.GetComponent<Renderer>().enabled = true;
				yield return new WaitForSecondsRealtime(0.5f);
				player.GetComponent<Renderer>().enabled = false;
				killerAsteroid.GetComponent<Renderer>().enabled = false;

				gameManagerScript.loops++;
			}

			gameManagerScript.stats_gamesPlayed++;
			PlayerPrefs.SetInt("stats_shotsFired", gameManagerScript.stats_shotsFired);
			PlayerPrefs.SetInt("stats_gamesPlayed", gameManagerScript.stats_gamesPlayed);
			gameManagerScript.UpdateStats();

			// FIX THIS
			//userInterface.scoreText.SetActive(false);

			endScoreTextOBJ.SetActive(true);
			endScoreText.text = "Score: " + Shoot.score;

			HighscoreCheck();
			endHighscoreTextOBJ.SetActive(true);

			userInterface.gameOverPanel.SetActive(true);

			gameManagerScript.isBlinking = false;

			

			inGameOverMenu = true;
		}

	}

	public float highscore;
	void HighscoreCheck()
	{
		if(Shoot.score >= highscore)
		{
			highscore = Shoot.score;
			PlayerPrefs.SetFloat("Highscore", highscore);
			userInterface.text_highscore.text = highscore.ToString();
		}
		endHighscoreText.text = "Highscore: " + highscore;
		highscoreMenu.text = "Highscore: " + highscore;
	}


	// SHOOTING
	public bool inGameOverMenu = false;
	
	
	void Fire()
	{
		
		if (gameManagerScript.isBlinking == false && gameManagerScript.inMainMenu == false && gameManagerScript.gameOver == false)
		{
		
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (gameManagerScript.inGame == true)
				{
					gameManagerScript.stats_shotsFired++;
					PlayerPrefs.SetInt("stats_shotsFired", gameManagerScript.stats_shotsFired);
					FindObjectOfType<AudioManager>().Play("Shoot");
					Instantiate(bulletPrefab, shootLoc.position, shootLoc.rotation);
				}
			}
		
		}
		if (Input.GetKeyDown(KeyCode.Space))
			{
				if (gameManagerScript.inMainMenu == true)
				{

				gameManagerScript.StartGame();

				}
				if(inGameOverMenu == true)
				{
					userInterface.gameOverPanel.SetActive(false);
					gameManagerScript.RestartGame();
					inGameOverMenu = false;
				}
			}
		
	}


	// BACK TO MENU BUTTON.

	



// PAUSE

	public GameObject pausedPanel;

	public GameObject three;
	public GameObject two;
	public GameObject one;

	void PauseGame(){
		if (resumingGame == false)
		{
			pausedPanel.SetActive(true);
			Time.timeScale = 0;
			gameManagerScript.paused = true;
			fireSprite.GetComponent<Renderer>().enabled = false;
			FindObjectOfType<AudioManager>().StopPlaying("Thrust");
		}	
	}

	void UnpauseGame(){
		pausedPanel.SetActive(false);
		StartCoroutine("ResumeGame");
		
	}

	public bool resumingGame = false;
	IEnumerator ResumeGame()
		{
				resumingGame = true;

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
				gameManagerScript.paused = false;
				resumingGame = false;
		}
}