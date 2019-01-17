using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UB;

public class Get_Smokes : MonoBehaviour {

    public D2FogsPE[] Fogs; 

	// Use this for initialization
	void Start () {
        Fogs = GetComponents<D2FogsPE>();
		
	}
}
