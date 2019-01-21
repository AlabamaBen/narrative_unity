using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjetManager : MonoBehaviour
{

    public GameObject pointAndClickSet;
    private List<ObjectTemplate> objectCollections;
    private List<ClickableObject> clickableObjets; // current objects list that are interractable

    public static int phase = 0;

    public static ClickableObjetManager instance = null;

    public bool finishedPAndCStep;
    public bool startPAndClick;

    public GameObject verre_eau;

    public PlayerMovement playerMovement;

    // Use this for initialization
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
            ObjectCollections pointAndClickSetScript = pointAndClickSet.GetComponent<ObjectCollections>();
            objectCollections = pointAndClickSetScript.objects;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        finishedPAndCStep = true;
        startPAndClick = false;
    }

    public void SetActivableObjects(int _phase)
    {
        clickableObjets = new List<ClickableObject>();

        // Deactivate all objects in each set.
        foreach (Transform tr in pointAndClickSet.transform)
        {
            tr.gameObject.GetComponent<ClickableObject>().isInterractable = false;
        }

        // Activate object from 1st step
        foreach (GameObject obj in objectCollections[_phase].gameObjectList)
        {
            obj.GetComponent<ClickableObject>().isInterractable = true;
            clickableObjets.Add(obj.GetComponent<ClickableObject>());
        }
    }


    public void StartClickableObject()
    {
        switch (phase)
        {
            case 0:
                startPAndClick = true;
                finishedPAndCStep = false;
                SetActivableObjects(phase);
                BlinkAllObjects();
                break;
            case 1:
                SetActivableObjects(phase);
                startPAndClick = true;
                finishedPAndCStep = false;
                SetActivableObjects(phase);
                BlinkAllObjects();
                break;
        }
    }

    public void ObjectClicked(ClickableObject obj)
    {
        // Debug.Log("ObjectClicked "+phase);
        switch (phase)
        {
            case 0:
                // Display Thought about objects
                if (!SpeechManager.instance.textDisplayed && startPAndClick && !finishedPAndCStep) // !dialogue.Equals("")
                {
                    SpeechManager.instance.DisplayThoughOnObject(obj.dialogue);

                    // If the box is clicked end, mini game
                    if (obj.gameObject.name == "Boite")
                    {
                        StopBlinkAllObjects();
                        finishedPAndCStep = true;
                        startPAndClick = false;

                        // Set next set

                        // Deactivate all objects in each set.
                        foreach (Transform tr in pointAndClickSet.transform)
                        {
                            tr.gameObject.GetComponent<ClickableObject>().isInterractable = false;
                        }
                        clickableObjets.Clear();
                        foreach (GameObject nextObj in objectCollections[phase + 1].gameObjectList)
                        {
                            clickableObjets.Add(nextObj.GetComponent<ClickableObject>());
                        }

                        phase++;
                    }
                }
                break;
            case 1:
                if (!SpeechManager.instance.textDisplayed && startPAndClick && !finishedPAndCStep) // !dialogue.Equals("")
                {
                    if (obj.gameObject.activeSelf)
                    {
                        Debug.Log("GO and Destroy : " + obj.gameObject.name);
                        playerMovement.GoAndDestroy(obj);
                    }

                }
                break;
            case 2:
                if (!SpeechManager.instance.textDisplayed && startPAndClick && !finishedPAndCStep) // !dialogue.Equals("")
                {
                    if (obj.gameObject.activeSelf)
                    {
                        playerMovement.GoAndDestroy(obj);
                        //obj.StopBlinking();
                        //obj.gameObject.SetActive(false);
                        //clickableObjets.Remove(obj.GetComponent<ClickableObject>());

                        startPAndClick = false;

                        verre_eau.GetComponent<Animator>().SetBool("renverse", true);

                        Invoke("EndPhase3Minigame", 2F);
                        phase++;
                    }
                }
                break;
        }
    }

    public void Destroy_Object(ClickableObject obj)
    {
        Debug.Log("Destroy_Object : " + obj.gameObject.name);

        obj.StopBlinking();
        clickableObjets.Remove(obj.GetComponent<ClickableObject>());
        obj.gameObject.SetActive(false);
        // If all objects are clicked, end mini game
        if (clickableObjets.Count == 0 && phase == 1)
        {
            phase++;

            SetActivableObjects(phase);
            BlinkAllObjects();
        }

    }

    private void EndPhase3Minigame()
    {
        finishedPAndCStep = true;
    }

    private void BlinkAllObjects()
    {
        foreach (ClickableObject obj in clickableObjets)
        {
            obj.ObjectBlink();
        }
    }

    // NOt used
    private void StopBlinkAllObjects()
    {
        foreach (ClickableObject obj in clickableObjets)
        {
            obj.StopBlinking();
        }
    }
}
