using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Djin_Intervention : MonoBehaviour {


    public TMPro.TextMeshPro boiteDialogue;
    //public SFXSound_Voice talk_sound;
    public float text_speed = 0.02f;

    // Use this for initialization
    void Start () {

        StartCoroutine(AnimateTextMonolog("LOLOLOLLOLOLOLLOLOLOLLOLOLOLLOLOLOLLOLOLOL", text_speed));

    }


    public IEnumerator AnimateTextMonolog(string strComplete, float speed)
    {
        int i = 0;
        //talk_sound.PlayTheSound();
        while (i < strComplete.Length)
        {
            boiteDialogue.text += strComplete[i++];
            yield return new WaitForSeconds(speed);
        }
    }
}
