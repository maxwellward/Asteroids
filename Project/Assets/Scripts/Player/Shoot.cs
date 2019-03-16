﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    

    public float speed;

    float xLoc;
    float yLoc;



    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            kills = kills + 9;
            Debug.Log("Kills " + kills);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            kills = kills + 14;
            Debug.Log("Kills " + kills);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "DestroyBullet")
        {
            transform.position = gameObject.transform.position * -1;
            StartCoroutine("DestroyBullet");
        }
    }

    int kills;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Asteroid")
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);

            objectName = col.gameObject.name;

            asteroid = col.gameObject;

            BreakAsteroid();
            OnHit();

            kills++;
            CheckLevel();
        }
        else if (col.gameObject.tag == "AsteroidSmall")
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);

            objectName = col.gameObject.name;

            asteroid = col.gameObject;

            BreakAsteroidSmall();
            OnHit();
        }
        else if (col.gameObject.tag == "AsteroidTiny")
        {
            asteroid = col.gameObject;
            asteroidPosition = asteroid.transform.position;
            Destroy(this.gameObject);
            Destroy(col.gameObject);
            OnHit();
        }
    }

    int level;

    void CheckLevel()
    {
        if (kills >= 10 && level < 1)
        {

            Debug.Log("level 1 reached");

            level++;

            Enemies.minWait = 1;
            Enemies.maxWait = 2;

            kills = 0;

        } else if (kills >= 15 && level == 1)
        {
            level++;
            Debug.Log("level 2 reached");
        }
    }



    IEnumerator DestroyBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
            OnMiss();
        }
    }


    // ENEMY DESTROY MANAGEMENT

    string objectName;
    GameObject asteroid;

    public GameObject asteroidTwo;
    public GameObject asteroidThree;

    public static Vector3 asteroidPosition;
    
    public static float score;

    private Enemies enemyScript;

    void Start()
    {
        enemyScript = FindObjectOfType<Enemies>();
    }

    void BreakAsteroid()
    {
        asteroidPosition = asteroid.transform.position;



        Instantiate(asteroidTwo, asteroidPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        Instantiate(asteroidTwo, asteroidPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

    }

    void BreakAsteroidSmall()
    {
        asteroidPosition = asteroid.transform.position;

        Instantiate(asteroidThree, asteroidPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        Instantiate(asteroidThree, asteroidPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

    }


    public static float scoreToGive;


    void OnHit()
    {
        enemyScript.DisplayParticles();
        score = (score + scoreToGive);
        scoreToGive = Mathf.RoundToInt(scoreToGive * 1.2f);
    }

    void OnMiss()
    {
        scoreToGive = Mathf.RoundToInt(scoreToGive / 1.5f);
    }
}
