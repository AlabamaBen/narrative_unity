using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetSliderValue : MonoBehaviour {

	public enum SliderType{master, music, sfx, uiSounds};
	public SliderType sliderType;
	private AudioVolumeManager audioVolumeManager;

	private void Start () {

	}

	public void SetTheValues(){
		audioVolumeManager = transform.parent.parent.parent.parent.parent.GetComponent<AudioVolumeManager>();
		switch(sliderType){
		case SliderType.master:
			AudioListener.volume = SaveLoadManager.LoadMasterVolumeValue ();
			GetComponent<Slider> ().value = AudioListener.volume;
			break;
		case SliderType.music: 
			audioVolumeManager.updateVolume(1, SaveLoadManager.LoadMusicVolumeValue());
			GetComponent<Slider>().value = SaveLoadManager.LoadMusicVolumeValue();
			break;
		case SliderType.sfx: 
			audioVolumeManager.updateVolume(0, SaveLoadManager.LoadSFXVolumeValue());
			GetComponent<Slider>().value = SaveLoadManager.LoadSFXVolumeValue();
			break;
		case SliderType.uiSounds: 
			audioVolumeManager.updateVolume(2, SaveLoadManager.LoadUiSoundsVolumeValue());
			GetComponent<Slider>().value = SaveLoadManager.LoadUiSoundsVolumeValue();
			break;
		default:;
			break;
		}
	}
}
