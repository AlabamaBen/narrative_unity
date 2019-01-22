using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject Credit;
    public GameObject MainMenu;
    public Image blackpanel;
    public float fade_speed = 3f;

    bool fadeout = false; 

    public void OnClickPlay()
    {
        Debug.Log("PLAY");

        fadeout = true;
        blackpanel.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(fadeout)
        {
            blackpanel.color = new Color(blackpanel.color.r, blackpanel.color.g, blackpanel.color.b, blackpanel.color.a + fade_speed * Time.deltaTime);
            if (blackpanel.color.a > 0.99f)
            {
                fadeout = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void OnClickCredits()
    {
        Debug.Log("CREDITS");
        MainMenu.SetActive(false);
        Credit.SetActive(true);
    }

    public void OnClickExitCredits()
    {
        Debug.Log("CREDITS");
        MainMenu.SetActive(true);
        Credit.SetActive(false);
    }

}
