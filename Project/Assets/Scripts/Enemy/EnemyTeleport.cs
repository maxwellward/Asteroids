using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleport : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "TeleportEnemy")
        {
            //transform.rotation = Quaternion.Euler(0, 0, Random.Range(-60, -140));
            transform.position = gameObject.transform.position * -1;
        }
    }
}
