using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame2_Behavior : MonoBehaviour {



    public TextMesh CountDown_Display;
    public float CountDown;
    public GameObject bubble_prefabs;
    public Transform topleft, botomright; 


    List<string> words = new List<string>
    {
        "Poire",
        "Pomme", 
        "Kiwi", 
        "Banane", 
        "Mange", 
        "Orange", 
        "Abricot"
    };

    // Use this for initialization
    void Start () {



    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void Pop_Bubble()
    {
        Vector2 pos = new Vector2(Random.Range(topleft.position.x, botomright.position.x), Random.Range(botomright.position.y, topleft.position.y ));
        Instantiate(bubble_prefabs, pos, Quaternion.identity);
    }

    public void Bubble_Hit()
    {

    }

}
