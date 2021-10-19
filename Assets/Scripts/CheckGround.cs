using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private Player_Controller player;

    public GameObject playerObject;

    public PlayerRespawn prspwn;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player_Controller>();

        playerObject = GameObject.Find("Player");
        prspwn = playerObject.GetComponent<PlayerRespawn>();
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
        player.grounded = false;
        player.isOnTrap = false;

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
