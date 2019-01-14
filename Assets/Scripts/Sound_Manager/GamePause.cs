using UnityEngine;
using System.Collections;

public class GamePause : MonoBehaviour {

	private void Start(){

	}

	public void PauseGame(){
		Invoke ("PauseTheGame", 0.2f);
	}

	private void PauseTheGame(){
		Time.timeScale = 0f;
	}

	public void UnpauseGame(){
		Time.timeScale = 1f;
	}

	public void SwapState(){
		if (Time.timeScale > 0) {
			PauseGame ();
		}else{
			UnpauseGame ();
		}
	}
}
