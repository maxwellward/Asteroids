using System.Collections;
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
        }
        else if (col.gameObject.tag == "AsteroidSmall")
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);

            objectName = col.gameObject.name;

            asteroid = col.gameObject;

            BreakAsteroidSmall();
        }
        else if (col.gameObject.tag == "AsteroidTiny")
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);
            score = score + 3;
        }
    }





    IEnumerator DestroyBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
        }
    }


    // ENEMY DESTROY MANAGEMENT

    string objectName;
    GameObject asteroid;

    public GameObject asteroidTwo;
    public GameObject asteroidThree;

    Vector3 asteroidPosition;

    public static int score;

    void BreakAsteroid()
    {
        asteroidPosition = asteroid.transform.position;

        Instantiate(asteroidTwo, asteroidPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        Instantiate(asteroidTwo, asteroidPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        score = score + 1;
    }

    void BreakAsteroidSmall()
    {
        asteroidPosition = asteroid.transform.position;

        Instantiate(asteroidThree, asteroidPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        Instantiate(asteroidThree, asteroidPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        score = score + 2;
    }

}
