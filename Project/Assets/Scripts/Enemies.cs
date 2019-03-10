using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{

    public float xMin = -6f;
    public float xMax = 6f;

    public float yMin = 3f;
    public float yMax = 3.5f;

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
        Vector2 spawnPoint = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));

        Instantiate(asteroidOne, spawnPoint, transform.rotation);
    }
}
