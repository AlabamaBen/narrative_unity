 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMonologue : MonoBehaviour {

    [Header("Monologue_Pensee_Alex")]
    [SerializeField]
    private Text boiteDialogue;
    [SerializeField]
    private float monolog_speed = 0.02f;

    [HideInInspector]
    public Animator animator;
    private string stringToDisplay;
    public SFXSound_Voice talk_sound;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void SetMonolog(string _boiteDialogue)
    {
        animator.SetBool("openMonolog", false);
        if (!SpeechManager.instance.textDisplayed && ClickableObjetManager.instance.startPAndClick && !ClickableObjetManager.instance.finishedPAndCStep)
        {
            StartCoroutine(AnimateTextMonolog(_boiteDialogue, monolog_speed));

        }
    }

    public IEnumerator AnimateTextMonolog(string strComplete, float speed)
    {
        animator.SetBool("openMonolog",true);
        SpeechManager.instance.textDisplayed = true;
        int i = 0;
        stringToDisplay = "";
        while (i < strComplete.Length)
        {
            stringToDisplay += strComplete[i++];
            boiteDialogue.text = stringToDisplay;
            talk_sound.PlayTheSound();
            yield return new WaitForSeconds(speed);
        }
        // animator.SetTrigger("closeMonolog");
        SpeechManager.instance.textDisplayed = false;
    }


}
