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

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("entered");
        if(col.gameObject.tag == "DestroyBullet")
        {
            transform.position = gameObject.transform.position * -1;
            StartCoroutine("DestroyBullet");
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
