using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_Controller : MonoBehaviour
{
    public enum StateEnum
    {
        On,
        Off
    }

    private SpriteRenderer spriteR;
    private GameObject EventSystem;
    public Sprite torchOn;
    private StateEnum torchState;

    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        EventSystem = GameObject.FindWithTag("GameController");
        torchState = StateEnum.Off;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Attack"))
        {
            if (torchState == StateEnum.Off)
            {
                spriteR.sprite = torchOn;
                torchState = StateEnum.On;
                EventSystem.GetComponent<Game_Controller>().IncreaseTorchCount();
            }
        }
    }
}
