using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationScript : MonoBehaviour
{
    Rigidbody2D rb;

    private float mod = 0.1f;

    private float zVal = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = FindObjectOfType<CharacteMovement>().GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != Vector2.zero)
        {
            Vector3 ret = new Vector3(0, 0, zVal);
            this.transform.eulerAngles = ret;

            zVal += mod;

            if (transform.eulerAngles.z >= 5.0f && transform.eulerAngles.z < 7.0f)
            {
                mod = -0.02f;
            }
            else if (transform.eulerAngles.z < 352.0f && transform.eulerAngles.z > 350.0f)
            {
                mod = 0.02f;
            }
        }
        // else
        // {
        //     this.transform.eulerAngles = Vector3.zero;
        // }
}
}
