using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptionsPanel : MonoBehaviour {

	public List<GameObject> theObjectsToActivate = new List<GameObject>();
	public List<GameObject> theObjectsToDeactivate = new List<GameObject>();

	private void Start () {

	}

	public void OpenOptionsPanel(){
		for(int i=0; i<theObjectsToActivate.Count ; i++){
			theObjectsToActivate [i].SetActive(true);
		}
		for(int i=0; i<theObjectsToDeactivate.Count ; i++){
			if (theObjectsToDeactivate [i] != null && theObjectsToDeactivate [i].activeInHierarchy) {
				theObjectsToDeactivate [i].SetActive (false);
			}
		}
	}

	public void CloseOptionsPanel(){
		for(int i=0; i<theObjectsToActivate.Count ; i++){
			theObjectsToActivate [i].SetActive(false);
		}
		for(int i=0; i<theObjectsToDeactivate.Count ; i++){
			if (theObjectsToDeactivate [i] != null && !theObjectsToDeactivate [i].activeInHierarchy) {
				theObjectsToDeactivate [i].SetActive (true);
			}
		}
	}
}
