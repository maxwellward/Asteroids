using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAddForce : MonoBehaviour
{

    public float speed;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    /*public void OnDestroy()
    {
        Debug.Log("Object destroyed");

        if (gameObject.tag == "Asteroid")
        {
            Debug.Log("Asteroid");
        }
    }*/
}
