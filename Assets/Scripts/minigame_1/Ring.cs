using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ring : MonoBehaviour {

    Vector3 start_position;

    public float speed = 10;
    public float distance = 3;


    public int Target = 10;

    public SpriteRenderer dirt;

    // Use this for initialization
    void Start () {
        start_position = transform.position;

        last_mouse_position = this.transform.position;
    }

    bool opening = false;
    bool opened = false;  

    public void Opening()
    {
        opening = true; 
    }
	
	// Update is called once per frame
	void Update () {

        if(opening)
        {
            this.transform.Translate(new Vector3(0, 5 * Time.deltaTime));
            if ((start_position - transform.position).sqrMagnitude > distance)
            {
                opening = false;
                opened = true;
                Debug.Log("Opened");
            }

        }
    }

    bool Cleaned = false; 

    Vector2 last_mouse_position; 
    private void OnMouseDrag()
    {
        Vector2 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Debug.Log((curScreenPoint - last_mouse_position).sqrMagnitude);

        if ((curScreenPoint - last_mouse_position).sqrMagnitude > 0.1 && !Cleaned)
        {
            Debug.Log("Drag");
            dirt.color = new Color(dirt.color.r, dirt.color.g, dirt.color.b, dirt.color.a - 0.01f);
            if(dirt.color.a * 255 < 1f)
            {
                Cleaned = true; 
            }
        }
        last_mouse_position = curScreenPoint;
    }
}
