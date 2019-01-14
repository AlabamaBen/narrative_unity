using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjetManager : MonoBehaviour {

    public List<GameObject> pointAndClickSet;
    public static int phase = 0;

    private List<ClickableObject> clickableObjets;

	// Use this for initialization
	void Start () {
        foreach (GameObject obj in pointAndClickSet)
        {
            obj.SetActive(false);
        }
        pointAndClickSet[0].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
