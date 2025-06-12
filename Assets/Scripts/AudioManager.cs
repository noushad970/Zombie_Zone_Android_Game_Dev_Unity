using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource shieldAttack, zombieWalk, spaceAttack, Bite,zombieDie;
    public AudioSource[] swordAttack;
    public AudioSource audioSource; // Reference to the AudioSource
    public AudioClip footstepClip;  // Footstep sound clip
    float footstepInterval; // Time between footsteps
    private float footstepTimer = 0f;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
       
        
    }
    public void shieldAttackPlay()
    {
        shieldAttack.Play();
    }
    public void zombieWalkPlay()
    {
        if(!zombieWalk.isPlaying)
        zombieWalk.Play();
    }
    public void spaceAttackPlay()
    {
        spaceAttack.Play();
    }
    public void BitePlay()
    {
        Bite.Play();
    }
    public void SwordAttackPlay()
    {
        int randomIndex = Random.Range(0, swordAttack.Length);
        swordAttack[randomIndex].Play();
    }
    public void playFootstep()
    {
        footstepTimer += Time.deltaTime;

        // Play footstep sound at intervals
        if (footstepTimer >= footstepInterval)
        {
            PlayFootstepSound();
            footstepTimer = 0f;
        }
    }
    void PlayFootstepSound()
    {
        audioSource.PlayOneShot(footstepClip);
    }
    public void zombieDiePlay()
    {
        zombieDie.Play();
    }
}