using UnityEngine;
using System.Collections;

public class WipePlayerPrefs : MonoBehaviour {


	private void Start () {
		PlayerPrefs.DeleteAll();
	}

	public void WipeTheValues () {
		PlayerPrefs.DeleteAll();
	}
}
