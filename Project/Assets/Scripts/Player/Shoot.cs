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
            Debug.Log("hit asteriod, nice shot... idiot");
            Destroy(col.gameObject);
            Destroy(this.gameObject);
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

}
