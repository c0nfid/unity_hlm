using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Triggering");
        if (collider.GetComponent<EnemyOptionsScript>())
        {
            Debug.Log("Enemy hit!");
            Destroy(gameObject);
        }
        
        if (collider.tag == "other")
        {
            Debug.Log("other hit!");
            Destroy(gameObject);
        }
    }
}
