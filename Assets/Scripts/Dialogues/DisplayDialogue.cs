using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDialogue : MonoBehaviour {  
    public GameObject dialogue_Alex_Nat;
    public GameObject messageBox_Temp_Natyahs;
    public GameObject messageBox_Temp_Alex;
    public GameObject message_List_Panel;
    public List<GameObject> messagesList; 
    private string stringToDisplay; // dynamic string that is displayed
    public Animator animator;
    [Header("SFX sound param")]
    public SFXSound_Voice talk_sound_Alex;
    public SFXSound_Voice talk_sound_Natyahs;
    [SerializeField]
    private float text_speed = 0.02f;

    [Header("Dialogue param")]
    public Image alex;
    public Image natyahs;
    public List<SpriteData> allSprites;

    private void Awake()
    {
        messagesList = new List<GameObject>();
    }

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }


    IEnumerator AnimateTextDialog(Text textBox, string strComplete, float speed, string interlocuteur)
    {
        SpeechManager.instance.textDisplayed = true;
        int i = 0;
        stringToDisplay = "";
        while (i < strComplete.Length)
        {
            stringToDisplay += strComplete[i++];
            textBox.text = stringToDisplay;

            if (interlocuteur.Equals("Natyahs"))
            {
                talk_sound_Natyahs.PlayTheSound();
            }
            else if (interlocuteur.Equals("Alex"))
            {
                talk_sound_Alex.PlayTheSound();
            }
            else
            {
                Debug.Log("Error in character name");
            }


            yield return new WaitForSeconds(speed);
        }
        SpeechManager.instance.textDisplayed = false;
    }

    public void SlideDialogue(string interlocuteur, string text, string spriteName)
    {
        foreach (GameObject msg in messagesList)
        {
            msg.GetComponent<MoveMessageBox>().targetPosition = msg.transform.position + Vector3.up * 100;
        }

        GameObject currentMsg = null;
        if (interlocuteur.Equals("Natyahs"))
        {
            currentMsg = Instantiate(messageBox_Temp_Natyahs);
            currentMsg.transform.position = messageBox_Temp_Natyahs.transform.position;

            ReplaceSprite(spriteName, natyahs);
        }
        else if (interlocuteur.Equals("Alex"))
        {
            currentMsg = Instantiate(messageBox_Temp_Alex);
            currentMsg.transform.position = messageBox_Temp_Alex.transform.position;

            ReplaceSprite(spriteName, alex);
        }
        else
        {
            Debug.Log("Error in character name");
            return;
        }

        currentMsg.transform.SetParent(message_List_Panel.transform);
        currentMsg.transform.localScale = Vector3.one;
        currentMsg.SetActive(true);
        messagesList.Add(currentMsg);
        StartCoroutine(AnimateTextDialog(currentMsg.GetComponentInChildren<Text>(), text, text_speed, interlocuteur));

        if (messagesList.Count > 5)
        {
            GameObject msgToDestroy = messagesList[0];
            messagesList.Remove(msgToDestroy);
            Destroy(msgToDestroy);
        }
    }


    public void DestroyAllDialogues()
    {
        StartCoroutine(DestroyLastDialog(0.35F));
    }


    IEnumerator DestroyLastDialog(float speed)
    {
        while (messagesList.Count>0)
        {
            GameObject temp = messagesList[0];
            messagesList.RemoveAt(0);
            Destroy(temp);
            yield return new WaitForSeconds(speed);
        }
    }

    private void ReplaceSprite(string _name,Image spriteToReplace)
    {
        Debug.Log("spr._name : " + _name);
        foreach (SpriteData spr in allSprites)
        {
            if (spr.img_name != "" && spr.img_name.Equals(_name))
            {
                Debug.Log("oui");
                spriteToReplace.sprite = spr.sprite;
            }
        }
    }

}
