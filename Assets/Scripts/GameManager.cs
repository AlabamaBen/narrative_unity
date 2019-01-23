using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public static int step = -3;

    public bool dialoguesSeqFinished;

    private bool sceneLoaded;
    public bool blockInput = false;

    public static bool blockMovementOnGround = false;
    public GameObject curtains_Panel;

    public SFXSound SFX_Door;


    public GameObject worlds_Canvas;


    // Constant from DialoguesManager
    // public static int sequenceIndex;
    // public static int dialogueIndex;

    // Use this for initialization
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
            dialoguesSeqFinished = false;
            sceneLoaded = false;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        // TEST
        //Cinematics.instance.DisplayCinematic(2);
        //step = 17;
        //dialoguesSeqFinished = false;
        //ClickableObjetManager.phase = 1;
        //step = 41;

    }

    // Update is called once per frame
    void Update()
    {
        Scene m_Scene;
        switch (step)
        {
            case -3:
                // init next step
                Cinematics.instance.bgIntro.SetActive(true);
                Cinematics.instance.endCinematic = false;
                Cinematics.instance.DisplayPlanche(0);
                step++;
                break;
            case -2: //  Cinematique 4 - Met la bague
                if (Cinematics.instance.endCinematic)
                {
                    Cinematics.instance.bgIntro.SetActive(false);
                    // Init next step
                    blockInput = true;
                    Invoke("waitAndUnblockInput", 0f);
                    step++;
                }
                break;
            case -1: 
                if (!blockInput)
                {
                    step++;
                }
                break;
            case 0: // 0 - Intro, 2 pensees défilent
                if (!dialoguesSeqFinished)
                {
                    if (!SpeechManager.instance.startDialogue)
                    {
                        SpeechManager.instance.DisplayNextSequenceMonolog();
                        SpeechManager.instance.startDialogue = true;
                    }
                }
                else
                {
                    SpeechManager.instance.startDialogue = false;
                    if (!SpeechManager.instance.displayMonologue.animator.GetBool("openMonolog")) // if player has closed last thought (open Monolog closed)
                    {

                        dialoguesSeqFinished = false;

                        // Init next step
                        PhoneManager.instance.StartPhone();
                        step++;
                    }
                }
                break;
            case 1: // Message de Lucie - Smartphone
                if (PhoneManager.instance.phoneGameFinished)
                {
                    PhoneManager.instance.phoneGameFinished = false;

                    // Init next step
                    ClickableObjetManager.instance.StartClickableObject();
                    step++;
                }
                break;
            case 2: // Click sur la boite mystérieuse de la soiree d'hier
                if (ClickableObjetManager.instance.finishedPAndCStep)
                {
                    step++;
                    ClickableObjetManager.instance.finishedPAndCStep = false;
                }
                break;
            case 3: // Lancement mini jeu boite
                m_Scene = SceneManager.GetActiveScene();
                if (m_Scene.name != "minigame_1" && !sceneLoaded)
                {
                    //TEST, next line to remove 
                    //Ring.Game_End = true;

                    CurtainsFadeIn();
                    sceneLoaded = true;
                    StartCoroutine(LoadScene("minigame_1", 2f));
                }

                if (m_Scene.name == "minigame_1" && curtains_Panel.GetComponent<Animator>().gameObject.activeSelf && curtains_Panel.GetComponent<Animator>().GetBool("fadeIn"))
                {
                    CurtainsFadeOut();
                }
                else
                {
                    if (Ring.Game_End)
                    {
                        CurtainsFadeIn();
                        step++;
                    }
                }
                break;
            case 4: // Reload de la scene principale
                m_Scene = SceneManager.GetActiveScene();
                if (m_Scene.name != "MainScene" && !sceneLoaded)
                {
                    CurtainsFadeOut();
                    sceneLoaded = true;
                    //StartCoroutine(LoadScene("MainScene", 0f));
                    StartCoroutine(LoadYourAsyncScene("MainScene"));
                    dialoguesSeqFinished = false;
                }
                break;
            case 5: // 1 - Debut 1er rencontre dialogue Natyahs et Alex
                if (!dialoguesSeqFinished)
                {
                    if (!SpeechManager.instance.startDialogue)
                    {
                        blockInput = true;
                        blockMovementOnGround = true;
                        // Display first line of dialogue
                        SpeechManager.instance.DisplayFirstSequence();

                        Invoke("waitAndUnblockInput", 1f);
                    }
                    else if (!blockInput && SpeechManager.instance.startDialogue && Input.anyKeyDown && !SpeechManager.instance.textDisplayed) // Player click to display next dialog
                    {
                        dialoguesSeqFinished = SpeechManager.instance.DisplayNextSequenceDialogue();
                    }
                }
                else
                {
                    if (!SpeechManager.instance.textDisplayed) // Player click to end dialog
                    {
                        SpeechManager.instance.HideDialog();
                        blockMovementOnGround = false;
                        step++;
                        dialoguesSeqFinished = false;
                        sceneLoaded = false;
                    }

                    // Init next step
                }
                break;
            case 6: // 1 - Fin de dialogue rencontre entre Natyahs et Alex
                if (!dialoguesSeqFinished && SpeechManager.instance.displayDialogue.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !SpeechManager.instance.displayDialogue.animator.IsInTransition(0))
                { // Current animation (Fadeout Dialogues) finished
                    dialoguesSeqFinished = true;
                    SpeechManager.instance.displayDialogue.dialogue_Alex_Nat.SetActive(false);
                    // Init next step
                    PhoneManager.instance.StartPhone();
                    step++;
                }
                break;
            case 7: // Message de Lucie - Smartphone
                if (PhoneManager.instance.phoneGameFinished)
                {
                    // Init next step
                    step++;
                    PhoneManager.instance.phoneGameFinished = false;
                    dialoguesSeqFinished = false;
                }
                break;
            case 8: // 2 - Debut dialogue rangement Natyahs et Alex
                if (!dialoguesSeqFinished)
                {
                    if (!SpeechManager.instance.startDialogue)
                    {
                        blockInput = true;
                        blockMovementOnGround = true;
                        // Display first line of dialogue
                        SpeechManager.instance.DisplayFirstSequence();

                        Invoke("waitAndUnblockInput", 1f);
                    }
                    else if (!blockInput && SpeechManager.instance.startDialogue && Input.anyKeyDown && !SpeechManager.instance.textDisplayed) // Player click to display next dialog
                    {
                        dialoguesSeqFinished = SpeechManager.instance.DisplayNextSequenceDialogue();
                    }
                }
                else
                {
                    if (!SpeechManager.instance.textDisplayed) // Player click to end dialog
                    {
                        SpeechManager.instance.HideDialog();
                        blockMovementOnGround = false;
                        step++;
                        dialoguesSeqFinished = false;
                        Invoke("waitAndUnblockInput", 2f);
                        blockInput = true;
                    }

                    // Init next step
                }
                break;
            case 9: // 2 - Fin de dialogue entre Natyahs et Alex
                if (!blockInput && !dialoguesSeqFinished && SpeechManager.instance.displayDialogue.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !SpeechManager.instance.displayDialogue.animator.IsInTransition(0))
                { // Current animation (Fadeout Dialogues) finished
                    dialoguesSeqFinished = true;
                    SpeechManager.instance.displayDialogue.dialogue_Alex_Nat.SetActive(false);

                    // Init next step
                    ClickableObjetManager.instance.StartClickableObject();
                    step++;
                }
                break;
            case 10: // Click et renverse verrre eau
                if (ClickableObjetManager.instance.finishedPAndCStep)
                {
                    step++;
                    ClickableObjetManager.instance.finishedPAndCStep = false;
                    // init next step
                    Cinematics.instance.DisplayPlanche(1);
                    blockMovementOnGround = true;
                }
                break;
            case 11: // Cinematique 2, Mere qui arrive et Pc dans l'eau
                if (Cinematics.instance.endCinematic)
                {
                    //init next step
                    Cinematics.instance.endCinematic = false;
                    Cinematics.instance.DisplayPlanche(2);
                    step++;
                }
                break;
            case 12: // Fin de la cinematique 2
                if (Cinematics.instance.endCinematic)
                {
                    blockInput = true;
                    //Cinematics.instance.DisplayText("");
                    blockMovementOnGround = false;
                    dialoguesSeqFinished = false;
                    step++;
                    Invoke("waitAndUnblockInput", 1.75f);
                    CurtainsFadeIn();
                }
                break;
            case 13: // Fade IN / Fade Out
                if (!blockInput)
                {
                    blockInput = true;
                    CurtainsFadeOut();
                    Invoke("waitAndUnblockInput", 1f);
                    blockMovementOnGround = true;
                    step++;
                }
                break;
            case 14:
                if (!blockInput)
                {
                    // BENJAMIN : Jouer un son de porte qui claque

                    SFX_Door.PlayTheSound();

                    blockInput = true;
                    SpeechManager.instance.displayMonologue.SetMonolog("Ah enfin tranquille !");
                    // init next step
                    step++;
                }
                break;
            case 15:
                if (!blockInput)
                {
                    blockInput = true;
                    SpeechManager.instance.displayMonologue.SetMonolog("Bon j'ai du boulot");
                    // init next step

                    step++;
                }
                break;
            case 16: // Lancement cinématique 3 - Mon pc est mort
                if (!blockInput)
                {
                    // init next step
                    Cinematics.instance.endCinematic = false;
                    Cinematics.instance.DisplayPlanche(3);
                    step++;
                }
                break;
            case 17: // Fin de la cinematique 3
                if (Cinematics.instance.endCinematic)
                {
                    //init next step
                    Cinematics.instance.endCinematic = false;
                    Cinematics.instance.DisplayPlanche(4);
                    step++;
                }
                break;
            case 18: // Fin de la cinematique 3
                if (Cinematics.instance.endCinematic)
                {
                    //init next step
                    PhoneManager.instance.StartPhone();
                    step++;
                }
                break;
            case 19: // Message de Lucie - Smartphone
                if (PhoneManager.instance.phoneGameFinished)
                {
                    PhoneManager.instance.phoneGameFinished = false;

                    // Init next step
                    blockInput = true;
                    dialoguesSeqFinished = false;
                    Invoke("waitAndUnblockInput", 1.5f);
                    step++;
                }
                break;
            case 20: // 3 - Debut dialogue Natyahs et Alex catastrophe, besoin d'un nouveau pc 1er voeu nouveau PC
                if (!dialoguesSeqFinished && !blockInput)
                {
                    if (!SpeechManager.instance.startDialogue)
                    {
                        blockInput = true;
                        blockMovementOnGround = true;
                        // Display first line of dialogue
                        SpeechManager.instance.DisplayFirstSequence();

                        Invoke("waitAndUnblockInput", 1f);
                    }
                    else if (!blockInput && SpeechManager.instance.startDialogue && Input.anyKeyDown && !SpeechManager.instance.textDisplayed) // Player click to display next dialog
                    {
                        dialoguesSeqFinished = SpeechManager.instance.DisplayNextSequenceDialogue();
                    }
                }
                else if (dialoguesSeqFinished)
                {
                    if (!SpeechManager.instance.textDisplayed) // Player click to end dialog
                    {
                        SpeechManager.instance.HideDialog();
                        blockMovementOnGround = false;
                        dialoguesSeqFinished = false;
                        step++;
                    }

                    // Init next step
                }
                break;
            case 21: // 3 - Fin de dialogue entre Natyahs et Alex besoin d'un nouveau pc 1er voeu nouveau PC
                if (!dialoguesSeqFinished && SpeechManager.instance.displayDialogue.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !SpeechManager.instance.displayDialogue.animator.IsInTransition(0))
                { // Current animation (Fadeout Dialogues) finished
                    dialoguesSeqFinished = true;
                    SpeechManager.instance.displayDialogue.dialogue_Alex_Nat.SetActive(false);

                    // init next step
                    Cinematics.instance.endCinematic = false;
                    Cinematics.instance.DisplayPlanche(5); 
                    step++;
                }
                break;
            case 22: //  Cinematique 4 - Met la bague
                if (Cinematics.instance.endCinematic)
                {
                    // Init next step
                    blockInput = true;
                    CurtainsFadeIn();
                    Invoke("waitAndUnblockInput", 2f);
                    step++;
                }
                break;
            // TO REDO
            case 23: // Blink antre
                if (!blockInput)
                {
                    Cinematics.instance.endCinematic = false;
                    Cinematics.instance.DisplayAntre(5f, 0.3f);
                    step++;
                }
                break;
            case 24: // Fin de la cinematique antre
                if (Cinematics.instance.endCinematic)
                {
                    blockInput = true;
                    CurtainsFadeOut();
                    Invoke("waitAndUnblockInput", 1f);
                    step++;
                }
                break;
            case 25: // Fin du fade out
                if (!blockInput)
                {
                    blockInput = true;
                    step++;
                    Invoke("waitAndUnblockInput", 1.5f);
                }
                break;
            case 26: // Monologue sur vision antre
                if (!blockInput)
                {
                    // init next step
                    blockInput = true;
                    // Debug.Log("C'était quoi cette vision");
                    SpeechManager.instance.displayMonologue.SetMonolog("C'était quoi cette vision ?");
                    step++;
                }
                break;
            case 27:
                if (!blockInput)
                {
                    // init next step

                    Cinematics.instance.endCinematic = false;
                    Cinematics.instance.DisplayPlanche(6); 
                    dialoguesSeqFinished = false;
                    step++;
                }
                break;
            case 28: // 4 - Dialogue Natyahs et Alex Qu’est-ce que t’as fait de mon ordinateur ?, Bon j'ai du travail
                if (!dialoguesSeqFinished && Cinematics.instance.endCinematic)
                {
                    if (!SpeechManager.instance.startDialogue)
                    {
                        blockInput = true;
                        blockMovementOnGround = true;
                        // Display first line of dialogue
                        SpeechManager.instance.DisplayFirstSequence();

                        Invoke("waitAndUnblockInput", 1f);
                    }
                    else if (!blockInput && SpeechManager.instance.startDialogue && Input.anyKeyDown && !SpeechManager.instance.textDisplayed) // Player click to display next dialog
                    {
                        dialoguesSeqFinished = SpeechManager.instance.DisplayNextSequenceDialogue();
                    }
                }
                else if (dialoguesSeqFinished && Cinematics.instance.endCinematic)
                {
                    if (!SpeechManager.instance.textDisplayed) // Player click to end dialog
                    {
                        SpeechManager.instance.HideDialog();
                        blockMovementOnGround = false;
                        dialoguesSeqFinished = false;
                        step++;
                    }

                }
                break;
            case 29: // 4 - Fin de dialogue Natyahs et Alex Qu’est-ce que t’as fait de mon ordinateur ?, Bon j'ai du travail
                if (!dialoguesSeqFinished && SpeechManager.instance.displayDialogue.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !SpeechManager.instance.displayDialogue.animator.IsInTransition(0))
                { // Current animation (Fadeout Dialogues) finished
                    dialoguesSeqFinished = true;
                    SpeechManager.instance.displayDialogue.dialogue_Alex_Nat.SetActive(false);

                    // Init next step
                    step++;
                }
                break;
            case 30: // Lancement mini devoir
                m_Scene = SceneManager.GetActiveScene();
                if (m_Scene.name != "minigame_2" && !sceneLoaded)
                {
                    //TEST, next line to remove
                    //Minigame2_Behavior.MINIGAME2_END = true;

                    CurtainsFadeIn();
                    sceneLoaded = true;
                    StartCoroutine(LoadScene("minigame_2", 2f));
                }

                if (m_Scene.name == "minigame_2" && curtains_Panel.GetComponent<Animator>().gameObject.activeSelf && curtains_Panel.GetComponent<Animator>().GetBool("fadeIn"))
                {
                    CurtainsFadeOut();
                }
                else
                {
                    if (Minigame2_Behavior.MINIGAME2_END)
                    {
                        CurtainsFadeIn();
                        step++;
                    }
                }
                break;
            case 31: // Reload de la scene principale
                m_Scene = SceneManager.GetActiveScene();
                if (m_Scene.name != "MainScene" && !sceneLoaded)
                {
                    CurtainsFadeOut();
                    sceneLoaded = true;
                    //StartCoroutine(LoadScene("MainScene", 0f));
                    StartCoroutine(LoadYourAsyncScene("MainScene"));
                    dialoguesSeqFinished = false;
                }
                break;
            case 32: // 5 - Debut dialogue Natyahs et Alex, je vais jamais y arriver, aide moi Nat pls
                if (!dialoguesSeqFinished)
                {
                    if (!SpeechManager.instance.startDialogue)
                    {
                        blockInput = true;
                        blockMovementOnGround = true;
                        // Display first line of dialogue
                        SpeechManager.instance.DisplayFirstSequence();

                        Invoke("waitAndUnblockInput", 1f);
                    }
                    else if (!blockInput && SpeechManager.instance.startDialogue && Input.anyKeyDown && !SpeechManager.instance.textDisplayed) // Player click to display next dialog
                    {
                        dialoguesSeqFinished = SpeechManager.instance.DisplayNextSequenceDialogue();
                    }
                }
                else
                {
                    if (!SpeechManager.instance.textDisplayed) // Player click to end dialog
                    {
                        SpeechManager.instance.HideDialog();
                        blockMovementOnGround = false;
                        dialoguesSeqFinished = false;
                        step++;
                    }

                    // Init next step
                }
                break;
            case 33: // Fin ,  5 - Debut dialogue Natyahs et Alex, je vais jamais y arriver, aide moi Nat pls
                if (!dialoguesSeqFinished && SpeechManager.instance.displayDialogue.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !SpeechManager.instance.displayDialogue.animator.IsInTransition(0))
                { // Current animation (Fadeout Dialogues) finished
                    dialoguesSeqFinished = true;
                    SpeechManager.instance.displayDialogue.dialogue_Alex_Nat.SetActive(false);

                    // Init next step
                    blockInput = true;
                    CurtainsFadeIn();
                    Invoke("waitAndUnblockInput", 2f);
                    step++;
                }
                break;
            case 34: // Blink antre
                if (!blockInput)
                {
                    Cinematics.instance.endCinematic = false;
                    Cinematics.instance.DisplayAntre(2f, 0.3f);
                    step++;
                }
                break;
            case 35: // Fin de la cinematique antre
                if (Cinematics.instance.endCinematic)
                {
                    blockInput = true;
                    CurtainsFadeOut();
                    Invoke("waitAndUnblockInput", 1f);
                    step++;
                }
                break;
            case 36: // Fin du fade out
                if (!blockInput)
                {
                    blockInput = true;
                    step++;
                    Invoke("waitAndUnblockInput", 1.5f);
                }
                break;
            case 37: // Fin du fade out - Cinematique  - Convocation
                if (!blockInput)
                {
                    // init next step
                    Cinematics.instance.endCinematic = false;
                    Cinematics.instance.DisplayPlanche(7);
                    step++;
                }
                break;
            case 38: //  Cinematique 4 - Met la bague
                if (Cinematics.instance.endCinematic)
                {
                    blockInput = true;
                    dialoguesSeqFinished = false;
                    step++;
                    Invoke("waitAndUnblockInput", 1.5f);
                }
                break;
            case 39: // 6 - Debut dialogue Natyahs et Alex, pourquoi je suis convoquee ?
                if (!dialoguesSeqFinished && !blockInput)
                {
                    if (!SpeechManager.instance.startDialogue)
                    {
                        blockInput = true;
                        blockMovementOnGround = true;
                        // Display first line of dialogue
                        SpeechManager.instance.DisplayFirstSequence();

                        Invoke("waitAndUnblockInput", 1f);
                    }
                    else if (!blockInput && SpeechManager.instance.startDialogue && Input.anyKeyDown && !SpeechManager.instance.textDisplayed) // Player click to display next dialog
                    {
                        dialoguesSeqFinished = SpeechManager.instance.DisplayNextSequenceDialogue();
                    }
                }
                else if (dialoguesSeqFinished && !blockInput)
                {
                    if (!SpeechManager.instance.textDisplayed) // Player click to end dialog
                    {
                        SpeechManager.instance.HideDialog();
                        blockMovementOnGround = false;
                        dialoguesSeqFinished = false;
                        step++;
                    }

                    // Init next step
                }
                break;
            case 40: // 6 - Fin pourquoi je suis convoquee ?
                if (!dialoguesSeqFinished && SpeechManager.instance.displayDialogue.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !SpeechManager.instance.displayDialogue.animator.IsInTransition(0))
                { // Current animation (Fadeout Dialogues) finished
                    dialoguesSeqFinished = true;
                    SpeechManager.instance.displayDialogue.dialogue_Alex_Nat.SetActive(false);

                    // Init next step
                    Debug.Log(" Fin pourquoi je suis convoquee ?");
                    blockInput = true;
                    Invoke("waitAndUnblockInput", 2f);
                    step++;
                }
                break;
            case 41: // Fin du fade out - Cinematique  - Convocation, Choix de cartes
                if (!blockInput)
                {
                    // init next step
                    worlds_Canvas.SetActive(true);
                }
                break;
            case 42: // Fin de choix cartes / Fade IN 
                blockInput = true;
                CurtainsFadeIn();
                Invoke("waitAndUnblockInput", 3f);
                step++;
                break;
            case 43: // Lancement mini jeu boite

                if (!blockInput)
                {
                    m_Scene = SceneManager.GetActiveScene();
                    if (m_Scene.name != "final_room" && !sceneLoaded)
                    {

                        worlds_Canvas.SetActive(false);
                        curtains_Panel.SetActive(false);
                        sceneLoaded = true;
                        StartCoroutine(LoadYourAsyncScene("final_room"));
                    }
                }
                break;
        }
    }


    IEnumerator LoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        sceneLoaded = false;
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (step == 31)
        {
            // Erase collected item from step 31
            ClickableObjetManager.instance.DeActivateCleanedObjects();
        }
        else if (step == 41)
        {
            curtains_Panel.SetActive(false);
            worlds_Canvas.SetActive(false);
            Debug.Log("THEE END");
        }
        step++;
    }


    private void waitAndUnblockInput()
    {
        blockInput = false;
    }

    private void CurtainsFadeIn()
    {
        curtains_Panel.GetComponent<Animator>().SetBool("fadeIn", true);
        Color newColor = curtains_Panel.GetComponent<SpriteRenderer>().color;
        newColor.a = 0;
        curtains_Panel.GetComponent<SpriteRenderer>().color = newColor;
    }

    public void CurtainsFadeOut()
    {
        //Debug.Log("CurtainsFadeOut");
        Color newColor = curtains_Panel.GetComponent<SpriteRenderer>().color;
        newColor.a = 255;
        curtains_Panel.GetComponent<SpriteRenderer>().color = newColor;
        curtains_Panel.SetActive(true);
        curtains_Panel.GetComponent<Animator>().SetBool("fadeIn", false);
        Invoke("DeActivateCurtains", 1F);
    }

    public void DeActivateCurtains()
    {
        //Debug.Log("DeActivateCurtains");
        Color newColor = curtains_Panel.GetComponent<SpriteRenderer>().color;
        newColor.a = 0;
        curtains_Panel.GetComponent<SpriteRenderer>().color = newColor;
    }


    public void ChoixCarte(int choix)
    {
        Debug.Log("choix : " + choix);
        step++;
    }
}
