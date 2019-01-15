using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour {

    public static PhoneManager instance = null;
    private Animator animator;
    private static int step = 0;
    private int substep = 0;

    [SerializeField]
    private Image screen;
    public List<Sprite> screenSprites;
    public List<GameObject> buttonToDesactivate;

    public bool phoneGameFinished = false;
    // Use this for initialization
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void StartPhone()
    {
        switch (step)
        {
            case 0:
                bool phoneOpened = animator.GetBool("phoneOpened");

                if (!phoneOpened)
                {
                    StartCoroutine(LaunchAnimationPhone(true, 1f));
                }
                break;
        }
    }

    private IEnumerator LaunchAnimationPhone(bool phoneOpened, float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("phoneOpened", phoneOpened);
    }
    
    public void ClickOnMessageButton()
    {
        switch (substep)
        {
            case 0:
                screen.sprite = screenSprites[substep];
                screen.gameObject.SetActive(true);
                DesactivateAllButtons();
                buttonToDesactivate[1].SetActive(true);
                substep++;
                break;
            case 1:
                screen.sprite = screenSprites[substep];
                DesactivateAllButtons();
                buttonToDesactivate[2].SetActive(true);
                substep++;
                break;
            case 2:
                StartCoroutine(LaunchAnimationPhone(false, 0.75f));
                substep =0;
                DesactivateAllButtons();
                phoneGameFinished = true;
                step++;
                break;
        }
    }

    private void DesactivateAllButtons()
    {
        foreach (GameObject button in buttonToDesactivate)
        {
            button.SetActive(false);
        }
    }
}
