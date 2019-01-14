using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjetManager : MonoBehaviour {

    public List<GameObject> pointAndClickSet;
    public static int phase = 0;

    private List<ClickableObject> clickableObjets;

    public static ClickableObjetManager instance = null;

    public bool finishedPAndCStep;
    public bool startPAndClick;

    // Use this for initialization
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        foreach (GameObject obj in pointAndClickSet)
        {
            obj.SetActive(false);
        }
        pointAndClickSet[0].SetActive(true);
        finishedPAndCStep = true;
        startPAndClick = false;
    }
    
    public void ObjectClicked(ClickableObject obj)
    {
        switch (phase)
        {
            case 0:
                if (startPAndClick && !finishedPAndCStep && obj.gameObject.name == "Boite")
                {
                    finishedPAndCStep = true;
                    startPAndClick = false;
                }
                break;
            case 1:
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
