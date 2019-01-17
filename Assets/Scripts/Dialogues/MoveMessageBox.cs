using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMessageBox : MonoBehaviour {

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    public Vector3 targetPosition;

    public void Start()
    {
        targetPosition = transform.position;
    }
    public void AnimateTextBox()
    {
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void Update()
    {
        if(transform.position != targetPosition)
            AnimateTextBox();
    }
}
