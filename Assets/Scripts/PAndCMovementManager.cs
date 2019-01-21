using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This script has to be attached in the (walking) collider sprite
// A Polygin Collider 2D must also be attached to detect mouse click
public class PAndCMovementManager : MonoBehaviour {
    [SerializeField]
    private PlayerMovement playerMovement;
    private bool monologueUiCliked;

    // Use this for initialization
    void Start () {
        monologueUiCliked = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMonologueUI()
    {
        playerMovement.Move();
    }

    private void OnMouseDown()
    { 
        if (!GameManager.blockMovementOnGround && !EventSystem.current.IsPointerOverGameObject()) // Do not move when clicking on a UI button
        {
            playerMovement.Move();
        }
    }
}
