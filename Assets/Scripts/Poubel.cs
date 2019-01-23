using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poubel : MonoBehaviour {

    // Update is called once per frame
    void Awake()
    {
        GameManager.instance.worlds_Canvas.SetActive(false);
        GameManager.instance.curtains_Panel.SetActive(false);
    }

    // Use this for initialization
    void Start () {

       GameManager.instance.worlds_Canvas.SetActive(false);
        GameManager.instance.curtains_Panel.SetActive(false);
    }

    private void Update()
    {
        GameManager.instance.worlds_Canvas.SetActive(false);
        GameManager.instance.curtains_Panel.SetActive(false);
    }

}
