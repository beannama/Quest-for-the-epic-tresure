using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public enum StateEnum
    {
        On,
        Off
    }

    private SpriteRenderer spriteR;
    private GameObject EventSystem;

    //TODO: REMOVE THIS 2 DEPENDENCIES
    private PlayerController pController; 
    private GameController gameController;

    public Sprite torchOn;
    public Sprite torchOff;
    private StateEnum torchState;

    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        EventSystem = GameObject.FindWithTag("GameController");
        gameController = EventSystem.GetComponent<GameController>();

        pController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();


        torchState = StateEnum.Off;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        AttackController attack_Controller = col.gameObject.GetComponent<AttackController>();
        if (col.gameObject.CompareTag("Attack"))
        {
            if ((torchState == StateEnum.Off) && (attack_Controller.state == AttackController.State.Fire))
            {
                if (gameController.CheckIfRightTorch(gameObject))
                {
                    //LIGHT TORCH
                    LightTorch();
                }
                else
                {
                    //TODO: REMOVE THIS AND DO IT WITHOUT DEPENDENCIES
                    //PUNISH PLAYER
                    pController.ReceiveDamage(); 
                    //TURN OFF TORCHES
                    gameController.TurnOffAllTorches();
                    Debug.Log("Bad Torch");
                }
            }
            else if ((torchState == StateEnum.On) && (attack_Controller.state == AttackController.State.Cold))
            {
                ExtinguishTorch();
            }            
        }
    }

    public void LightTorch()
    {
        spriteR.sprite = torchOn;
        torchState = StateEnum.On;
        gameController.IncreaseTorchCount();
    }
    public void ExtinguishTorch()
    {
        spriteR.sprite = torchOff;
        torchState = StateEnum.Off;
        gameController.DecreaseTorchCount();
    }
}
