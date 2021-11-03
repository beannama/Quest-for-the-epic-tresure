using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{

    public PlayerStateList pState;
    private PlayerController player;
    private PlayerSoundController pSound;
    private Rigidbody2D rb2d;

    public GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        pState = GetComponent<PlayerStateList>();
        pSound = GetComponent<PlayerSoundController>();

        playerObject = GameObject.Find("Player");
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Traps") || col.gameObject.CompareTag("Water"))
        {
            player.KillPlayer();
        }
        Vector3 direction = col.gameObject.transform.position;
        if (col.gameObject.CompareTag("Enemy"))
        {
            CheckAttack(direction, col);
            player.ReceiveDamage();

            rb2d.velocity = Vector2.zero;
        }
        if (col.gameObject.CompareTag("Heart"))
        {
            player.GainLife();
            Destroy(col.gameObject); //Destroy the heart object 
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
