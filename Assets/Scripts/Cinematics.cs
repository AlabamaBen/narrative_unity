using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cinematics : MonoBehaviour
{
    public List<GameObject> planches;
    public GameObject legend;
    public GameObject antre;
    private List<Image> images;
    private int compteur;
    private bool blockInput;
    public bool startCinematic;
    public bool endCinematic;
    public float speedAnimation;
    private int plancheIndex;

    public static Cinematics instance = null;

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
            blockInput = false;
            startCinematic = false;
            endCinematic = false;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Use this for initialization
    void InitCinematics(int index)
    {
        images = new List<Image>();
        foreach (Transform tr in planches[index].transform)
        {
            Image img = tr.GetComponent<Image>();
            img.enabled = false;
            images.Add(img);
        }
        compteur = 0;
        plancheIndex = index;
        startCinematic = true;
        blockInput = true;

        StartCoroutine(FadeImageBgPlanche(false, planches[index].GetComponent<Image>(), index, speedAnimation));
        StartCoroutine(FadeVignette(false, compteur, index, speedAnimation));

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !blockInput && startCinematic)
        {
            blockInput = true;
            StartCoroutine(FadeVignette(false, compteur, plancheIndex, speedAnimation));
        }
    }

    IEnumerator FadeVignette(bool fadeAway, int imgIndex, int plancheIndex, float speed)
    {
        if (compteur < images.Count)
        {
            // fade from opaque to transparent
            if (fadeAway)
            {
                // loop over 1 second backwards
                for (float i = 1; i >= 0; i -= Time.deltaTime * speed)
                {
                    // set color with i as alpha
                    images[imgIndex].color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
            // fade from transparent to opaque
            else
            {
                images[imgIndex].enabled = true;
                // loop over 0.5 second
                for (float i = 0; i <= 1; i += Time.deltaTime * speed)
                {
                    // set color with i as alpha
                    images[imgIndex].color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
            blockInput = false;
            compteur++;
        }
        else
        {
            startCinematic = false;
            // Fade out BG
            foreach (Image img in images)
            {
                StartCoroutine(FadeImageBgPlanche(true, img,plancheIndex, speedAnimation));
            }
            StartCoroutine(FadeImageBgPlanche(true, planches[plancheIndex].GetComponent<Image>(), plancheIndex, speedAnimation));
        }
    }

    IEnumerator FadeImageBgPlanche(bool fadeAway,Image img,int index, float speed)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime * speed)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
            planches[index].gameObject.SetActive(false);
            endCinematic = true;
        }
        // fade from transparent to opaque
        else
        {
            img.enabled = true;
            // loop over 0.5 second
            for (float i = 0; i <= 1; i += Time.deltaTime * speed)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

    IEnumerator FadeINandOutImage(bool fadeAway, Image img, float speed, GameObject objToDeactivate)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime * speed)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
            objToDeactivate.SetActive(false);
            endCinematic = true;
        }
        // fade from transparent to opaque
        else
        {
            img.enabled = true;
            // loop over 0.5 second
            for (float i = 0; i <= 1; i += Time.deltaTime * speed)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
            StartCoroutine(FadeINandOutImage(true, img, speed, antre));
        }
    }


    public void DisplayCinematic(int index)
    {
        planches[index].SetActive(true);
        InitCinematics(index);
    }

    private void WaitAndUnblockInput()
    {
        blockInput = false;
    }

    public void DisplayText(string msg)
    {
        legend.SetActive(true);
        legend.GetComponent<Text>().text = msg;
    }

    public void DisplayAntre(float delay,float transparency)
    {
        endCinematic = false;
        antre.SetActive(true);
        Image antreImg = antre.GetComponent<Image>();
        Color transparencyColor = antreImg.color;
        transparencyColor.a = transparency;
        antreImg.color = transparencyColor;
        StartCoroutine(FadeINandOutImage(false, antreImg, delay,antre));

    }
}
