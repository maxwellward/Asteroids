using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{

    float xMinT = -6f;
    float xMaxT = 6f;
    float yMinT = 3f;
    float yMaxT = 3.5f;

    float xMinB = -6f;
    float xMaxB = 6f;
    float yMinB = -3f;
    float yMaxB = -3.5f;

    float xMinR = 6.09f;
    float xMaxR = 6.12f;
    float yMinR = 3f;
    float yMaxR = -3f;

    float xMinL = -6.09f;
    float xMaxL = -6.12f;
    float yMinL = 3f;
    float yMaxL = -3.5f;

    Vector2 spawnPoint;

    //Asteroids

    public GameObject asteroidOne;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPointTop = new Vector2(Random.Range(xMinT, xMaxT), Random.Range(yMinT, yMaxT));
        Vector2 spawnPointBottom = new Vector2(Random.Range(xMinB, xMaxB), Random.Range(yMinB, yMaxB));
        Vector2 spawnPointRight = new Vector2(Random.Range(xMinR, xMaxR), Random.Range(yMinR, yMaxR));
        Vector2 spawnPointLeft = new Vector2(Random.Range(xMinL, xMaxL), Random.Range(yMinL, yMaxL));

        Instantiate(asteroidOne, spawnPointTop, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        Instantiate(asteroidOne, spawnPointBottom, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        Instantiate(asteroidOne, spawnPointRight, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        Instantiate(asteroidOne, spawnPointLeft, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
    }
}
