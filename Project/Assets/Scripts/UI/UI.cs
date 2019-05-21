using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    private Enemies enemyScript;
    private GameManager gameManagerScript;

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

        // Easter egg
        egg_buttonToCatch.gameObject.SetActive(false);
        egg_linkText.SetActive(false);

        // Console
        DeveloperConsole.SetActive(false);
        consoleActive = false;
    }

    bool consoleActive = false;
    public string output;
    public InputField ConsoleInput;
    public GameObject DeveloperConsole;


    void Update()
    {
        
        // Developer Console
        if(Input.GetKeyDown(KeyCode.Q))
        {

            if (consoleActive == false)
            {
                DeveloperConsole.SetActive(true);
                consoleActive = true;
            }
            else
            {
                DeveloperConsole.SetActive(false);
                consoleActive = false;
            }

        }
        if (consoleActive == true)
        {
            
            if (Input.GetKeyDown(KeyCode.Return))
            {
                output = ConsoleInput.text; 
                ConsoleInput.text = "";
                InputCheck();
            }
        }

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


// EASTER EGG #1

    public GameObject egg_linkText;

    public Transform egg_asteroidToExpand;
    int egg_timesPressed;
    public void EasterEgg()
    {
        egg_timesPressed++;
        if (egg_timesPressed == 120) // CHANGE THIS TO 120
        {
            StartCoroutine("egg_TeleportBall");
            egg_buttonToCatch.gameObject.SetActive(true);
            Destroy(egg_asteroidToExpand.gameObject);
        }

        egg_asteroidToExpand.localScale += new Vector3(1, 1, 1);
    }

// Catch me ball
    bool ballCaught = false;
    public Transform egg_buttonToCatch;

    public void egg_BallCaught()
    {
        ballCaught = true;
        Debug.Log("nice");
        Destroy(egg_buttonToCatch.gameObject);
        egg_linkText.SetActive(true);
    }

    IEnumerator egg_TeleportBall()
    {
        while(ballCaught == false){
            if (ballCaught == false)
            {
                yield return new WaitForSecondsRealtime(1f);
                egg_buttonToCatch.transform.position = new Vector3(Random.Range(-684, 857), Random.Range(415, -415), 0);
            }
        }
    }
    
// ----




// DEV CONSOLE COMMANDS
// DEV CONSOLE COMMANDS
// DEV CONSOLE COMMANDS

    void InputCheck()
    {
        if (output == "hello")
        {
            Debug.Log("yeet");
        }    
    }

// MENUS

    public void ReturnToMenu()
	{
		playerScript.pausedPanel.SetActive(false);
		Time.timeScale = 1;
		playerScript.paused = false;
		playerScript.resumingGame = false;

		playerScript.life1.enabled = true;
		playerScript.life2.enabled = true;
		playerScript.life3.enabled = true;

		playerScript.inGameOverMenu = false;
		playerScript.inMainMenu = true;

		playerScript.gameOverPanel.SetActive(false);
		playerScript.startPanel.SetActive(true);

		Shoot.score = 0;
		

		Vector3 restartPosition = new Vector3(0, 0, 0);
		playerScript.player.transform.position = (restartPosition);
		playerScript.player.transform.rotation = Quaternion.Euler(restartPosition.x, restartPosition.y, restartPosition.z);

		playerScript.player.velocity = Vector3.zero;
		playerScript.player.angularVelocity = 0;

		enemyScript = FindObjectOfType<Enemies>();
		enemyScript.StopParticles();

		playerScript.gameOver = false;
		playerScript.scoreText.SetActive(true);
		Shoot.scoreToGive = 100f;
		
		playerScript.loops = 0;

		playerScript.DestroyAll();

		gameManagerScript.stats_gamesPlayed++;
		PlayerPrefs.SetInt("stats_gamesPlayed", gameManagerScript.stats_gamesPlayed);
		gameManagerScript.UpdateStats();
	}


}
