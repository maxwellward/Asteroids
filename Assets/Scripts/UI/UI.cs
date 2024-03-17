using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    private Enemies enemyScript;
    private GameManager gameManagerScript;
    private UI userInterface;

    // SCORE
    public Text scoreText;
    public GameObject scoreTextObj;

    // STATS
    public Text text_highscore;
    public Text text_shotsFired;
    public Text text_asteroidsHit;
    public Text text_asteroidsMissed;
    public Text text_gamesPlayed;

    // GAME MANAGER
    public GameObject startPanel;
	public GameObject scorePanel;
    public GameObject gameOverPanel;


    void Start()
    {

        enemyScript = FindObjectOfType<Enemies>();
        // Stats
        gameManagerScript = FindObjectOfType<GameManager>();
        playerScript = FindObjectOfType<PlayerManager>();
    }

    void Update()
    {
        // Score
        scoreText.text = "Score: " + Shoot.score;
    }

    

// STATS PAGE

    private PlayerManager playerScript;

    public GameObject statsCanvas;

    public void LoadStatsPage()
    {
        startPanel.SetActive(false);
        statsCanvas.SetActive(true);
    }

    public void stats_ReturnToMenu()
    {
        startPanel.SetActive(true);
        statsCanvas.SetActive(false);
    }

    public GameObject resetButton;
    public GameObject confirmButton;
    bool stats_confirmStage = false;
    public void stats_resetStats()
    {
        if (stats_confirmStage == false)
        {
            resetButton.SetActive(false);
            confirmButton.SetActive(true);
            stats_confirmStage = true;
        }
        else
        {
            gameManagerScript.stats_gamesPlayed = 0;
			gameManagerScript.stats_shotsFired = 0;
			playerScript.highscore = 0;
            
			gameManagerScript.UpdateStats();

            stats_confirmStage = false;
            resetButton.SetActive(true);
            confirmButton.SetActive(false);

            PlayerPrefs.SetFloat("Highscore", playerScript.highscore);
		    PlayerPrefs.SetInt("stats_shotsFired", gameManagerScript.stats_shotsFired);
		    PlayerPrefs.SetInt("stats_gamesPlayed", gameManagerScript.stats_gamesPlayed);
        }
    }


// MENUS

    public void ReturnToMenu()
	{
		playerScript.pausedPanel.SetActive(false);
		Time.timeScale = 1;
        gameManagerScript.paused = false;
		playerScript.resumingGame = false;

        gameManagerScript.life1.enabled = true;
        gameManagerScript.life2.enabled = true;
        gameManagerScript.life3.enabled = true;

		playerScript.inGameOverMenu = false;
        gameManagerScript.inMainMenu = true;

        userInterface.gameOverPanel.SetActive(false);
        userInterface.startPanel.SetActive(true);

		Shoot.score = 0;
		

		Vector3 restartPosition = new Vector3(0, 0, 0);
		playerScript.player.transform.position = (restartPosition);
		playerScript.player.transform.rotation = Quaternion.Euler(restartPosition.x, restartPosition.y, restartPosition.z);

		playerScript.player.velocity = Vector3.zero;
		playerScript.player.angularVelocity = 0;

		enemyScript = FindObjectOfType<Enemies>();
		enemyScript.StopParticles();

		gameManagerScript.gameOver = false;
        //userInterface.scoreText.SetActive(true);
		Shoot.scoreToGive = 100f;

        gameManagerScript.loops = 0;

		playerScript.DestroyAll();

		gameManagerScript.stats_gamesPlayed++;
		PlayerPrefs.SetInt("stats_gamesPlayed", gameManagerScript.stats_gamesPlayed);
		gameManagerScript.UpdateStats();
	}


}
