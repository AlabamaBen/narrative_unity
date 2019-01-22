using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards_Behaviour : MonoBehaviour {

    public Transform target;
    public float smoothFactor = 2;
    public float amplitude = 2;
    public float osc_speed = 2;



    bool moving_up = true; 

    void Update()
    {
        if(moving_up)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * smoothFactor);
            if((transform.position - target.position).sqrMagnitude < 0.01f)
            {
                moving_up = false; 
            }

        }
        else
        {
            transform.position = new Vector3(target.position.x, target.position.y + amplitude * Mathf.Sin(Time.time * osc_speed));
        }
    }

}
