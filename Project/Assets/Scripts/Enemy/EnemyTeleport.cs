using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleport : MonoBehaviour
{

    /*public GameObject topTriggerEnemy;
    public GameObject bottomTriggerEnemy;
    public GameObject rightTriggerEnemy;
    public GameObject leftTriggerEnemy;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("entered");
        if (col.gameObject.tag == "TeleportEnemy")
        {
            StartCoroutine("RemoveCollidersEnemy");
            transform.position = gameObject.transform.position * -1;
        }

    }

    IEnumerator RemoveCollidersEnemy()
    {
        topTriggerEnemy.GetComponent<Collider2D>().enabled = false;
        bottomTriggerEnemy.GetComponent<Collider2D>().enabled = false;
        rightTriggerEnemy.GetComponent<Collider2D>().enabled = false;
        leftTriggerEnemy.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(1f);

        topTriggerEnemy.GetComponent<Collider2D>().enabled = true;
        bottomTriggerEnemy.GetComponent<Collider2D>().enabled = true;
        rightTriggerEnemy.GetComponent<Collider2D>().enabled = true;
        leftTriggerEnemy.GetComponent<Collider2D>().enabled = true;

    }*/

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "TeleportEnemy")
        {
            transform.rotation = Quaternion.Euler(Random.Range(-35, 35), 0, 0);
            transform.position = gameObject.transform.position * -1;
        }

    }

}
