using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    float xMinT = -4f;
    float xMaxT = 4f;
    float yMinT = 3f;
    float yMaxT = 3.5f;

    float xMinB = -4f;
    float xMaxB = 4f;
    float yMinB = -3f;
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

    void Start()
    {
        side = Random.Range(1, 5);
        SpawnEnemy();
        minWait = 10;
        maxWait = 16;
        StartCoroutine("SpawnEnemyTimer");
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            explosionParticles.Play();
            side = Random.Range(1, 5);
            SpawnEnemy();
        }
    }

    public static float minWait;
    public static float maxWait;

    IEnumerator SpawnEnemyTimer()
    {
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

    public void DisplayParticles()
    {
        particleObject.transform.position = Shoot.asteroidPosition;
        explosionParticles.Play();
    }

}
