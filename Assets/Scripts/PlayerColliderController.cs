using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{

    public PlayerStateList pState;
    private PlayerController player;
    private Rigidbody2D rb2d;

    public GameObject playerObject;

    public PlayerRespawn prspwn;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        pState = GetComponent<PlayerStateList>();

        playerObject = GameObject.Find("Player");
        prspwn = playerObject.GetComponent<PlayerRespawn>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Platform")){
            rb2d.velocity = new Vector3(0f,0f,0f);
            player.transform.parent = col.transform;

            pState.isGrounded = true;

        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Land"))
        {
            pState.isGrounded = true;
        }

        if (col.gameObject.CompareTag("Traps"))
        {
            pState.isOnTrap = true;
            prspwn.KillPlayer();
        }

        if(col.gameObject.CompareTag("Platform")){
            player.transform.parent = col.transform;
            pState.isGrounded = true;

        }

        Vector3 direction = col.gameObject.transform.position;

        if (col.gameObject.CompareTag("Enemy"))
        {
            CheckAttack(direction, col);
            prspwn.PlayerDamaged();

            rb2d.velocity = Vector2.zero;
        }

    }

    void OnCollisionExit2D(Collision2D col)
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

    void CheckAttack(Vector3 attackDirection, Collision2D col)
    {
        Vector3 direction = transform.position - attackDirection;

        //Do
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                //From the left
                Debug.Log("hitted from the left");
                transform.position = new Vector3(transform.position.x + 2f, 0, 0);
            }
            else
            {
                //From the right
                Debug.Log("hitted from the right");
                transform.position = new Vector3(transform.position.x - 2f, 0, 0);
            }

           
        }
    }


}
