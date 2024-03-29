using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private EnemyOptionsScript attacked;
    [SerializeField] private float speed;

    [SerializeField] private int damage;

    public bool isEnemy = false;
    // Start is called before the first frame update

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isEnemy && other.gameObject.tag == "Enemy")
        {
            attacked = other.gameObject.GetComponent<EnemyOptionsScript>();
            attacked.HP -= damage;
            Debug.Log("Enemy");
            Destroy(gameObject);
        } else if (isEnemy && other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacteMovement>().HP -= damage;
            Debug.Log("Player");
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "other")
        {
            Debug.Log("other hit!");
            Destroy(gameObject);
        }
        
    }
    // private void OnTriggerStay2D(Collider2D collider)
    // {
    //     Debug.Log("Triggering");
    //     if (collider.tag == "Enemy")
    //     {
    //         Debug.Log("Enemy hit!");
    //         Destroy(gameObject);
    //     }
    //     
    //     if (collider.tag == "other")
    //     {
    //         Debug.Log("other hit!");
    //         Destroy(gameObject);
    //     }
    // }
}
