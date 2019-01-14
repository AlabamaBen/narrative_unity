using UnityEngine;

public class UISound : MonoBehaviour {

	public AudioClip theSound;

	private void Start () {
		
	}
	
	public void PlayTheSound(){
		GameObject.FindGameObjectWithTag("UISounds").GetComponent<AudioSource>().clip = theSound;
		GameObject.FindGameObjectWithTag("UISounds").GetComponent<AudioSource>().Play();
	}
}
