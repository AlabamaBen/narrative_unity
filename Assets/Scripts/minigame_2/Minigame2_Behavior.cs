﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Minigame2_Behavior : MonoBehaviour {


    public TextMeshPro Tracked_Display;
    public TextMesh CountDown_Display;
    public float CountDown;
    public GameObject bubble_prefabs;
    public Transform topleft, botomright;
    public Sheet_Behaviour sheet_Behaviour;
    public Djin_Intervention djin_Intervention;
    public Animation fade;
    public SFXSound SFX_Alarm;
    public AudioSource SFX_Tiktok;


    public static bool MINIGAME2_END = false; 

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
        "Ennéade",
        "Atoum",
        "Tefnout",
        "Osiris",
        "Anubis",
        "Bastet",
        "Khnoum",
        "Sobek",
        "JilayJôn",
        "Mout"
    };

    List<string> intervention = new List<string>
    {
        "Eh bah ça a l'ai compliqué ça. T'as l'air de bien galérer",
        "Tu sais je peux t'aider à écrire ça, c'est un sujet que je connais bien en plus...",
        "Aller tu vois bien que t'y arrivera pas, laisse moi faire.",
        "Ah il te reste pas beaucoup de temps, ça devient compliqué, pense un peu à ton amie"
    };

    int count = 0;

    public float timer_lenght = 30;

    float timer = 0; 


    // Use this for initialization
    void Start () {

        timer = timer_lenght; 

        //tracked bubble
        Pop_Bubble(words[count], true);
        Tracked_Display.text = words[count];

        //Fake bubbles
        Pop_Bubble(words[(count + 1) % (words.Count - 1)], false);
        Pop_Bubble(words[(count + 2) % (words.Count - 1)], false);

        count++;

        Invoke("Open_Intervention", 2); 

    }

    void Close_Intervention()
    {
        djin_Intervention.Disappear();
        Invoke("Open_Intervention", 4);
    }

    int nbr = 0; 

    void Open_Intervention()
    {
        djin_Intervention.transform.position = new Vector2(djin_Intervention.transform.position.x, Random.Range(botomright.position.y, topleft.position.y));
        djin_Intervention.Appear();
        djin_Intervention.Display_Text(intervention[nbr % (intervention.Count - 1)]);
        Invoke("Close_Intervention", 5);
        nbr++;
    }

    
    // Update is called once per frame
    void Update () {

        if (timer < 0f)
        {
            if(!fade.isPlaying)
            {
                fade.Play();
                SFX_Alarm.PlayTheSound();
                SFX_Tiktok.Stop();


                Invoke("End_Game", 5); 
            }
        }
        else
        {
            timer -= Time.deltaTime;
            CountDown_Display.text = "" + (int)timer;
        }
    }
     
    void End_Game()
    {
        MINIGAME2_END = true;
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

        if(count<7)
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
