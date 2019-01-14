using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour {
    public bool isClicked;
    public string dialogue;

    private void Awake()
    {
        isClicked = false;
    }

    private void OnMouseDown()
    {
        if (!dialogue.Equals(""))
        {

        }
        else
        {
            gameObject.SetActive(false);
        }
    }


}
