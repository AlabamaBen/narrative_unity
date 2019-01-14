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
                    if (Input.anyKeyDown) // next on boite de dialogue
                    {
                        // Debug.Log("INTRO");
                        dialoguesSeqFinished = DialoguesManager.instance.DisplaySequenceDialogues();
                    }
                }
                else
                {
                    Debug.Log("INTRO FINISHED");
                    step++;
                    dialoguesSeqFinished = false;

                    // Init next step
                    ClickableObjetManager.instance.startPAndClick = true;
                    ClickableObjetManager.instance.finishedPAndCStep = false;
                }
                break;
            case 1:
                if (ClickableObjetManager.instance.finishedPAndCStep)
                {
                    Debug.Log("PHASE 01 POINT AND CLICK finished");
                    ClickableObjetManager.instance.finishedPAndCStep = false;
                }
                break;
            case 2:
                if (!dialoguesSeqFinished)
                {
                    if (Input.anyKeyDown) // next on boite de dialogue
                    {

                        dialoguesSeqFinished = DialoguesManager.instance.DisplaySequenceDialogues();
                    }
                }
                else
                {
                    Debug.Log("INTRO FINISHED");
                    step++;
                    dialoguesSeqFinished = false;

                    // Init next step
                }
                break;
        }
    }
}
