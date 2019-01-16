using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheet_Behaviour : MonoBehaviour {

    public float line_size = 0.5f;

    public Transform mask;

    public int score; 

	// Use this for initialization
	void Start () {
        score = 0; 
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hit(); 
        }
	}

    public void hit()
    {
        mask.Translate(new Vector2(0, - line_size));
    }
}
