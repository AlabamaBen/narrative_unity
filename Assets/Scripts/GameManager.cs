using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public static int step = 0;

    public bool dialoguesSeqFinished;

    // Use this for initialization
    void Awake() {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
            dialoguesSeqFinished = false;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        switch (step)
        {
            case 0:
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
                    ClickableObjetManager.instance.startPAndClick = true;
                    ClickableObjetManager.instance.finishedPAndCStep = false;
                }
                break;
            case 1:
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
}
