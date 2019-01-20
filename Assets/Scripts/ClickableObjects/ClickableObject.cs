using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    [HideInInspector]
    public bool isClicked;
    // public bool hideAfterClick;
    public string dialogue;

    [Header("SpriteOutline")]
    public bool blinking = false;
    public float blinkingSpeed;

    private void Awake()
    {
        isClicked = false;
        blinking = false;
    }

    private void OnMouseDown()
    {
        if (!dialogue.Equals("") && !SpeechManager.instance.textDisplayed)
        {
            SpeechManager.instance.DisplayThoughOnObject(dialogue);
            ClickableObjetManager.instance.ObjectClicked(this);
        }
        else if(dialogue.Equals(""))
        {
            this.gameObject.SetActive(false);
        }
    }
    
    #region SpriteOutline
    public void ObjectBlink()
    {
        blinking = true;
        StartCoroutine("SpriteBlink");
    }

    IEnumerator SpriteBlink()
    {
        Debug.Log("SpriteBlink");
        while (blinking)
        {
            if (this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled == true)
            {
                this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;  //make changes
            }
            else
            {
                this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;   //make changes
            }
            yield return new WaitForSeconds(1F);
            Debug.Log("Sending");
        }
    }

    public void StopBlinking()
    {
        blinking = false;
        if (this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled == true)
        {
            this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;  //make changes
        }
    }
    #endregion


}
