using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private Player_Controller player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player_Controller>();
        
    }

    void OnCollisionStay2D(Collision2D col){
        if(col.gameObject.CompareTag("Land"))
        {
            player.grounded = true;
        }
        if(col.gameObject.CompareTag("Traps")){
            player.isOnTrap = true;
        }
    }

    void OnCollisionExit2D(Collision2D col){
        player.grounded = false;
        player.isOnTrap = false;
    }
}
