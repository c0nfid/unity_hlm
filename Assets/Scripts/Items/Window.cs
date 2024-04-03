using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public Sprite brokenWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall" && other.gameObject.GetComponent<Window>() != null)
        {
            BoxCollider2D coll = other.gameObject.GetComponent<BoxCollider2D>();
            other.gameObject.GetComponent<SpriteRenderer>().sprite =
                other.gameObject.GetComponent<Window>().brokenWindow;
            other.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }
    
    
    //

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            breakWindow();
        }
    }

    public void breakWindow()
    {
        BoxCollider2D bc2d = this.GetComponent<BoxCollider2D>();
        bc2d.enabled = false;
        this.GetComponent<SpriteRenderer>().sprite = brokenWindow;
        this.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
}
