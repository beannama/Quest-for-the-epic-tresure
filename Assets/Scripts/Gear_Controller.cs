using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear_Controller : MonoBehaviour
{
    public Transform pandGear;
    public GameObject platform;
    public Platform_controller platform_Controller;
    public bool isMoving = false;

    void Start(){
        pandGear = transform.parent;
        platform = pandGear.Find("MovingPlatform").gameObject;
        platform_Controller = platform.GetComponent<Platform_controller>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Attack"))
        {
            Attack_Controller attack_Controller = col.gameObject.GetComponent<Attack_Controller>();
            if(attack_Controller.state == Attack_Controller.State.Fire)
            {
                isMoving = true;
                platform_Controller.speed = 1f;
            }
            else if (attack_Controller.state == Attack_Controller.State.Cold)
            {
                isMoving = false;
                platform_Controller.speed = 0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
