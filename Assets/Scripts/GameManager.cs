using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public static int step = 0;

    public bool dialoguesSeqFinished;

    private bool sceneLoaded;

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
                    if (!DialoguesManager.instance.startDialogue)
                        DialoguesManager.instance.startDialogue = true;
                }
                else{
                    Debug.Log("INTRO FINISHED");
                    DialoguesManager.instance.startDialogue = false;
                    step++;
                    dialoguesSeqFinished = false;

                    // Init next step
                    PhoneManager.instance.StartPhone();
                }
                break;
            case 1: // Smartphone
                if(PhoneManager.instance.phoneGameFinished)
                {
                    Debug.Log("PHONE FINISHED");
                    // Init next step
                    ClickableObjetManager.instance.startPAndClick = true;
                    ClickableObjetManager.instance.finishedPAndCStep = false;
                    step++;
                }
                break;
            case 2:
                if (ClickableObjetManager.instance.finishedPAndCStep)
                {
                    Debug.Log("PHASE 01 POINT AND CLICK finished");
                    step++;
                    ClickableObjetManager.instance.finishedPAndCStep = false;
                }
                break;
            case 3:
                m_Scene = SceneManager.GetActiveScene();
                if (m_Scene.name != "minigame_1" && !sceneLoaded)
                {
                    sceneLoaded = true;
                    StartCoroutine(LoadScene("minigame_1",2f));
                }
                else
                {
                    if (Ring.Cleaned)
                    {
                        Debug.Log("minigame_1 finished");
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
                    StartCoroutine(LoadScene("MainScene", 2f));
                }
                break;
            case 5:
                if (!dialoguesSeqFinished)
                {
                    if (Input.anyKeyDown) // next on boite de dialogue
                    {
                        dialoguesSeqFinished = DialoguesManager.instance.DisplaySequenceDialogues();
                    }
                }
                else
                {
                    step++;
                    dialoguesSeqFinished = false;

                    // Init next step
                }
                break;
        }
    }
    
    IEnumerator LoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
