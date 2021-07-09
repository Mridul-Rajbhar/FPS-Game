using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFiles : MonoBehaviour
{

    [SerializeField]
    private AudioClip riffleSound, shotgunSound, pistolSound, machineGun, botFire, nobullet;

    void PlaySound(AudioSource myAudioSource ,AudioClip soundClip)
    {
        myAudioSource.clip = soundClip;
       myAudioSource.Play();
    }

    public void NoBullet(AudioSource audioSource)
    {
        PlaySound(audioSource ,nobullet);
    }

    public void RiffleSound(AudioSource audioSource)
    {
        PlaySound(audioSource, riffleSound);   
    }

    public void MachineGun(AudioSource audioSource)
    {
        PlaySound(audioSource, machineGun);
    }

    public void ShotGunSound(AudioSource audioSource)
    {
        PlaySound(audioSource, shotgunSound);
    }

    public void PistolSound(AudioSource audioSource)
    {
        PlaySound(audioSource, pistolSound);
    }
    public void BotSound(AudioSource audioSource)
    {
        PlaySound(audioSource, botFire);
    }
    

}
