using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps_Controller : MonoBehaviour
{
    public GameObject playerObject;
    public PlayerRespawn prspwn;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        prspwn =  playerObject.GetComponent<PlayerRespawn>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trap triggered!!");
            prspwn.PlayerKillOnTrap();
        }
    }
}
