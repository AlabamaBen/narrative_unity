using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSound_Voice : MonoBehaviour {

    public AudioClip[] theSounds;

    public int Talk_Rate = 2; 

    // Use this for initialization
    void Start () {
    }

    private int count = 0 ; 

    public void PlayTheSound()
    {

        count++; 

        if(count % Talk_Rate == 0)
        {
            int index = Random.Range(0, theSounds.Length - 1);
            GetComponent<AudioSource>().clip = theSounds[index];
            GetComponent<AudioSource>().Play();
        }

        //if(!GetComponent<AudioSource>().isPlaying)
        //{

        ////}
    }
}
