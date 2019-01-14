using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour {

    Vector3 originalCameraPosition;

    float shakeAmt = 0;

    public Camera mainCamera;

    private void Start()
    {
        originalCameraPosition = mainCamera.transform.position; 
    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt; // can also add to x and/or z
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = originalCameraPosition;
    }

    public void Shake(float amp, float frq, float lght)
    {
        shakeAmt = amp * .0025f;
        InvokeRepeating("CameraShake", 0, frq);
        Invoke("StopShaking", lght);
    }
}
