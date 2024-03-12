using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public Animator anim;
    
    float x;
    float y;

    public bool canMove = true;

    Vector3 mousePosition;
    Vector3 direct;

    Camera cam;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();
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
