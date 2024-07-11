using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{


    [Header("-------------Audio Source-------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    
    [Header("-------------Audio Clips-------------")]
    public AudioClip background;
    public AudioClip thrustersSFX;
    public AudioClip powerupPickupSFX;
    public AudioClip laserShootSFX1;
    public AudioClip bossIncomingSFX;
    public AudioClip enemyExplosion;
    /*public AudioClip laserShootSFX4;
    public AudioClip laserShootSFX5;
    public AudioClip laserShootSFX6;*/
    public AudioClip trippleLasersSFX;



    // Start is called before the first frame update
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }


}
