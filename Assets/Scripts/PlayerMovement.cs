using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    private bool isMoving;
    public GameObject pointer;
    private Vector3 target;
    private float spriteOffset;

    public Transform top, bottom; 
    public Animator animator;

    Vector2 init_scale;

    public float scale_offset = 0.7f; 

    // Use this for initialization
    void Start () {
        spriteOffset = 0; //this.GetComponent<SpriteRenderer>().size.y * this.transform.localScale.y / 4;
        // spriteOffset = 5f; //this.GetComponent<SpriteRenderer>().size.y/2;
        init_scale = transform.localScale; 
    }

    private bool facing_right = true; 

    void toogle_direction()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
        facing_right = !facing_right;
    }

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

                transform.localScale = new Vector2(-1f * transform.localScale.x, transform.localScale.y);

                //if(facing_right)
                //{
                //    transform.localScale = new Vector2(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y);
                //}
                //else
                //{
                //    transform.localScale = new Vector2(1f * Mathf.Abs(transform.localScale.x), transform.localScale.y);
                //}
            }
            else
            {
                Vector2 direction = (transform.position - target).normalized;

                //Debug.Log("x : " + (transform.position - target).x);

                if ((transform.position - target).x > 0.1f)
                {
                    transform.localScale = new Vector2(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                if ((transform.position - target).x < 0.1f)
                {
                    transform.localScale = new Vector2(1f * Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }

                float angle = Vector2.Angle(Vector2.up, direction);

                //Back
                if (angle < 45f)
                {
                    animator.SetInteger("Direction", 2);
                }
                //Side
                if (angle > 45 && angle < 135)
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

        float positiony =  (top.position.y - transform.position.y) / (top.position.y - bottom.position.y);

        transform.localScale = new Vector2((init_scale.x * Mathf.Sign(transform.localScale.x) * positiony) + scale_offset * Mathf.Sign(transform.localScale.x) , (init_scale.y * positiony) + scale_offset * Mathf.Sign(transform.localScale.y));
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
