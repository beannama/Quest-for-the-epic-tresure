using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public enum State
    {
        Fire,
        Cold
    }
    public State state;

    private SpriteRenderer spriteR;

    public Sprite[] spriteArray;

    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSprite();
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
