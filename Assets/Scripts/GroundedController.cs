using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedController : MonoBehaviour
{
    private EnemyMovement Enemy;

    void Start()
    {
        Enemy = GetComponentInParent<EnemyMovement>();
    }


    void OnTriggerStay2D(Collider2D col){
        if (col.gameObject.tag == "Land"){
            Enemy.isGrounded = true;
        }
        else
        {
            Enemy.isGrounded = false;
        }
    }

    void OnTriggerExist2D(Collider2D col){
        if (col.gameObject.tag == "Ground"){
            Enemy.isGrounded = false;
        }
    }
}
