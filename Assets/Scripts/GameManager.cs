using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // VARIABLES
    public int lives;
    public Image life1;
	public Image life2;
	public Image life3;

    public bool isBlinking = false;
    public bool inGame = false;
    public bool inMainMenu = true;
    public bool gameOver;
	public bool paused = false;

    public int loops;

    // SCRIPT REFRENCES
    private UI userInterface;
    private PlayerManager playerScript;
    private Enemies enemyScript;

    // ON START

    void Start(){
        // SCRIPT REFRENCES
        userInterface = FindObjectOfType<UI>();
        playerScript = FindObjectOfType<PlayerManager>();
        enemyScript = FindObjectOfType<Enemies>();   
    }

    // STATISTICS
    
    public float stats_highscore;
	public int stats_shotsFired;
	public int stats_gamesPlayed;

    public void UpdateStats()
	{
		userInterface = FindObjectOfType<UI>();
        playerScript = FindObjectOfType<PlayerManager>();
		userInterface.text_highscore.text = playerScript.highscore.ToString();
		userInterface.text_shotsFired.text = stats_shotsFired.ToString();
		userInterface.text_gamesPlayed.text = stats_gamesPlayed.ToString();
	}

    // QUIT GAME

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        PlayerPrefs.SetFloat("GamesPlayed", stats_gamesPlayed);
        PlayerPrefs.SetFloat("ShotsFired", stats_shotsFired);
        Debug.Log("Set values");
        Application.Quit();
    }

    // START GAME

    public void StartGame()
	{
		isBlinking = false;
		lives = 3; // CHANGE THIS TO 3

		FindObjectOfType<AudioManager>().Play("Coin");

		inGame = true;
		inMainMenu = false;

		Time.timeScale = 1;
		playerScript.player.GetComponent<Renderer>().enabled = true;
		userInterface.scorePanel.SetActive(true);
		userInterface.startPanel.SetActive(false);
		playerScript.fireSprite.GetComponent<Renderer>().enabled = false;
	}

    // RESTART GAME

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
		playerScript.player.GetComponent<Renderer>().enabled = true;
		userInterface.gameOverPanel.SetActive(false);

		Vector3 restartPosition = new Vector3(0, 0, 0);
		playerScript.player.transform.position = (restartPosition);
		playerScript.player.transform.rotation = Quaternion.Euler(restartPosition.x, restartPosition.y, restartPosition.z);

		playerScript.player.velocity = Vector3.zero;
		playerScript.player.angularVelocity = 0;

		enemyScript = FindObjectOfType<Enemies>();
		enemyScript.StopParticles();

		gameOver = false;
		userInterface.scoreTextObj.SetActive(true);
		Shoot.scoreToGive = 100f;
		
		loops = 0;

		playerScript.DestroyAll();
	}
}
