 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMonolog : MonoBehaviour {

    [Header("Monologue_Pensee_Alex")]
    [SerializeField]
    private Text nomInterlocuteur;
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
        if (!DialoguesManager.instance.textDisplayed && ClickableObjetManager.instance.startPAndClick && !ClickableObjetManager.instance.finishedPAndCStep)
        {
            StartCoroutine(AnimateTextMonolog(_boiteDialogue, monolog_speed));

        }
    }

    public IEnumerator AnimateTextMonolog(string strComplete, float speed)
    {
        animator.SetBool("openMonolog",true);
        DialoguesManager.instance.textDisplayed = true;
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
        DialoguesManager.instance.textDisplayed = false;
    }


}
