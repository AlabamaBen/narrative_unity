using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject Credit;
    public GameObject MainMenu;
    public Animation blackpanel;
    public Animation musicfade;
    public ParticleSystem[] particleSystems; 

    public void OnClickPlay()
    {
        Debug.Log("PLAY");

        blackpanel.gameObject.SetActive(true);

        blackpanel.Play();
        musicfade.Play();

        foreach(ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Stop(); 
        }

        Invoke("LoadGame", 7); 
    }

    void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
