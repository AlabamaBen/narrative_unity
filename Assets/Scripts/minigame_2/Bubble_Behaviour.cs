using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble_Behaviour : MonoBehaviour {

    public Transform sprite; 

    public TextMesh textmesh;

    public string Text = "Pomme";

    public float speed = 10f;

    public Sheet_Behaviour sheet_Behaviour; 

    Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
        sprite.localScale = new Vector2(0.2f * Text.Length, transform.localScale.y);
        textmesh.text = Text;
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.AddForce(new Vector2(speed * Random.value , speed * Random.value));
        rigidbody.AddTorque(speed/2 * Random.Range(-1, 1));
    }

    // Update is called once per frame
    void Update () {
        textmesh.text = Text;
        sprite.localScale = new Vector2(0.2f * Text.Length, transform.localScale.y);
    }

    private void OnMouseDown()
    {
        sheet_Behaviour.hit();
        GameObject.Destroy(this.gameObject);
    }
}
