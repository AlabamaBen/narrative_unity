using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour {

    public static PhoneManager instance = null;
    private Animator animator;
    private static int step = 0;
    private int substep = 0;

    [SerializeField]
    private Image screen;
    public List<Sprite> screenSprites;
    public List<GameObject> buttonToDesactivate;

    private string stringToDisplay;
    [SerializeField]
    private Text boiteEnvoi;
    private bool textDisplayed;

    [SerializeField]
    private GameObject smsObj;

    public bool phoneGameFinished = false;
    // Use this for initialization
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        textDisplayed = false;
    }

    public void StartPhone()
    {
        bool phoneOpened = animator.GetBool("phoneOpened");

        if (!phoneOpened)
        {
            StartCoroutine(LaunchAnimationPhone(true, 1f));
        }
    }
    
    public void ClickOnMessageButton()
    {

        switch (step)
        {
            case 0:
                switch (substep)
                {
                    case 0:
                        screen.sprite = screenSprites[substep];
                        screen.gameObject.SetActive(true);
                        DesactivateAllButtons();
                        buttonToDesactivate[1].SetActive(true);
                        substep++;
                        break;
                    case 1:
                        screen.sprite = screenSprites[substep];
                        DesactivateAllButtons();
                        buttonToDesactivate[2].SetActive(true);
                        substep++;
                        break;
                    case 2:
                        SetDialogueBox("Haha, Ok ça marche !");
                        DesactivateAllButtons();
                        buttonToDesactivate[3].SetActive(true);
                        substep++;
                        break;
                    case 3:
                        boiteEnvoi.text = "";
                        smsObj.SetActive(true);
                        smsObj.GetComponent<Animator>().SetTrigger("sendMessage");

                        StartCoroutine(LaunchAnimationPhone(false, 2f));
                        DesactivateAllButtons();
                        phoneGameFinished = true;
                        substep = 0;
                        step++;
                        break;
                }
                break;
            case 1:
                switch (substep)
                {
                    case 0:
                        screen.sprite = screenSprites[substep];
                        screen.gameObject.SetActive(true);
                        DesactivateAllButtons();
                        buttonToDesactivate[1].SetActive(true);
                        substep++;
                        break;
                }
                break;
        }
    }

    private IEnumerator LaunchAnimationPhone(bool phoneOpened, float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("phoneOpened", phoneOpened);
    }

    private void DesactivateAllButtons()
    {
        foreach (GameObject button in buttonToDesactivate)
        {
            button.SetActive(false);
        }
    }

    public void SetDialogueBox(string textMessage)
    {
        StartCoroutine(AnimateText(textMessage, 0.05F));
    }

    IEnumerator AnimateText(string strComplete, float speed)
    {
        textDisplayed = true;
        int i = 0;
        stringToDisplay = "";
        while (i < strComplete.Length)
        {
            stringToDisplay += strComplete[i++];
            boiteEnvoi.text = stringToDisplay;
            yield return new WaitForSeconds(speed);
        }
        textDisplayed = false;
    }
}
