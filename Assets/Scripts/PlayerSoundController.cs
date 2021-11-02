using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSrcFireball;
    public AudioSource audioSrcIcebeam;
    PlayerStateList playerState;

    private void Start()
    {
        playerState = GetComponent<PlayerStateList>();
    }

    public void MakeSound()
    {
        if (playerState.usingDragon)
        {
            audioSrcFireball.Play();
        }
        else
        {
            audioSrcIcebeam.Play();
        }
    }


}
