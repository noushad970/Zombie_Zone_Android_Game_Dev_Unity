using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource shieldAttack, zombieWalk, spaceAttack, Bite,zombieDie,bossButcherWalk, BossButcherDie, BossButcherAttack,BossButcherHurt;
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
        if(shieldAttack.isPlaying)
            return; // Prevent overlapping sounds
        shieldAttack.Play();
    }
    public void zombieWalkPlay()
    {
        if(!zombieWalk.isPlaying)
        zombieWalk.Play();
    }
    public void bossButcherWalkPlay()
    {
        if(!bossButcherWalk.isPlaying)
            bossButcherWalk.Play();
    }
    public void spaceAttackPlay()
    {
        if(spaceAttack.isPlaying)
            return; // Prevent overlapping sounds
        spaceAttack.Play();
    }
    public void BitePlay()
    {
        if(Bite.isPlaying)
            return; // Prevent overlapping sounds
        Bite.Play();
    }
    public void SwordAttackPlay()
    {
        int randomIndex = Random.Range(0, swordAttack.Length);
        if(swordAttack[randomIndex].isPlaying)
            return; // Prevent overlapping sounds
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
        if(zombieDie.isPlaying)
            return; // Prevent overlapping sounds
        zombieDie.Play();
    }

    public void BossButcherDiePlay()
    {
        if(BossButcherDie.isPlaying)
            return; // Prevent overlapping sounds
        BossButcherDie.Play();
    }
    public void BossButcherAttackPlay()
    {
        if(BossButcherAttack.isPlaying)
            return; // Prevent overlapping sounds
        BossButcherAttack.Play();
    }
    public void BossButcherHurtPlay()
    {
        if(BossButcherHurt.isPlaying)
            return; // Prevent overlapping sounds
        BossButcherHurt.Play();
    }

}