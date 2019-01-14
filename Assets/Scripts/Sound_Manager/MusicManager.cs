using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour {

	public List<AudioClip> musicList;

	private void Start(){

	}

	private void OnEnable(){
		SceneManager.sceneLoaded += ManageAudio;
	}

	private void OnDisable(){
		SceneManager.sceneLoaded -= ManageAudio;
	}

	public void SwapMusic(int music){
		//fade out current+fade in new music
		fadeOut();
		GetComponent<AudioSource>().clip = musicList[music];
		GetComponent<AudioSource>().Play();
		fadeIn();
	}


	private void fadeIn() {
		if ( GetComponent<AudioSource>().volume < 1f) {
			GetComponent<AudioSource>().volume += 0.1f * Time.deltaTime;
		}
	}
	
	private void fadeOut() {
		if(GetComponent<AudioSource>().volume > 0.1f){
			GetComponent<AudioSource>().volume -= 0.1f * Time.deltaTime;
		}
	}

	private void ManageAudio(Scene scene, LoadSceneMode mode){
		//main menu
		if(scene.buildIndex == 0){
			SwapMusic (0);
		}
		//level selection
		else if(scene.buildIndex == 1){
			SwapMusic (1);
		}
		//Demo_environement
		else if(scene.buildIndex == 2){
			SwapMusic (2);
		}
		//quizz
		else if(scene.buildIndex == 3){
			SwapMusic (3);
		}
		//Lvl01
		else if(scene.buildIndex == 4){
			SwapMusic (4);
		}
		//Tutorial
		else if(scene.buildIndex == 5){
			SwapMusic (5);
		}
	}
}
