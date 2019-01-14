using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveLoadManager : MonoBehaviour {
	
	
	//---------------------- Save -----------------------------------------------------------------

	public static void SaveMasterVolumeValue(float value){
		PlayerPrefs.SetFloat("masterVolumeValue", value);
	}

	public static void SaveMusicVolumeValue(float value){
		PlayerPrefs.SetFloat("musicVolumeValue", value);
	}

	public static void SaveSFXVolumeValue(float value){
		PlayerPrefs.SetFloat("SFXVolumeValue", value);
	}

	public static void SaveUiSoundsVolumeValue(float value){
		PlayerPrefs.SetFloat("UiSoundsVolumeValue", value);
	}

	//---------------------- Load -----------------------------------------------------------------

	public static float LoadMasterVolumeValue(){
		if(!PlayerPrefs.HasKey("masterVolumeValue")){
			return 1f;
		}
		return PlayerPrefs.GetFloat("masterVolumeValue");
	}

	public static float LoadMusicVolumeValue(){
		if(!PlayerPrefs.HasKey("musicVolumeValue")){
			return 1f;
		}
		return PlayerPrefs.GetFloat("musicVolumeValue");
	}

	public static float LoadSFXVolumeValue(){
		if(!PlayerPrefs.HasKey("SFXVolumeValue")){
			return 1f;
		}
		return PlayerPrefs.GetFloat("SFXVolumeValue");
	}

	public static float LoadUiSoundsVolumeValue(){
		if(!PlayerPrefs.HasKey("UiSoundsVolumeValue")){
			return 1f;
		}
		return PlayerPrefs.GetFloat("UiSoundsVolumeValue");
	}
}
