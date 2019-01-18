using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    [HideInInspector]
    public bool isClicked;
    // public bool hideAfterClick;
    public string dialogue;

    private void Awake()
    {
        isClicked = false;
    }

    private void OnMouseDown()
    {
        if (!dialogue.Equals("") && !DialoguesManager.instance.textDisplayed)
        {
            DialoguesManager.instance.DisplayThoughOnObject(dialogue);
            ClickableObjetManager.instance.ObjectClicked(this);
        }
        else if(dialogue.Equals(""))
        {
            this.gameObject.SetActive(false);
        }
    }


}
