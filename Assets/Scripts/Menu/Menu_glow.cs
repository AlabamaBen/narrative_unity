using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;


public class Menu_glow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    public GameObject text_glow;
    public GameObject text; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        text_glow.SetActive(true);
        text.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text_glow.SetActive(false);
        text.SetActive(true);
    }

}
