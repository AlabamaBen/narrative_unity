using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    private bool isMoving;
    public GameObject pointer;
    private Vector3 target;
    private float spriteOffset;

    public Animator animator;

    // Use this for initialization
    void Start () {
        spriteOffset = 0; //this.GetComponent<SpriteRenderer>().size.y * this.transform.localScale.y / 4;
        // spriteOffset = 5f; //this.GetComponent<SpriteRenderer>().size.y/2;
    }

    private bool facing_right = true; 

	// Update is called once per frame
	void Update () {
        if (isMoving)
        {
            animator.SetBool("Walking", true);
            transform.position = Vector3.MoveTowards(transform.position, target + Vector3.up * spriteOffset, speed * Time.deltaTime);
            if((transform.position - target).sqrMagnitude <0.1f)
            {
                isMoving = false;
                animator.SetBool("Walking", false);
            }

            Vector2 direction = (transform.position - target).normalized;

            //Debug.Log("x : " + (transform.position - target).x);

            if ((transform.position - target).x > 0.1f && facing_right)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
                facing_right = false;
            }
            if ((transform.position - target).x < 0.1f && !facing_right)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
                facing_right = true;
            }

            float angle = Vector2.Angle(Vector2.up, direction); 

            //Back
            if( angle < 45f)
            {
                animator.SetInteger("Direction", 2);
            }
            //Side
            if ( angle > 45 && angle < 135)
            {
                animator.SetInteger("Direction", 0);
            }
            //Front
            if (angle > 135)
            {
                animator.SetInteger("Direction", 1);
            }

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
