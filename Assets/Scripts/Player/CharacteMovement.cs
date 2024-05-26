using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteMovement : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public GameObject DeadWindow;
    
    
    Rigidbody2D rb;
    public float speed;
    public Animator anim;

    private Light light;
    public float minIntensity = 0.9f;
    public float maxIntensity = 1.5f;

    public int HP = 100;
    public bool isDead = false;
    float x;
    float y;

    public bool canMove = true;

    Vector3 mousePosition;
    Vector3 direct;

    Camera cam;
    private float timer;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
        light = GameObject.FindGameObjectWithTag("Light").gameObject.GetComponent<Light>();
        HP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();
        changeLight();
    }

    void changeLight()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0.0f;
        }
        else if ((timer <= 0.7f && timer >= 0.1f) | (timer > 0.8f && timer <= 1f))
        {
            light.intensity = Random.Range(minIntensity, maxIntensity);
        }
        
    }
    
    public void /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    FixedUpdate()
    {
        if(canMove){
            MovementManager();
        }
        RotationCharacter();
        if (HP <= 0)
        {
            isDead = true;
            DeadWindow.SetActive(true);
            source.PlayOneShot(clip);
            Time.timeScale = 0f;
            
        }
    }

    void InputManager()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
    }

    void MovementManager()
    {
        anim.SetFloat("move", Mathf.Abs(x) + Mathf.Abs(y));
        rb.velocity = new Vector2(x * speed, y * speed);
    }

    void RotationCharacter()
    {
        mousePosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cam.transform.position.z));
        rb.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg);
    }
}
