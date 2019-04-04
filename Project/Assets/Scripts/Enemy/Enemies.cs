using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
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

    public float speed = 5;
    public static float side;
    Vector2 spawnPoint;

    //Asteroids

    public GameObject asteroidOne;

    private Enemies enemyScript;
    private Shoot shootScript;

    private PlayerManager playerScript;

    void Start()
    {
        shootScript = FindObjectOfType<Shoot>();
        

        side = Random.Range(1, 5);
        minWait = 6;
        maxWait = 8;
        StartCoroutine("SpawnEnemyTimer");
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            side = Random.Range(1, 5);
            SpawnEnemy();
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    kills = kills + 10;
        //    Debug.Log(kills);
        //}
    }

    public float minWait;
    public float maxWait;

    IEnumerator SpawnEnemyTimer()
    {
        yield return new WaitForSeconds(2f);

        SpawnEnemy();

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            side = Random.Range(1, 5);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {

        Vector2 spawnPointTop = new Vector2(Random.Range(xMinT, xMaxT), Random.Range(yMinT, yMaxT));
        Vector2 spawnPointBottom = new Vector2(Random.Range(xMinB, xMaxB), Random.Range(yMinB, yMaxB));
        Vector2 spawnPointRight = new Vector2(Random.Range(xMinR, xMaxR), Random.Range(yMinR, yMaxR));
        Vector2 spawnPointLeft = new Vector2(Random.Range(xMinL, xMaxL), Random.Range(yMinL, yMaxL));

        if (side == 1)
        {
            Instantiate(asteroidOne, spawnPointTop, Quaternion.Euler(new Vector3(0, 0, Random.Range(135, 240))));
        }
        else if (side == 2)
        {
            Instantiate(asteroidOne, spawnPointBottom, Quaternion.Euler(new Vector3(0, 0, Random.Range(40, -30))));
        }
        else if (side == 3)
        {
            Instantiate(asteroidOne, spawnPointRight, Quaternion.Euler(new Vector3(0, 0, Random.Range(120, 35))));
        }
        else if (side == 4)
        {
            Instantiate(asteroidOne, spawnPointLeft, Quaternion.Euler(new Vector3(0, 0, Random.Range(-120, -35))));
        }
    }

    public GameObject particleObject;
    public ParticleSystem explosionParticles;

    public void StopParticles()
    {
        explosionParticles.Clear();
    }

    public void DisplayParticles()
    {
        particleObject.transform.position = Shoot.asteroidPosition;
        explosionParticles.Play();
    }


    // LEVEL MANAGEMENT
    /* public int kills;
    int level;

    void CheckLevel()
    {
        if (kills >= 10 && level < 1)
        {

            Debug.Log("level 1 reached");

            level++;

            minWait = 1;
            maxWait = 2;

            kills = 0;

        } else if (kills >= 15 && level == 1)
        {
            level++;
            Debug.Log("level 2 reached");
        }
    }
*/
}
