using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSound_Voice : MonoBehaviour {

    public AudioClip[] theSounds;

    // Use this for initialization
    void Start () {
    }

    public void PlayTheSound()
    {
        if(!GetComponent<AudioSource>().isPlaying)
        {
            int index = Random.Range(0, theSounds.Length - 1);
            GetComponent<AudioSource>().clip = theSounds[index];
            GetComponent<AudioSource>().Play();
        }
    }
}
