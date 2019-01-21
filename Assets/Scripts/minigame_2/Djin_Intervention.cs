using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Djin_Intervention : MonoBehaviour {


    public TMPro.TextMeshPro boiteDialogue;
    //public SFXSound_Voice talk_sound;
    public float text_speed = 0.02f;
    public float speed = 3f;


    public void Display_Text(string text)
    {
        boiteDialogue.text = "";
        StartCoroutine(AnimateTextMonolog(text, text_speed));
    }

    public void Appear()
    {
        StartCoroutine(AnimateMove(speed));
    }

    public void Disappear()
    {
        StartCoroutine(AnimateMoveOut(speed));
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

    public Transform stop_point; 

    public IEnumerator AnimateMove(float speed)
    {
        while (stop_point.position.x - this.transform.position.x < 0)
        {
            this.transform.Translate(new Vector2(- speed * Time.deltaTime , 0f));
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator AnimateMoveOut(float speed)
    {
        while (this.transform.position.x < stop_point.position.x + 10)
        {
            this.transform.Translate(new Vector2(speed * Time.deltaTime, 0f));
            yield return new WaitForSeconds(0.01f);
        }
    }
}
