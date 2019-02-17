using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UB;

public enum Ring_State
{
    in_the_box,
    getting_out,
    is_out,
    cleaned,
    opened,
    SmokeFadeOut,
    End
}

public class Ring : MonoBehaviour
{

    Vector3 start_position;

    public float speed = 10;
    public float distance = 3;


    public int Target = 10;

    public SpriteRenderer dirt;

    public SFXSound SFX_Wipe;
    public SFXSound SFX_Cleaned;
    public SFXSound SFX_Steam;


    public GameObject cat;

    public ParticleSystem fire;

    Ring_State state;


    public Get_Smokes get_Smokes;
    public float fog_speed;

    [SerializeField]
    private Image tutoImage;
    private Text textTutoImage;

    // Use this for initialization
    void Start()
    {
        start_position = transform.position;

        last_mouse_position = this.transform.position;

        state = Ring_State.in_the_box;
        textTutoImage = tutoImage.GetComponentInChildren<Text>();
    }

    public void Opening()
    {

        state = Ring_State.getting_out;
    }

    public float time = 0;
    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case Ring_State.in_the_box:
                textTutoImage.text = "Cliquer sur la boite";
                break;
            case Ring_State.getting_out:
                this.transform.Translate(new Vector3(0, 5 * Time.deltaTime));
                if ((start_position - transform.position).sqrMagnitude > distance)
                {
                    state = Ring_State.is_out;
                }
                break;
            case Ring_State.is_out:
                textTutoImage.text = "Maintenir clic gauche sur la bague et bougez la souris";
                break;
            case Ring_State.cleaned:
                tutoImage.gameObject.SetActive(false);
                //Invoke("open", 3f); 
                time += Time.deltaTime;
                Glow.color = new Color(Glow.color.r, Glow.color.g, Glow.color.b, (Mathf.Cos(time * glow_speed / Mathf.PI) + 1f) / 2f);
                break;
            case Ring_State.opened:
                textTutoImage.text = "";
                foreach (D2FogsPE fog in get_Smokes.Fogs)
                {
                    fog.enabled = true;
                    fog.Density += fog_speed * Time.deltaTime;
                    if (fog.Density > 3f)
                    {
                        state = Ring_State.SmokeFadeOut;
                        cat.SetActive(true);
                    }
                }
                break;
            case Ring_State.SmokeFadeOut:

                fire.Stop();
                foreach (D2FogsPE fog in get_Smokes.Fogs)
                {
                    fog.Density -= fog_speed * Time.deltaTime;
                    if (fog.Density < 0f)
                    {
                        state = Ring_State.End;
                    }
                }

                break;
            case Ring_State.End:
                Game_End = true;
                break;


            default:
                break;
        }


    }
    public Sprite Opened_sprite;
    public SpriteRenderer Glow;
    public float glow_speed = 10f;

    private void OnMouseDown()
    {
        if (state == Ring_State.cleaned)
        {
            GetComponent<SpriteRenderer>().sprite = Opened_sprite;
            Glow.enabled = false;
            state = Ring_State.opened;

            SFX_Steam.PlayTheSound();

            fire.Play();

            foreach (D2FogsPE fog in get_Smokes.Fogs)
            {
                fog.enabled = true;
                fog.Density = 0;
            }

        }
    }

    [HideInInspector]
    public static bool Game_End = false;

    //float sound_wip_time = 0; 

    Vector2 last_mouse_position;
    private void OnMouseDrag()
    {
        Vector2 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if ((curScreenPoint - last_mouse_position).sqrMagnitude > 0.1 && state == Ring_State.is_out)
        {

            dirt.color = new Color(dirt.color.r, dirt.color.g, dirt.color.b, dirt.color.a - 0.01f);


            //Debug.Log((int)(dirt.color.a * 255));
            if ((int)(dirt.color.a * 255) % 11 == 0)
            {
                SFX_Wipe.PlayTheSound();
            }

            if (dirt.color.a * 255 < 1f)
            {
                state = Ring_State.cleaned;
                SFX_Cleaned.PlayTheSound();
            }
        }
        last_mouse_position = curScreenPoint;
    }

}
