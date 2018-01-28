using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioSource BacteriaChew;
    public AudioSource BacteriaGrowHappy;
    public AudioSource BacteriaShrinkSad;
    public AudioSource BacteriaReprod;
    public AudioSource BacteriaInteract;
    public AudioSource BacteriaDeath;
    public AudioSource FireGunTrigger;
    public AudioSource FireGunStream;
    public AudioSource IceGunTrigger;
    public AudioSource IceGunStream;
    public AudioSource SugarPickUp;
    public AudioSource SugarRelease;
    public AudioSource CharacterHeal;
    public AudioSource CharacterDeath;
    public AudioSource CharacterSwallow;
    public AudioSource CharacterDamage;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayBacteriaChew()
    {
        BacteriaChew.Play();
    }

    public void PlayBacteriaChew(bool value)
    {
        if (value)
        {
            BacteriaChew.Play();

        }
        else
        {
            BacteriaChew.Stop();
        }
    }


    public void PlayBacteriaGrowHappy()
    {
        BacteriaGrowHappy.Play();
    }

    public void PlayBacteriaGrowHappy(bool value)
    {
        if (value)
        {
            BacteriaGrowHappy.Play();
        }
        else
        {
            BacteriaGrowHappy.Stop();
        }
    }


    public void PlayBacteriaShrinkSad()
    {
        BacteriaShrinkSad.Play();
    }

    public void PlayBacteriaShrinkSad(bool value)
    {
        if (value)
        {
            BacteriaShrinkSad.Play();
        }
        else
        {
            BacteriaShrinkSad.Stop();
        }
    }


    public void PlayBacteriaReprod()
    {
        BacteriaReprod.Play();
        Debug.Log("Play Reprod sound in AudioCtrler");
    }

    public void PlayBacteriaInteract()
    {
        BacteriaInteract.Play();
    }

    public void PlayBacteriaDeath()
    {
        BacteriaDeath.Play();
    }

    public void PlayBacteriaReprod(bool value)
    {
        if (value)
        {
            PlayBacteriaReprod();
        }
        else
        {
            BacteriaReprod.Stop();
        }
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

    public void PlayCharacterHeal()
    {
        CharacterHeal.Play();
    }

    public void PlayCharacterDeath()
    {
        CharacterDeath.Play();
    }

    public void PlayCharacterSwallow()
    {
        CharacterSwallow.Play();
    }

    public void PlayCharacterDamage()
    {
        CharacterDamage.Play();
    }
}
