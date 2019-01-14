using UnityEngine;
using System.Collections;

public class AudioSourceType : MonoBehaviour {

	public enum SoundGroup {Music, Sfx, UiSounds}
	public SoundGroup soundGroup = SoundGroup.Sfx;
	private AudioSource audioSource;

	private void Awake(){
		audioSource = GetComponent<AudioSource> ();
		
		switch (soundGroup){
		case SoundGroup.Sfx: AudioVolumeManager.AddSourceToArray(audioSource, 0);
			break;
		case SoundGroup.Music: AudioVolumeManager.AddSourceToArray(audioSource, 1);
			break;
		case SoundGroup.UiSounds: AudioVolumeManager.AddSourceToArray(audioSource, 2);
			break;
		}
	}

	private void OnDisable(){
		if(soundGroup == SoundGroup.Sfx){
			AudioVolumeManager.RemoveSourceFromArray(audioSource, 0);
		}else if(soundGroup == SoundGroup.UiSounds){
			AudioVolumeManager.RemoveSourceFromArray(audioSource, 2);
		}
	}

	public void UpdateVolume(float volume) {
		GetComponent<AudioSource>().volume = volume;
	}
}
