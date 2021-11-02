using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource playerAudioSource;
    [SerializeField]
    public AudioClip audioSrcFireball;
    public AudioClip audioSrcIcebeam;
    PlayerStateList playerState;

    private void Start()
    {
        playerState = gameObject.GetComponent<PlayerStateList>();
        playerAudioSource = gameObject.GetComponent<AudioSource>();

        playerAudioSource.clip = audioSrcFireball;
    }


    public void MakeSound()
    {
        if (playerState.usingDragon)
        {
            playerAudioSource.clip = audioSrcFireball;
        }
        else
        {
            playerAudioSource.clip = audioSrcIcebeam;

        }
        playerAudioSource.Play();
    }


}
