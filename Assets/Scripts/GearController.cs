using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    public Transform pandGear;
    public GameObject platform;
    public PlatformController platform_Controller;
    public bool isMoving = false;

    void Start(){
        pandGear = transform.parent;
        platform = pandGear.Find("MovingPlatform").gameObject;
        platform_Controller = platform.GetComponent<PlatformController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Attack"))
        {
            AttackController attack_Controller = col.gameObject.GetComponent<AttackController>();
            if(attack_Controller.state == AttackController.State.Fire)
            {
                isMoving = true;
                platform_Controller.speed = 1f;
            }
            else if (attack_Controller.state == AttackController.State.Cold)
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
