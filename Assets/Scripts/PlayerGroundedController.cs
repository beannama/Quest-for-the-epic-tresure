using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedController : MonoBehaviour
{
    public PlayerStateList pState;
    private PlayerController player;
    private PlayerSoundController pSound;
    private Rigidbody2D rb2d;

    public GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        pState = GetComponentInParent<PlayerStateList>();
        pSound = GetComponentInParent<PlayerSoundController>();

        playerObject = GameObject.Find("Player");
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Platform"))
        {
            rb2d.velocity = new Vector3(0f,0f,0f);
            player.transform.parent = col.transform;

            pState.isGrounded = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Land"))
        {
            pState.isGrounded = true;
        }

        if (col.gameObject.CompareTag("Platform"))
        {
            player.transform.parent = col.transform;
            pState.isGrounded = true;
        }

        
    }


    void OnTriggerExit2D(Collider2D col)
    {
        pState.isOnTrap = false;
        if(col.gameObject.CompareTag("Land")){

            pState.isGrounded = false;
        }
        if(col.gameObject.CompareTag("Platform")){
            player.transform.parent = null;

            pState.isGrounded = false;

        }

    }

    void CheckAttack(Vector3 attackDirection, Collider2D col)
    {
        Vector3 direction = transform.position - attackDirection;

        //Do
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                //From the left
                //Debug.Log("hitted from the left");
                transform.position = new Vector3(transform.position.x + 2f, transform.position.y, 0);
            }
            else
            {
                //From the right
                //Debug.Log("hitted from the right");
                transform.position = new Vector3(transform.position.x - 2f, transform.position.y, 0);
            }

           
        }
    }
}
