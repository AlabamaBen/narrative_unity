 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMonologue : MonoBehaviour {

    [Header("Monologue_Pensee_Alex")]
    [SerializeField]
    private Text boiteDialogue;

    [HideInInspector]
    public Animator animator;
    private string stringToDisplay;

    [Header("SFX sound param")]
    public SFXSound_Voice talk_sound;
    [SerializeField]
    private float text_speed = 0.02f;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void SetMonologPAncC(string _boiteDialogue)
    {
        if (!SpeechManager.instance.textDisplayed && ClickableObjetManager.instance.startPAndClick && !ClickableObjetManager.instance.finishedPAndCStep)
        {
            animator.SetBool("openMonolog", false);
            StartCoroutine(AnimateTextMonolog(_boiteDialogue, text_speed));
        }
    }


    public void SetMonolog(string _boiteDialogue)
    {
        animator.SetBool("openMonolog", false);
        StartCoroutine(AnimateTextMonolog(_boiteDialogue, text_speed));
    }


    public IEnumerator AnimateTextMonolog(string strComplete, float speed)
    {
        animator.SetBool("openMonolog",true);
        SpeechManager.instance.textDisplayed = true;
        int i = 0;
        stringToDisplay = "";
        talk_sound.PlayTheSound();
        while (i < strComplete.Length)
        {
            stringToDisplay += strComplete[i++];
            boiteDialogue.text = stringToDisplay;
            yield return new WaitForSeconds(speed);
        }
        // animator.SetTrigger("closeMonolog"); 
        SpeechManager.instance.textDisplayed = false;
    }
}
