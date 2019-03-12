using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAddForce : MonoBehaviour
{

    public float speed;

    void Update()
    {
        if (Enemies.side = 2)
        {

        }
        transform.Translate(-Vector2.up * speed * Time.deltaTime);
    }
}
