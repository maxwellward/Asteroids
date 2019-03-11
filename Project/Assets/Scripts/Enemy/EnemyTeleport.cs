using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleport : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("entered");
        if (col.gameObject.tag == "TeleportEnemy")
        {
            transform.position = gameObject.transform.position * -1;
        }

    }

}
