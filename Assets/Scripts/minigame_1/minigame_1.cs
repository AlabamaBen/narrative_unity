using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class minigame_1 : MonoBehaviour {

    Vector3 originalPosition;

    float shakeAmt = 0;

    public Slider progress_bar;

    public int Target = 10;

    public Ring ring; 


    public float amplitude;
    public float length; 

    private void OnMouseDown()
    {
        if (!shakeOn && progress_bar.value < Target)
        {
            Debug.Log("Click");
            Shake(amplitude, length);
            progress_bar.value++;
            if (progress_bar.value == Target)
            {
                Debug.Log("Open");
                GetComponent<Animator>().SetTrigger("Open");
                ring.Opening();
            }
        }
    }

    private void Start()
    {
        originalPosition = transform.position;
        progress_bar.maxValue = Target;
        progress_bar.value = 0; 
    }


    public void Shake(float amp, float lght)
    {
        Invoke("ShakeCameraOn", 0);
        Invoke("ShakeCameraOff", lght);
    }

    // vars
    private bool shakeOn = false;
    private float shakePower = 0;


    // Update is called once per frame
    void Update()
    {

        // if shake is enabled
        if (shakeOn)
        {
            // reset original position
            transform.position = originalPosition;

            // generate random position in a 1 unit circle and add power
            Vector2 ShakePos = Random.insideUnitCircle * shakePower;

            // transform to new position adding the new coordinates
            transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);
        }
    }

    // shake on
    public void ShakeCameraOn()
    {
        //enable shaking and setting power
        shakeOn = true;
        shakePower = amplitude;
    }

    // shake off
    public void ShakeCameraOff()
    {
        // shake off
        shakeOn = false;

        // set original position after 
        transform.position = originalPosition;
    }



}

