using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float speed;
    public GameObject bulletPrefab;
    public Transform shootLoc;

    public float thrust;
    public float topSpeed;
    public Rigidbody2D player;


    // Update is called once per frame
    void Update()
    {
        //FaceMouse();
        Shoot();
        Movement();
    }

    void Movement()
    {
        // Right turn
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -3, Space.World);
        }
        // Left turn
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, 3, Space.World);
        }
        // Forward Thrust
        if (Input.GetKey(KeyCode.UpArrow))
        {
            player.AddForce(transform.up * thrust);
        }

        if (player.velocity.magnitude > topSpeed)
            player.velocity = player.velocity.normalized * topSpeed;
    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.tag == "TeleportPlayer")
        {
            //StartCoroutine("RemoveCollidersPlayer");
            transform.position = gameObject.transform.position * -1;
        }
   
    }

    public GameObject scoreText;
    public GameObject gameOverPanel;

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Asteroid" || col.gameObject.tag == "AsteroidSmall" || col.gameObject.tag == "AsteroidTiny" || col.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
            Time.timeScale = 0;
            scoreText.SetActive(false);
            gameOverPanel.SetActive(true);
        }



    }

    void Shoot()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, shootLoc.position, shootLoc.rotation);
        }

    }
}
