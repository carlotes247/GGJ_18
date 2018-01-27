using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource BacteriaChew;
    public AudioSource BacteriaGrowHappy;
    public AudioSource BacteriaShrinkSad;
    public AudioSource FireGunTrigger;
    public AudioSource FireGunStream;
    public AudioSource IceGunTrigger;
    public AudioSource IceGunStream;
    public AudioSource SugarPickUp;
    public AudioSource SugarRelease;
    public AudioSource CharacterSwallow;
    public AudioSource CharacterHeal;
    public AudioSource CharacterDeath;
    public AudioSource CharacterDamage;


    public void PlayBacteriaChew()
    {
        BacteriaChew.Play();
    }

    public void PlayBacteriaGrowHappy()
    {
        BacteriaHappy.Play();
    }

    public void PlayBacteriaShrinkSad()
    {
        BacteriaShrink.Play();
    }

    public void PlayFireGunTrigger()
    {
        FireGunTrigger.Play();
    }

    public void PlayFireGunStream()
    {
        FireGunStream.Play();
    }

    public void PlayIceGunTrigger()
    {
        IceGunTrigger.Play();
    }

    public void PlayIceGunStream()
    {
        IceGunStream.Play();
    }

    public void PlaySugarPickUp()
    { 
        SugarPickUp.Play();
    }

    public void PlaySugarRelease()
    {
        SugarRelease.Play();
    }

    public void PlayCharacterSwallow()
    {
        CharacterSwallow.Play();
    }

    public void PlayCharacterHeal()
    {
        CharacterHeal.Play();
    }

    public void PlayCharacterDeath()
    {
        CharacterDeath.Play();
    }

    public void PlayCharacterDamage()
    {
        CharacterDamage.Play();
    }
}