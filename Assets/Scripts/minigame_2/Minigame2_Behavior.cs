using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame2_Behavior : MonoBehaviour {


    public TextMesh Tracked_Display;
    public TextMesh CountDown_Display;
    public float CountDown;
    public GameObject bubble_prefabs;
    public Transform topleft, botomright;
    public Sheet_Behaviour sheet_Behaviour;


    List<string> words = new List<string>
    {
        "Akhenaton",
        "Bédouins",
        "Djerouts",
        "Fayoum",
        "Osiris",
        "Maât",
        "Mastaba",
        "Natron",
        "Ouab",
        "Oudjat",
        "Pharaon",
        "Pschent",
        "Scarabée",
        "Vizir",
        "Anubis",
        "Chadouf",
        "Cobra",
        "Ennéade"
    };

    int count = 0;

    public float timer_lenght = 30;

    float timer = 0; 


    // Use this for initialization
    void Start () {

        timer = timer_lenght; 

        int i = Random.Range(0, words.Count - 1);


        //tracked bubble
        Pop_Bubble(words[i], true);
        Tracked_Display.text = words[i];

        //Fake bubbles
        Pop_Bubble(words[(i - 1) % (words.Count - 1)], false);
        Pop_Bubble(words[(i + 1) % (words.Count - 1)], false);


    }

    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;
        CountDown_Display.text = ""+(int)timer; 
		
	}


    private void Pop_Bubble(string name, bool tracked)
    {
        Vector2 pos = new Vector2(Random.Range(topleft.position.x, botomright.position.x), Random.Range(botomright.position.y, topleft.position.y ));
        GameObject bubble = Instantiate(bubble_prefabs, pos, Quaternion.identity);

        bubble.GetComponent<Bubble_Behaviour>().Text = name;
        bubble.GetComponent<Bubble_Behaviour>().to_hit = tracked;
        bubble.GetComponent<Bubble_Behaviour>().minigame2_Behavior = this;


    }


    public void Bubble_Hit(GameObject hited_bubble)
    {

        if(count<5)
        {
            int i = 3 * count; 

            //tracked bubble
            Pop_Bubble(words[i], true);
            Tracked_Display.text = words[i];

            //Fake bubbles
            Pop_Bubble(words[(i + 1)], false);
            Pop_Bubble(words[(i + 2)], false);

            sheet_Behaviour.hit();

            Destroy(hited_bubble);
        }
        else
        {
            int i = 3 * count;

            //No tracked bubble -- Impossible to win -- 
            Tracked_Display.text = words[i];

            //Fake bubbles
            Pop_Bubble(words[(i + 1)], false);
            Pop_Bubble(words[(i + 2)], false);

            sheet_Behaviour.hit();

            Destroy(hited_bubble);
        }


        count++;

    }

}
