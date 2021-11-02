using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource playerAudioSource;

    [Header("Attacks Sounds")]
    [SerializeField]
    public AudioClip audioSrcFireball;
    public AudioClip audioSrcIcebeam;

    [Space(5)]

    [Header("Life Managment")]
    public AudioClip audioSrcGainLife;
    public AudioClip audioSrcLoseLife;
    public AudioClip audioSrcDeathDragon;
    public AudioClip audioSrcDeathMage;

    PlayerStateList playerState;

    private void Start()
    {
        playerState = gameObject.GetComponent<PlayerStateList>();
        playerAudioSource = gameObject.GetComponent<AudioSource>();

        playerAudioSource.clip = audioSrcFireball;
    }


    public void AttackSound()
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

    public void GainLifeSound()
    {
        playerAudioSource.clip = audioSrcGainLife;
        playerAudioSource.Play();
    }
    public void LoseLifeSound()
    {
        playerAudioSource.clip = audioSrcLoseLife;
        playerAudioSource.Play();
    }

    public void DeathSound()
    {
        if (playerState.usingDragon)
        {
            playerAudioSource.clip = audioSrcDeathDragon;
        }
        else
        {
            playerAudioSource.clip = audioSrcDeathMage;

        }
        playerAudioSource.Play();
    }
}
