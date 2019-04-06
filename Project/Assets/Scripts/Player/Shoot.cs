using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    private Enemies enemyScript;

    void Start()
    {
        enemyScript = FindObjectOfType<Enemies>();
    }

    public float speed;

    float xLoc;
    float yLoc;



    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "DestroyBullet")
        {
            transform.position = gameObject.transform.position * -1;
            StartCoroutine("DestroyBullet");
        }
    }

    

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

   //         Enemies.kills++;
//            Enemies.CheckLevel();
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
            FindObjectOfType<AudioManager>().Play("Small");
            asteroid = col.gameObject;
            asteroidPosition = asteroid.transform.position;
            Destroy(this.gameObject);
            Destroy(col.gameObject);
            OnHit();
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



    void BreakAsteroid()
    {
        asteroidPosition = asteroid.transform.position;

        FindObjectOfType<AudioManager>().Play("Small");

        Instantiate(asteroidTwo, asteroidPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        Instantiate(asteroidTwo, asteroidPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

    }

    void BreakAsteroidSmall()
    {
        asteroidPosition = asteroid.transform.position;

        FindObjectOfType<AudioManager>().Play("Medium");

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
