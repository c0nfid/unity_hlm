using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    [SerializeField] Transform target;

    private NavMeshAgent agent;
    public Vector3 destination;
    
    
    
    private int layermask = 1 << 8;
    private GameObject player;
    private RaycastHit2D hit;
    private Vector3 playerLastPos;
    private float playerVelocityAngle;
    [SerializeField]
    public bool haveGun = false;

    public bool shoot = false;

    [SerializeField] private bool Patrul = false;
    [SerializeField] private List<Transform> points;
    private int currentPoint = 0;
    
    private EnemyOptionsScript HP_O;
    private Vector3 lastKnownPosition;
    private bool targetVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        destination = target.position;
        HP_O = gameObject.GetComponent<EnemyOptionsScript>();
        playerLastPos = transform.position;
        layermask = ~layermask;
        lastKnownPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        CheckVisibility();
        Vector3 velocity = agent.velocity;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        if (targetVisible && HP_O.HP > 0)
        {
            if (Vector3.Distance(transform.position, target.position) <= 1.5f && haveGun)
            {
                agent.velocity = Vector3.zero;
                agent.SetDestination(transform.position);
                shoot = true;
            }
            else if (Vector3.Distance(transform.position, target.position) <= 0.371f && !haveGun)
            {
                agent.velocity = Vector3.zero;
                agent.SetDestination(transform.position);
                shoot = true;
                Debug.Log("ShootStick");
            }
            else
            {
                Debug.Log("Go to man" + Vector3.Distance(transform.position, lastKnownPosition));
                shoot = false;
                agent.SetDestination(target.position);
                agent.updatePosition = true;
            }
        }
        else
        {
            //Debug.Log(lastKnownPosition != Vector3.zero);
            if (lastKnownPosition != Vector3.zero)
            {
                agent.SetDestination(lastKnownPosition);
                //Debug.Log(Vector3.Distance(transform.position, lastKnownPosition));
            }
            else
            {
                if (Patrul && points.Count > 0)
                {
                    agent.SetDestination(points[currentPoint].position);
                    if (Vector3.Distance(transform.position, points[currentPoint].position) <= 0.5f)
                        currentPoint += 1;

                    if (currentPoint + 1 > points.Count)
                        currentPoint = 0;
                    Debug.Log(currentPoint);
                    Debug.Log("COUNT" + points.Count);
                }
                else
                {
                    
                }
            }
        }

        if (velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            if (Vector3.Distance(transform.position, lastKnownPosition) <= 1f && !shoot && !targetVisible)
            {
                transform.rotation = Quaternion.AngleAxis(playerVelocityAngle, Vector3.forward);
                agent.updatePosition = false;
            }
            else if (shoot && HP_O.HP > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((target.position.y - transform.position.y), (target.position.x - transform.position.x)) * Mathf.Rad2Deg);
            }
        }
        
        // RayTrack();
        // {
        //     if (hit.collider.gameObject.layer == 9)
        //     {
        //         destination = target.position;
        //         Debug.Log("Target");
        //         agent.isStopped = false;
        //         playerLastPos = target.transform.position;
        //         Rigidbody2D rb = target.gameObject.GetComponent<R
        // if (hit.collider != null)igidbody2D>();
        //         playerVelocityAngle = rb.velocity != Vector2.zero
        //             ? Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg
        //             : playerVelocityAngle;
        //     }
        //     else
        //     {
        //         destination = playerLastPos;
        //     }
        // }

        // agent.SetDestination(destination);
        // Vector3 velocity = agent.velocity;
        // float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        // if (velocity != Vector3.zero)
        // {
        //     transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // }
        //
        // if (playerLastPos - transform.position < Vector3.one)
        // {
        //     agent.isStopped = true;
        //     transform.rotation = Quaternion.AngleAxis(playerVelocityAngle, Vector3.forward);
        // }
}

    void RayTrack()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);
        Vector3 dir = target.transform.position - transform.position;
        hit = Physics2D.Raycast(new Vector2 (transform.position.x, transform.position.y), new Vector2 (dir.x, dir.y), dist, layermask);
        Debug.DrawRay(transform.position, dir, Color.red);
    }
    
    
    void CheckVisibility()
    {
        RayTrack();
        Rigidbody2D rb = target.gameObject.GetComponent<Rigidbody2D>();
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 9)
            {
                targetVisible = true;
                lastKnownPosition = target.transform.position;
                playerVelocityAngle = rb.velocity != Vector2.zero
                    ? Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg
                    : playerVelocityAngle;
            }
            else
            {
                targetVisible = false;
            }
        }
        else
        {
            targetVisible = false;
            lastKnownPosition = Vector3.zero;
        }
    }
}
