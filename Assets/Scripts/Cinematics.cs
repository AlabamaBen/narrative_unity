using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cinematics : MonoBehaviour
{
    private List<Image> images;
    private int compteur;
    private bool blockInput;
    public bool startCinematic;
    public bool endCinematic;
    public float speed;

    public static Cinematics instance = null;

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
            InitCinematics();
        }
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Use this for initialization
    void InitCinematics()
    {
        images = new List<Image>();
        foreach (Transform tr in this.transform.GetChild(0))
        {
            Image img = tr.GetComponent<Image>();
            img.enabled = false;
            images.Add(img);
        }
        compteur = 0;
        blockInput = false;
        startCinematic = false;
        endCinematic = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !blockInput && startCinematic)
        {
            blockInput = true;
            StartCoroutine(FadeVignette(false, compteur));
        }
    }

    IEnumerator FadeVignette(bool fadeAway, int index)
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
                    images[index].color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
            // fade from transparent to opaque
            else
            {
                images[index].enabled = true;
                // loop over 0.5 second
                for (float i = 0; i <= 1; i += Time.deltaTime * speed)
                {
                    // set color with i as alpha
                    images[index].color = new Color(1, 1, 1, i);
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
                StartCoroutine(FadeImage(true, img));
            }
            StartCoroutine(FadeImage(true, this.transform.GetChild(0).GetComponent<Image>()));
        }
    }

    IEnumerator FadeImage(bool fadeAway,Image img)
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
            this.transform.GetChild(0).gameObject.SetActive(false);
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

    public void DisplayCinematic(int index)
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        InitCinematics();
        StartCoroutine(FadeImage(false, this.transform.GetChild(0).GetComponent<Image>()));
        StartCoroutine(FadeVignette(false, compteur));
        startCinematic = true;
        blockInput = true;
    }

    private void WaitAndUnblockInput()
    {
        blockInput = false;
    }

    public void DisplayText(string msg)
    {
        this.transform.GetChild(1).gameObject.SetActive(true);
        this.transform.GetChild(1).GetComponent<Text>().text = msg;
    }
}
