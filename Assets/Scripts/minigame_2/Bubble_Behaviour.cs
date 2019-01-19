using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble_Behaviour : MonoBehaviour {

    public Transform sprite; 

    public TextMesh textmesh;

    public string Text = "Pomme";

    public float speed = 10f;

    public Sheet_Behaviour sheet_Behaviour; 

    Rigidbody2D rb;

    [HideInInspector]
    public bool to_hit; 

    [HideInInspector]
    public Minigame2_Behavior minigame2_Behavior;


	// Use this for initialization
	void Start () {
        sprite.localScale = new Vector2(0.2f * Text.Length, transform.localScale.y);
        textmesh.text = Text;
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(speed * Random.value , speed * Random.value));
        rb.AddTorque(speed/2 * Random.Range(-1, 1));
    }

    // Update is called once per frame
    void Update () {
        textmesh.text = Text;
        sprite.localScale = new Vector2(0.2f * Text.Length, transform.localScale.y);

        if(rb.velocity.SqrMagnitude() < 2f)
        {
            rb.AddForce(new Vector2(speed * Random.value, speed * Random.value));
            rb.AddTorque(speed / 2 * Random.Range(-1, 1));
        }
        if (rb.velocity.SqrMagnitude() > 200f)
        {
            rb.velocity = new Vector2(0, 0);
            rb.rotation = 0f;

            rb.AddForce(new Vector2(speed * Random.value, speed * Random.value));
            rb.AddTorque(speed / 2 * Random.Range(-1, 1));
        }
    }

    private void OnMouseDown()
    {
        if(to_hit)
        {
            minigame2_Behavior.Bubble_Hit(); 
        }
    }
}
