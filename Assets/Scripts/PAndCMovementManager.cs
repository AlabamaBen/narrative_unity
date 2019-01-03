using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script has to be attached in the (walking) collider sprite
// A Polygin Collider 2D must also be attached to detect mouse click
public class PAndCMovementManager : MonoBehaviour {
    [SerializeField]
    private PlayerMovement playerMovement;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseUp()
    {
    }

    private void OnMouseDown()
    {
        playerMovement.Move();
    }
}
