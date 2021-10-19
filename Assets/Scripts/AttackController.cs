using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public PlayerStateList pState;

    public enum State
    {
        Fire,
        Cold
    }
    public State state;

    private SpriteRenderer spriteR;

    public GameObject playerObject;

    public Sprite[] spriteArray;

    private void OnEnable()
    {
        playerObject = GameObject.Find("Player");

        CheckState();

    }
    // Start is called before the first frame update
    void Start()
    {
        pState = playerObject.GetComponent<PlayerStateList>();
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
        pState = playerObject.GetComponent<PlayerStateList>();
        if (pState.usingDragon == true)
        {
            state = State.Fire;
        }
        else if(pState.usingDragon == false)
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
