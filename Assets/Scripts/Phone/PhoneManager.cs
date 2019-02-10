using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour {

    public static PhoneManager instance = null;
    [HideInInspector]
    public Animator animator;
    private static int step = 0;
    private int substep = 0;

    [SerializeField]
    private Image screen;
    public List<Sprite> screenSprites;
    public List<GameObject> buttonToDesactivate;

    private string stringToDisplay;
    [SerializeField]
    private Text boiteEnvoi;
    [SerializeField]
    private Text smsTexte;
    private bool textDisplayed;

    [SerializeField]
    private GameObject smsObj;
    [SerializeField]
    private GameObject panel_Choix_sms;
    private int choix;

    [Header("SoundSFX")]
    public SFXSound SFX_Buzz;
    public SFXSound SFX_Tap;
    public SFXSound SFX_Send;
    public SFXSound SFX_Texting;




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
        choix = 0;
    }

    public void StartPhone()
    {
        screen.gameObject.SetActive(false);
        DesactivateAllButtons();
        buttonToDesactivate[0].SetActive(true);
        boiteEnvoi.text = "";
        smsObj.SetActive(false);
        smsObj.GetComponent<Image>().enabled = false;
        smsTexte.gameObject.SetActive(false);

        bool phoneOpened = animator.GetBool("phoneOpened");

        if (!phoneOpened)
        {
            StartCoroutine(LaunchAnimationPhone(true, 1f));
            SFX_Buzz.PlayTheSound();
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
                        SFX_Tap.PlayTheSound(); 
                        break;
                    case 1:
                        screen.sprite = screenSprites[substep];
                        DesactivateAllButtons();
                        buttonToDesactivate[2].SetActive(true);
                        substep++;
                        SFX_Tap.PlayTheSound();
                        break;
                    case 2:
                        SetDialogueBox("Haha, Ok ça marche !");
                        smsTexte.text = "Haha, Ok ça marche !";
                        SFX_Texting.PlayTheSound();
                        DesactivateAllButtons();
                        buttonToDesactivate[3].SetActive(true);
                        substep++;
                        break;
                    case 3:
                        if (!textDisplayed)
                        {
                            boiteEnvoi.text = "";
                            smsObj.SetActive(true);
                            smsObj.GetComponent<Animator>().SetTrigger("sendMessage");

                            StartCoroutine(LaunchAnimationPhone(false, 2f));
                            StartCoroutine(PlaySoundWithDelay(SFX_Send, 1f));
                            DesactivateAllButtons();
                            Invoke("FinishPhoneGame", 2.5F);
                        }
                        break;
                }
                break;
            case 1:
                switch (substep)
                {
                    case 0:
                        screen.sprite = screenSprites[2+substep];
                        screen.gameObject.SetActive(true);
                        DesactivateAllButtons();
                        SFX_Tap.PlayTheSound();
                        buttonToDesactivate[1].SetActive(true);
                        substep++;
                        break;
                    case 1:
                        screen.sprite = screenSprites[2 + substep];
                        DesactivateAllButtons();
                        buttonToDesactivate[2].SetActive(true);
                        SFX_Tap.PlayTheSound();
                        substep++;
                        break;
                    case 2:
                        SetDialogueBox("Ok !");
                        smsTexte.text = "Ok !";
                        DesactivateAllButtons();
                        buttonToDesactivate[3].SetActive(true);
                        SFX_Texting.PlayTheSound();
                        substep++;
                        break;
                    case 3:
                        if (!textDisplayed)
                        {
                            boiteEnvoi.text = "";
                            smsObj.SetActive(true);
                            smsObj.GetComponent<Animator>().SetTrigger("sendMessage");
                            StartCoroutine(PlaySoundWithDelay(SFX_Send, 1f));

                            StartCoroutine(LaunchAnimationPhone(false, 2f));
                            DesactivateAllButtons();
                            Invoke("FinishPhoneGame", 2.2F);
                        }
                        break;
                }
                break;
            case 2:
                switch (substep)
                {
                    case 0:
                        screen.sprite = screenSprites[4 + substep];
                        screen.gameObject.SetActive(true);
                        SFX_Tap.PlayTheSound();
                        DesactivateAllButtons();
                        buttonToDesactivate[1].SetActive(true);
                        substep++;
                        break;
                    case 1:
                        screen.sprite = screenSprites[4 + substep];
                        DesactivateAllButtons();
                        SFX_Tap.PlayTheSound();
                        this.panel_Choix_sms.SetActive(true);
                        choix = 0;
                        //buttonToDesactivate[2].SetActive(true);
                        substep++;
                        break;
                    case 2:
                        string choixMsg = "";
                        if (choix == 1)
                        {
                            choixMsg = "Oui, pas de soucis t’inquiète,\n on va tout déchirer !";
                            smsTexte.text = choixMsg;
                            SFX_Texting.PlayTheSound();
                            
                            buttonToDesactivate[3].SetActive(true);
                            substep++;
                        }
                        else if (choix == 2)
                        {
                            choixMsg = "Lucie, j’ai eu un soucis \navec mon ordinateur,\nmais t’inquiète,\nle devoir sera là demain\nà 8h30 sans soucis";
                            smsTexte.text = choixMsg;
                            SFX_Texting.PlayTheSound();

                            buttonToDesactivate[3].SetActive(true);
                            substep++;
                        }
                        break;
                    case 3:
                        smsObj.SetActive(true);
                        smsObj.GetComponent<Animator>().SetTrigger("sendMessage");
                        SFX_Send.PlayTheSound();

                        StartCoroutine(LaunchAnimationPhone(false, 2f));
                        DesactivateAllButtons();
                        Invoke("FinishPhoneGame", 2.2F);
                        substep++;
                        break;
                }
                break;
        }
    }
    private IEnumerator PlaySoundWithDelay(SFXSound sound, float delay)
    {
        yield return new WaitForSeconds(delay);
        sound.PlayTheSound();
    }

    public void SetChoice(int _choix)
    {
        choix = _choix;
        ClickOnMessageButton();
    }

    private void FinishPhoneGame()
    {
        phoneGameFinished = true;
        substep = 0;
        step++;
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
