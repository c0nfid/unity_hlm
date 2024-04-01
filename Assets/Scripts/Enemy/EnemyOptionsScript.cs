using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class EnemyOptionsScript : MonoBehaviour
{
    public enum EnemyType
    {
        patrul,
        pursingPlayer,
        goingToLastLoc
    }

    public EnemyType enemyType;

    public enum WeaponType
    {
        hand,
        STICK,
        UZI,
        Gun
    }

    
    public WeaponType weaponType;

    [SerializeField] private int weaponID;

    private SpriteRenderer SR;

    public Sprite[] w_sprites;

    private GameObject player;

    public bool clockwise, moving, guard;

    private Vector3 target;

    private Rigidbody2D rb;

    public Vector3 playerLastPos;

    private RaycastHit2D hit;

    public float speed;

    private int layermask = 1 << 8;

    public Transform check;

    public bool shooting;
    
    //
    public int HP;
    // Start is called before the first frame update
    void Start()
    {
        if (weaponType == WeaponType.hand)
            weaponID = 0;
        if (weaponType == WeaponType.STICK)
            weaponID = 1;
        if (weaponType == WeaponType.UZI)
            weaponID = 2;

        SR = GetComponent<SpriteRenderer>();
        SR.sprite = w_sprites[weaponID];
        player = GameObject.FindGameObjectWithTag("Player");
        playerLastPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        layermask = ~layermask;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        PlayerDetect();
    }

    private void FixedUpdate()
    {
        if (HP == 0)
        {
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        Debug.Log("Logging");
        float dist = Vector3.Distance(player.transform.position, transform.position);
        Vector3 dir = player.transform.position - transform.position;
        hit = Physics2D.Raycast(new Vector2 (transform.position.x, transform.position.y), new Vector2 (dir.x, dir.y), dist, layermask);
        Debug.DrawRay(transform.position, dir, Color.red);
        
        Vector3 fwt = this.transform.TransformDirection (Vector3.right);
        RaycastHit2D hit2 = Physics2D.Raycast (new Vector2 (this.transform.position.x, this.transform.position.y), new Vector2 (fwt.x, fwt.y), 0.5f, layermask);
        Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2 (fwt.x, fwt.y), Color.cyan);
        if (moving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (enemyType == EnemyType.patrul)
        {
            speed = 1f;

            if (hit2.collider != null)
            {
                if (hit2.collider.gameObject.layer == 10)
                {
                    if (!clockwise)
                    {
                        transform.Rotate(0, 0, 90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }
            }
        }

        if (enemyType == EnemyType.pursingPlayer && !player.GetComponent<CharacteMovement>().isDead)
        {
            speed = 1.0f;
            rb.transform.eulerAngles = new Vector3(0, 0,
                Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) *
                Mathf.Rad2Deg);

            if (hit.transform.gameObject.layer == 9)
            {
                playerLastPos = player.transform.position;

                if (weaponType == WeaponType.UZI)
                {
                    if (Vector3.Distance(transform.position, player.transform.position) <= 2f)
                    {
                        moving = false;
                        Shoot(0.5f);
                    }
                    else
                    {
                        if (!guard)
                        {
                            moving = true;
                        }
                    }
                } //else if (weaponType)
            }
        }

        if (enemyType == EnemyType.goingToLastLoc)
        {
            moving = true;
            speed = 1f;
            rb.transform.eulerAngles = new Vector3(0, 0,
                Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) *
                Mathf.Rad2Deg);
            if (Vector3.Distance(transform.position, playerLastPos) < 0.5f)
            {
                enemyType = EnemyType.patrul;
                moving = false;
            }
        }
        
    }

    void PlayerDetect()
    {
        Vector3 pos = transform.InverseTransformPoint(player.transform.position);
        if (hit.collider != null)
        {
            Debug.Log("Coll");
            Debug.Log(pos.x);
            if (hit.transform.gameObject.layer == 9 && pos.x > 1.2f &&
                Vector3.Distance(player.transform.position, transform.position) < 8f)
            {
                Debug.Log("enemy Detect");
                enemyType = EnemyType.pursingPlayer;
            }
            else
            {
                if (enemyType == EnemyType.pursingPlayer)
                    enemyType = EnemyType.goingToLastLoc;
            }
        }
        else
        {
            Debug.Log(hit.collider.gameObject.tag);
        }
    }

    void Shoot(float t)
    {
        if (!shooting)
        {
            StartCoroutine("attackShoot", t);
        }
    }

    IEnumerator attackShoot(float time)
    {
        shooting = true;
        GameObject bullet = Instantiate(Resources.Load("Prefabs/Items/UZI_Bullet"), check.position, check.rotation) as GameObject;
        bullet.GetComponent<Bullet>().isEnemy = true;
        yield return new WaitForSeconds(time);
        shooting = false;
    }
    
    
    
}
