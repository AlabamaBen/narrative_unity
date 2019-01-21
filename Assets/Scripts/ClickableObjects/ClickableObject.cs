using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    [HideInInspector]
    public GameObject prefab;
    //[HideInInspector]
    public bool isInterractable;
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
        prefab = this.gameObject;
        isInterractable = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("CLicked");
        if (isInterractable && !isClicked)
        {
            isClicked = true;
            ClickableObjetManager.instance.ObjectClicked(this);
            Invoke("waitAndUnblockInput", 0.5f);
        }
    }
    private void waitAndUnblockInput()
    {
        isClicked = false;
    }

    #region SpriteOutline
    public void ObjectBlink()
    {
        blinking = true;
        StartCoroutine("SpriteBlink");
    }

    IEnumerator SpriteBlink()
    {
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
