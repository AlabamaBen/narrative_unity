using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour {

    public static PhoneManager instance = null;
    private Animator animator;
    private static int step = 0;

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
                    animator.SetBool("phoneOpened",true);
                }
                break;
        }
    }
    
}
