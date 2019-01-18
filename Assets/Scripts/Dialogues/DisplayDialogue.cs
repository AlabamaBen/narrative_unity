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
    public SFXSound_Voice talk_sound;
    [SerializeField]
    private float text_speed = 0.02f;

    private void Awake()
    {
        messagesList = new List<GameObject>();
    }

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }


    IEnumerator AnimateTextDialog(Text textBox, string strComplete, float speed)
    {
        SpeechManager.instance.textDisplayed = true;
        int i = 0;
        stringToDisplay = "";
        while (i < strComplete.Length)
        {
            stringToDisplay += strComplete[i++];
            textBox.text = stringToDisplay;

            talk_sound.PlayTheSound();

            yield return new WaitForSeconds(speed);
        }
        SpeechManager.instance.textDisplayed = false;
    }

    public void SlideDialogue(string interlocuteur, string text)
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
        }
        else if (interlocuteur.Equals("Alex"))
        {
            currentMsg = Instantiate(messageBox_Temp_Alex);
            currentMsg.transform.position = messageBox_Temp_Alex.transform.position;
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
        StartCoroutine(AnimateTextDialog(currentMsg.GetComponentInChildren<Text>(), text, text_speed));

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
}
