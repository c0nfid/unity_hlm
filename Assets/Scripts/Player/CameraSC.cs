using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSC : MonoBehaviour
{
    Transform player;

    private bool followPlayer;
    Camera cam;

    GameObject pl;

    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacteMovement>().transform;
        followPlayer = false;
        cam = Camera.main;
        pl = GameObject.FindGameObjectWithTag("Player");
        dir = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            followPlayer = true;
            Vector3 camPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            camPos.z = 0;
            Renderer renderer = pl.GetComponent<Renderer>();
            bool isVisible = (MathF.Abs(camPos.x - player.position.x) < 1.5) && (MathF.Abs(camPos.y - player.position.y) < 0.8);
            if ((MathF.Abs(camPos.x - player.position.x) < 1.8) && (MathF.Abs(camPos.y - player.position.y) < 1.1))
            {
                dir = camPos - transform.position;
                dir.z = -10;
                transform.Translate(dir*2*Time.deltaTime);
            }
            else if ((MathF.Abs(camPos.y - player.position.y) >= 1.1) && (MathF.Abs(camPos.x - player.position.x) >= 1.8))
            {
                dir = Vector3.zero;
                transform.Translate(dir*2*Time.deltaTime);
            }
            else if ((MathF.Abs(camPos.x - player.position.x) >= 1.8) && (MathF.Abs(camPos.y - player.position.y) < 1.1))
            {
                dir = new Vector3(0, camPos.y - transform.position.y, 0);
                transform.Translate(dir*2*Time.deltaTime);
            }
            else if ((MathF.Abs(camPos.y - player.position.y) >= 1.1) && (MathF.Abs(camPos.x - player.position.x) < 1.8))
            {
                dir = new Vector3(camPos.x - transform.position.x, 0, 0);
                transform.Translate(dir*2*Time.deltaTime);
            }
            
            
            
        }
        else
        {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
    }
    public static bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
