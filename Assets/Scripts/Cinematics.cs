using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cinematics : MonoBehaviour
{
    public GameObject bgIntro;
    public List<GameObject> planches;
    public GameObject legend;
    public GameObject antre;
    public GameObject panel_Text;
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

        Debug.Log(endCinematic);

        StartCoroutine(FadeImageBgPlanche(false, planches[index].GetComponent<Image>(), index, speedAnimation,false));
        Debug.Log(endCinematic);
        StartCoroutine(FadeVignette(false, compteur, index, speedAnimation));
        Debug.Log(endCinematic);

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
            PlaySound(plancheIndex, imgIndex);
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
            for (int i=0;i<images.Count-1;i++)
            {
                StartCoroutine(FadeINandOutImage(true, images[i], speedAnimation, images[i].gameObject,false));
            }
            Invoke("WaitAndEndCinematic", 0.5f);
        }
    }

    private void WaitAndEndCinematic()
    {
        StartCoroutine(FadeINandOutImage(true, images[images.Count - 1], 5f, images[images.Count - 1].gameObject, false));
        StartCoroutine(FadeImageBgPlanche(true, planches[plancheIndex].GetComponent<Image>(), plancheIndex, speedAnimation,true));
    }

    IEnumerator FadeImageBgPlanche(bool fadeAway,Image img,int index, float speed, bool _endCinematic)
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
            if(_endCinematic)
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

    IEnumerator FadeINandOutImage(bool fadeAway, Image img, float speed, GameObject objToDeactivate, bool _endCinematic)
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
            if (_endCinematic)
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
            StartCoroutine(FadeINandOutImage(true, img, speed, antre,_endCinematic));
        }
    }

    IEnumerator FadeText(bool fadeAway, Text text, float speed, GameObject objToDeactivate)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime * speed)
            {
                // set color with i as alpha
                text.color = new Color(1, 1, 1, i);
                yield return null;
            }
            objToDeactivate.SetActive(false);
            endCinematic = true;
        }
        // fade from transparent to opaque
        else
        {
            text.enabled = true;
            // loop over 0.5 second
            for (float i = 0; i <= 1; i += Time.deltaTime * speed)
            {
                // set color with i as alpha
                text.color = new Color(1, 1, 1, i);
                yield return null;
            }
            StartCoroutine(FadeText(true, text, speed, objToDeactivate));
        }
    }

    public void DisplayText()
    {
        panel_Text.SetActive(true);
        StartCoroutine(FadeText(false, panel_Text.GetComponentInChildren<Text>(), 0.5f,panel_Text));
    }

    public void DisplayPlanche(int index)
    {
        Debug.Log(endCinematic);
        endCinematic = false;
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
        StartCoroutine(FadeINandOutImage(false, antreImg, delay,antre,true));

    }


    public SFXSound SFX_tok; 
    private void PlaySound(int plancheIndex, int imgIndex){

        if (plancheIndex == 2 && imgIndex == 0) // Jouer un son de toc toc
        {
            SFX_tok.PlayTheSound();
        }
    }

    
}
