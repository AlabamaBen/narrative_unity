using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class AmbienceSoundsManager : MonoBehaviour {

	public List<AudioClip> AmbienceSoundsList = new List<AudioClip>();

	private void Start(){

	}

	private void OnEnable(){
		SceneManager.sceneLoaded += ManageAudio;
	}

	private void OnDisable(){
		SceneManager.sceneLoaded -= ManageAudio;
	}

	private void SwapSounds(int AmbienceSounds){
		//fade out current+fade in new music
		fadeOut();
		GetComponent<AudioSource>().clip = AmbienceSoundsList[AmbienceSounds];
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
		//if the scene is a level
		if (scene.buildIndex == 0 || scene.buildIndex == 2 || scene.buildIndex == 3) {
			//stop sounds
			StopCoroutine("RandomSoundPick");
			GetComponent<AudioSource> ().Stop ();
		} else {
			StartCoroutine("RandomSoundPick");
		}
	}

	IEnumerator RandomSoundPick() {

		//need to loop and pick new random sound
		SwapSounds (Random.Range(0, AmbienceSoundsList.Count));
		yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);

		StartCoroutine("RandomSoundPick");
	}
}
