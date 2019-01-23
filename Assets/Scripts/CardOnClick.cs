using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOnClick : MonoBehaviour
{
    public int choix;
    private static bool isClicked = false;
    
    private void OnMouseDown()
    {
        if (!isClicked)
        {
            isClicked = true;
            GameManager.instance.ChoixCarte(choix);
        }
    }
}
