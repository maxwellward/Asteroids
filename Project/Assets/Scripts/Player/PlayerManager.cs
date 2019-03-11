using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed;
    public GameObject bulletPrefab;
    public Transform shootLoc;

    public float thrust;
    public float topSpeed;
    public Rigidbody2D player;

    public GameObject topTriggerPlayer;
    public GameObject bottomTriggerPlayer;
    public GameObject leftTriggerPlayer;
    public GameObject rightTriggerPlayer;

    /*public GameObject topTriggerBullet;
    public GameObject bottomTriggerBullet;
    public GameObject leftTriggerBullet;
    public GameObject rightTriggerBullet;*/


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

/*    IEnumerator RemoveCollidersPlayer()
    {
        topTriggerPlayer.GetComponent<Collider2D>().enabled = false;
        bottomTriggerPlayer.GetComponent<Collider2D>().enabled = false;
        rightTriggerPlayer.GetComponent<Collider2D>().enabled = false;
        leftTriggerPlayer.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(1f);

        topTriggerPlayer.GetComponent<Collider2D>().enabled = true;
        bottomTriggerPlayer.GetComponent<Collider2D>().enabled = true;
        rightTriggerPlayer.GetComponent<Collider2D>().enabled = true;
        leftTriggerPlayer.GetComponent<Collider2D>().enabled = true;

    }*/

    void Shoot()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, shootLoc.position, shootLoc.rotation);
        }

    }

/*    IEnumerator RemoveCollidersBullets()
    {
        topTriggerBullet.GetComponent<Collider2D>().enabled = false;
        bottomTriggerBullet.GetComponent<Collider2D>().enabled = false;
        rightTriggerBullet.GetComponent<Collider2D>().enabled = false;
        leftTriggerBullet.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(0.3f);

        topTriggerBullet.GetComponent<Collider2D>().enabled = true;
        bottomTriggerBullet.GetComponent<Collider2D>().enabled = true;
        rightTriggerBullet.GetComponent<Collider2D>().enabled = true;
        leftTriggerBullet.GetComponent<Collider2D>().enabled = true;

    }*/

    // This is a part of the old movement system, I'm leaving it here in case it's needed at a later date.
    /*void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );

        transform.up = direction;

    }*/
}
