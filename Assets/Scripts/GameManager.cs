using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public static int step = 0;

    public bool dialoguesSeqFinished;

    private bool sceneLoaded;
    private bool blockInput = false;

    public static bool blockMovementOnGround = false;
    public GameObject curtains_Panel;


    // Constant from DialoguesManager
    // public static int sequenceIndex;
    // public static int dialogueIndex;

    // Use this for initialization
    void Awake() {
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
        //step=10;
        //ClickableObjetManager.phase = 1;
    }

    // Update is called once per frame
    void Update ()
    {
        Scene m_Scene;
        switch (step)
        {
            case 0: // Intro - 2 pensees défilent
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
                        // TEST
                        //step=5;
                        step++;

                        dialoguesSeqFinished = false;

                        // Init next step
                        PhoneManager.instance.StartPhone();
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
                    Ring.Game_End = true;

                    CurtainsFadeIn();
                    sceneLoaded = true;
                    StartCoroutine(LoadScene("minigame_1",2f));
                }

                if(m_Scene.name == "minigame_1" && curtains_Panel.GetComponent<Animator>().gameObject.activeSelf && curtains_Panel.GetComponent<Animator>().GetBool("fadeIn"))
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

                    step++;
                }
                break;
            case 5: //Debut 1er dialogue Natyahs et Alex
                if (!dialoguesSeqFinished)
                {
                    if (!SpeechManager.instance.startDialogue && SpeechManager.instance.displayDialogue.dialogue_Alex_Nat!=null && SpeechManager.instance.displayDialogue.messagesList != null ) // NEED TO FIND BETTER SOLUTION
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
            case 6: // Fin de dialogue entre Natyahs et Alex
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
            case 8: // Debut dialogue rangement Natyahs et Alex
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
                case 9: // Fin de dialogue entre Natyahs et Alex
                if (!blockInput && !dialoguesSeqFinished && SpeechManager.instance.displayDialogue.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !SpeechManager.instance.displayDialogue.animator.IsInTransition(0))
                { // Current animation (Fadeout Dialogues) finished
                    dialoguesSeqFinished = true;
                    SpeechManager.instance.displayDialogue.dialogue_Alex_Nat.SetActive(false);
                    
                    // Init next step
                    ClickableObjetManager.instance.StartClickableObject();
                    step++;
                }
                break;
            case 10: // Click sur verrre eau
                if (ClickableObjetManager.instance.finishedPAndCStep)
                {
                    step++;
                    ClickableObjetManager.instance.finishedPAndCStep = false;
                    // init next step
                    Cinematics.instance.DisplayCinematic(2);
                }
                break;
            case 11: // Fin de la cinematique 2
                if (Cinematics.instance.endCinematic)
                {
                    //Cinematics.instance.DisplayText("");
                    CurtainsFadeIn();
                    step++;
                    Invoke("CurtainsFadeOut", 1F);
                    dialoguesSeqFinished = false;
                }
                break;
            case 12: //Debut dialogue Natyahs et Alex catastrophe
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
                    }

                    // Init next step
                }
                break;
            case 13: // Fin de dialogue entre Natyahs et Alex
                if (!dialoguesSeqFinished && SpeechManager.instance.displayDialogue.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !SpeechManager.instance.displayDialogue.animator.IsInTransition(0))
                { // Current animation (Fadeout Dialogues) finished
                    dialoguesSeqFinished = true;
                    // Init next step
                    PhoneManager.instance.StartPhone();
                    step++;
                }
                break;
            case 14: // Message de Lucie - Smartphone
                if (PhoneManager.instance.phoneGameFinished)
                {
                    // Init next step
                    step++;
                    PhoneManager.instance.phoneGameFinished = false;
                    dialoguesSeqFinished = false;
                }
                break;
            case 15: //Suite dialogue Natyahs et Alex catastrophe
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
            case 16: // Fin de dialogue entre Natyahs et Alex
                if (!dialoguesSeqFinished && SpeechManager.instance.displayDialogue.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !SpeechManager.instance.displayDialogue.animator.IsInTransition(0))
                { // Current animation (Fadeout Dialogues) finished
                    dialoguesSeqFinished = true;
                    SpeechManager.instance.displayDialogue.dialogue_Alex_Nat.SetActive(false);


                    // Init next step
                    step++;
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
    }

    private void waitAndUnblockInput()
    {
        blockInput = false;
    }

    private void CurtainsFadeIn()
    {
        curtains_Panel.SetActive(true);
        curtains_Panel.GetComponent<Animator>().SetBool("fadeIn", true);
        Color newColor = curtains_Panel.GetComponent<SpriteRenderer>().color;
        newColor.a = 0;
        curtains_Panel.GetComponent<SpriteRenderer>().color = newColor;
    }

    public void CurtainsFadeOut()
    {
        Debug.Log("CurtainsFadeOut");
        Color newColor = curtains_Panel.GetComponent<SpriteRenderer>().color;
        newColor.a = 255;
        curtains_Panel.GetComponent<SpriteRenderer>().color = newColor;
        curtains_Panel.SetActive(true);
        curtains_Panel.GetComponent<Animator>().SetBool("fadeIn", false);
        Invoke("DeActivateCurtains", 1F);
    }

    public void DeActivateCurtains()
    {
        Debug.Log("DeActivateCurtains");
        Color newColor = curtains_Panel.GetComponent<SpriteRenderer>().color;
        newColor.a = 0;
        curtains_Panel.GetComponent<SpriteRenderer>().color = newColor;
        curtains_Panel.SetActive(false);
    }
}
