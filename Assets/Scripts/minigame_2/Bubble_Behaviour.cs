using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble_Behaviour : MonoBehaviour {

    public Transform sprite; 

    public TextMesh textmesh;


    public string Text = "Pomme";

    public float speed = 10f;

    Rigidbody2D rb;

    [HideInInspector]
    public bool to_hit; 

    [HideInInspector]
    public Minigame2_Behavior minigame2_Behavior;


    public float maxRotationSpeed = 270 ;
    public float speedmin = 1f;


    public bool debug = false; 

    // Use this for initialization
    void Start () {
        sprite.localScale = new Vector2(sprite.localScale.x * Text.Length, sprite.localScale.y);
        textmesh.text = Text;
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(speed * Random.value , speed * Random.value));
        rb.AddTorque(speed/2 * Random.Range(-1, 1));
    }

    // Update is called once per frame
    void Update () {
        //textmesh.text = Text;
        //sprite.localScale = new Vector2(0.2f * Text.Length, transform.localScale.y);

        if(debug)
        {
            Debug.Log(rb.velocity.SqrMagnitude());
        }

        if(rb.velocity.SqrMagnitude() < speedmin)
        {
            rb.AddForce(new Vector2(speed * Random.value, speed * Random.value));
            rb.AddTorque(speed / 2 * Random.Range(-1, 1));
        }
        if (rb.velocity.SqrMagnitude() > 200f)
        {
            rb.velocity = new Vector2(0, 0);
            rb.angularVelocity = 0f;

            rb.AddForce(new Vector2(speed * Random.value, speed * Random.value));
            rb.AddTorque(speed / 2 * Random.Range(-1, 1));
        }

        if (rb.angularVelocity > maxRotationSpeed)
        {
            rb.angularVelocity = maxRotationSpeed;
        }
        if (rb.angularVelocity < -maxRotationSpeed)
        {
            rb.angularVelocity = -maxRotationSpeed;
        }

        //if(rb.angularVelocity > 45f)
        //{
        //    rb.velocity = new Vector2(0, 0);
        //    rb.angularVelocity = 0f;

        //    rb.AddForce(new Vector2(speed * Random.value, speed * Random.value));
        //    rb.AddTorque(speed / 2 * Random.Range(-1, 1));
        //}
    }

    private void OnMouseDown()
    {
        if(to_hit)
        {
            minigame2_Behavior.Bubble_Hit(this.gameObject); 
        }
    }
}
