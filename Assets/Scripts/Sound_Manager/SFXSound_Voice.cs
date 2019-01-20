using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSound_Voice : MonoBehaviour
{

    public AudioClip[] theSounds;
    public float delay = 0.2f; 


    // Use this for initialization
    void Start()
    {
        cooldown = 0f; 
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
    }

    private float cooldown; 

    public void PlayTheSound()
    {
        //Debug.Log("Play");
        if(cooldown < 0 )
        {
            foreach (AudioSource audioSource in GetComponents<AudioSource>())
            {
                if (!audioSource.isPlaying)
                {
                    int index = Random.Range(0, theSounds.Length - 1);
                    audioSource.clip = theSounds[index];
                    audioSource.Play();
                    cooldown = delay;
                    break;
                }
            }
        }


        //if (!GetComponent<AudioSource>().isPlaying)
        //{
        //    int index = Random.Range(0, theSounds.Length - 1);
        //    GetComponent<AudioSource>().clip = theSounds[index];
        //    GetComponent<AudioSource>().Play();
        //}
    }

    public void StopTheSound()
    {
        GetComponent<AudioSource>().Stop();
    }

}
