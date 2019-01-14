using UnityEngine;

public class SFXSound : MonoBehaviour {

	public AudioClip theSound;

	private void Start () {

	}

	public void PlayTheSound(){
		GetComponent<AudioSource>().clip = theSound;
		GetComponent<AudioSource>().Play();
	}
}
