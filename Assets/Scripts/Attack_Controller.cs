using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Controller : MonoBehaviour
{
    public enum State
    {
        Fire,
        Cold
    }
    private State state;

    private SpriteRenderer spriteR;

    public GameObject playerObject;

    public Sprite[] spriteArray;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        CheckSprite();
    }

    private void CheckState()
    {
        Player_Controller player_Controller = playerObject.GetComponent<Player_Controller>();
        if(player_Controller.characterState == Player_Controller.StateCharacter.Dragon)
        {
            state = State.Fire;
        }
        else if(player_Controller.characterState == Player_Controller.StateCharacter.Mage)
        {
            state = State.Cold;
        }
    }

    private void CheckSprite()
    {
        if(state == State.Fire)
        {
            spriteR.sprite = spriteArray[1];

            transform.localScale = new Vector3(1, 1, 1);
        }
        else if( state == State.Cold)
        {
            spriteR.sprite = spriteArray[0];
            transform.localScale = new Vector3(1, 3, 1);
        }
    }

}
