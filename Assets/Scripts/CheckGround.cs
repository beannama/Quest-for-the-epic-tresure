using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private Player_Controller player;
    private Rigidbody2D rb2d;

    public GameObject playerObject;

    public PlayerRespawn prspwn;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player_Controller>();

        playerObject = GameObject.Find("Player");
        prspwn = playerObject.GetComponent<PlayerRespawn>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Platform")){
            rb2d.velocity = new Vector3(0f,0f,0f);
            player.transform.parent = col.transform;
            player.grounded = true;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Land"))
        {
            player.grounded = true;
        }
        if (col.gameObject.CompareTag("Traps"))
        {
            player.isOnTrap = true;
        }
        if(col.gameObject.CompareTag("Platform")){
            player.transform.parent = col.transform;
            player.grounded = true;
        }

        Vector3 direction = col.gameObject.transform.position;

        if (col.gameObject.CompareTag("Enemy"))
        {
            CheckAttack(direction, col);
            prspwn.PlayerDamaged();

            player.rb2d.velocity = Vector2.zero;
        }

    }

    void OnCollisionExit2D(Collision2D col)
    {
        player.isOnTrap = false;
        if(col.gameObject.CompareTag("Land")){
            player.grounded = false;
        }
        if(col.gameObject.CompareTag("Platform")){
            player.transform.parent = null;
            player.grounded = false;
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
