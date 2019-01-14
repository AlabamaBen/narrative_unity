using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AudioVolumeManager : MonoBehaviour {

	static List<AudioSource>[] audioSources = new List<AudioSource>[3];
	public Slider MasterSlider;
	public Slider MusicSlider;
	public Slider SFXSlider;
	public Slider UiSoundsSlider;

	private void Start () {
		//using invoke to be sure the sources have been added to the array before
		Invoke("ValuesAfterDelay", 0.2f);
	}

	private void ValuesAfterDelay(){
		MasterSlider.GetComponent<SetSliderValue> ().SetTheValues ();
		MusicSlider.GetComponent<SetSliderValue> ().SetTheValues ();
		SFXSlider.GetComponent<SetSliderValue> ().SetTheValues ();
		UiSoundsSlider.GetComponent<SetSliderValue> ().SetTheValues ();
	}

	public static void AddSourceToArray(AudioSource audioSource, int volumeGroup){
		if (audioSources [volumeGroup] == null){
			audioSources [volumeGroup] = new List<AudioSource>();
		}
		audioSources[volumeGroup].Add(audioSource);
	}
	
	public static void RemoveSourceFromArray(AudioSource audioSource, int volumeGroup){
		if (audioSources [volumeGroup] != null){
			audioSources[volumeGroup].Remove(audioSource);
		}
	}

	public void updateVolume(int volumeGroup, float volume){
		//Send the new volume level to the audio sources
		if(audioSources != null && audioSources[volumeGroup] != null && audioSources[volumeGroup].Count > 0){
			for (int i = 0; i < audioSources[volumeGroup].Count; i++){
				if(audioSources[volumeGroup][i] != null && audioSources[volumeGroup][i].gameObject !=null){
					GameObject go = audioSources[volumeGroup][i].gameObject;
					go.BroadcastMessage("UpdateVolume", volume, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	public void OnMasterSliderChange(){
		AudioListener.volume = MasterSlider.value;
		SaveLoadManager.SaveMasterVolumeValue(MasterSlider.value);
	}

	public void OnMusicSliderChange(){
		updateVolume (1, MusicSlider.value);
		SaveLoadManager.SaveMusicVolumeValue(MusicSlider.value);
	}

	public void OnSFXSliderChange(){
		updateVolume (0, SFXSlider.value);
		SaveLoadManager.SaveSFXVolumeValue(SFXSlider.value);
	}

	public void OnUiSoundsSliderChange(){
		updateVolume (0, UiSoundsSlider.value);
		SaveLoadManager.SaveUiSoundsVolumeValue(UiSoundsSlider.value);
	}

	private void OnApplicationQuit(){
		SaveLoadManager.SaveMasterVolumeValue(MasterSlider.value);
		SaveLoadManager.SaveMusicVolumeValue(MusicSlider.value);
		SaveLoadManager.SaveSFXVolumeValue(SFXSlider.value);
		SaveLoadManager.SaveUiSoundsVolumeValue(UiSoundsSlider.value);
	}

	private void OnApplicationFocus(bool hasFocus) {
		if (!hasFocus) {
			SaveLoadManager.SaveMasterVolumeValue(MasterSlider.value);
			SaveLoadManager.SaveMusicVolumeValue(MusicSlider.value);
			SaveLoadManager.SaveSFXVolumeValue(SFXSlider.value);
			SaveLoadManager.SaveUiSoundsVolumeValue(UiSoundsSlider.value);
		}
	}

	private void OnApplicationPause(bool pauseStatus) {
		if (pauseStatus) {
			SaveLoadManager.SaveMasterVolumeValue(MasterSlider.value);
			SaveLoadManager.SaveMusicVolumeValue(MusicSlider.value);
			SaveLoadManager.SaveSFXVolumeValue(SFXSlider.value);
			SaveLoadManager.SaveUiSoundsVolumeValue(UiSoundsSlider.value);
		}
	}
}
