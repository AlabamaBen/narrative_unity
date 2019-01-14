using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    private bool isMoving;
    public GameObject pointer;
    private Vector3 target;
    private float spriteOffset;
      

    // Use this for initialization
    void Start () {
        spriteOffset = 1.5f; //this.GetComponent<SpriteRenderer>().size.y/2;
    }
	
	// Update is called once per frame
	void Update () {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target + Vector3.up * spriteOffset, speed * Time.deltaTime);
        }
	}

    public void Move()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;

        if (!isMoving)
        {
            isMoving = true;
        }
        pointer.transform.position = target;
    }
}
