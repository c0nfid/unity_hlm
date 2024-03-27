using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOptionsScript : MonoBehaviour
{
    public int HP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
        //PlayerDetect();
    }
    private void FixedUpdate()
    {
        if (HP == 0)
        {
            Destroy(gameObject);
        }
    }
