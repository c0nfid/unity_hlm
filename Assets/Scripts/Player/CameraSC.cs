using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSC : MonoBehaviour
{
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacteMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
