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
    }
	
	// Update is called once per frame
	void Update ()
    {
        Scene m_Scene;
        switch (step)
        {
            case 0: // Intro
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
                        step=3;
                        dialoguesSeqFinished = false;

                        // Init next step
                        PhoneManager.instance.StartPhone();
                    }
                }
                break;
            case 1: // Smartphone
                if(PhoneManager.instance.phoneGameFinished)
                {
                    // Init next step
                    ClickableObjetManager.instance.startPAndClick = true;
                    ClickableObjetManager.instance.finishedPAndCStep = false;
                    step++;
                }
                break;
            case 2:
                if (ClickableObjetManager.instance.finishedPAndCStep)
                {
                    step++;
                    ClickableObjetManager.instance.finishedPAndCStep = false;
                }
                break;
            case 3:
                m_Scene = SceneManager.GetActiveScene();
                if (m_Scene.name != "minigame_1" && !sceneLoaded)
                {
                    //TEST 
                    Ring.Game_End = true;

                    sceneLoaded = true;
                    StartCoroutine(LoadScene("minigame_1",2f));
                }
                else
                {
                    if (Ring.Game_End)
                    {
                        step++;
                        sceneLoaded = false;
                    }
                }
                break;
            case 4:
                m_Scene = SceneManager.GetActiveScene();
                if (m_Scene.name != "MainScene" && !sceneLoaded)
                {
                    sceneLoaded = true;
                    //StartCoroutine(LoadScene("MainScene", 0f));
                    StartCoroutine(LoadYourAsyncScene("MainScene"));

                    step++;
                }
                break;
            case 5:
                if (!dialoguesSeqFinished)
                {
                    if (!SpeechManager.instance.startDialogue && SpeechManager.instance.displayDialogue.dialogue_Alex_Nat!=null && SpeechManager.instance.displayDialogue.messagesList != null) // NEED TO FIND BETTER SOLUTION
                    {
                        blockInput = true;
                        // Display first line of dialogue
                        SpeechManager.instance.displayDialogue.dialogue_Alex_Nat.SetActive(true);
                        SpeechManager.instance.DisplayNextSequenceDialogue();
                        SpeechManager.instance.startDialogue = true;
                        Invoke("waitAndUnblockInput", 1f);
                    }
                    if (!blockInput && SpeechManager.instance.startDialogue && Input.anyKeyDown && !SpeechManager.instance.textDisplayed) // Player click to display next dialog
                    {
                        dialoguesSeqFinished = SpeechManager.instance.DisplayNextSequenceDialogue();
                    }
                }
                else
                {
                    SpeechManager.instance.displayDialogue.dialogue_Alex_Nat.SetActive(false);
                    SpeechManager.instance.startDialogue = false;
                    step++;
                    dialoguesSeqFinished = false;
                    sceneLoaded = false;

                    // Init next step
                }
                break;
            case 6:
                break;
        }
    }
    
    IEnumerator LoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
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
}
