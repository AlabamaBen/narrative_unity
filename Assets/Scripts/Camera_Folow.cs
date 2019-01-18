using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera_Folow : MonoBehaviour {

    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    public bool YMaxEnabled = false;
    public float YMaxValue = 0;

    public bool YMinEnabled = false;
    public float YMinValue = 0;

    public bool XMaxEnabled = false;
    public float XMaxValue = 0;

    public bool XMinEnabled = false;
    public float XMinValue = 0;

    void Update()
    {
        Vector3 targetPos = target.position; 

       //vertical 
       if(YMinEnabled && YMaxEnabled)
        {
            targetPos.y = Mathf.Clamp(target.position.y, YMinValue, YMaxValue); 
        }
       else if (YMinEnabled)
        {
            targetPos.y = Mathf.Clamp(target.position.y, YMinValue, target.position.y);
        }
       else if(YMaxEnabled){
            targetPos.y = Mathf.Clamp(target.position.y, target.position.y, YMaxValue);
        }

        //vertical 
        if (XMinEnabled && XMaxEnabled)
        {
            targetPos.x = Mathf.Clamp(target.position.x, XMinValue, XMaxValue);
        }
        else if (XMinEnabled)
        {
            targetPos.x = Mathf.Clamp(target.position.x, XMinValue, target.position.x);
        }
        else if (XMaxEnabled)
        {
            targetPos.x = Mathf.Clamp(target.position.x, target.position.x, XMaxValue);
        }


        targetPos.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);



    }
}
