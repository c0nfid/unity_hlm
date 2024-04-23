using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField]
    private Sprite[] stickAt, uziAt, legsSpr;

    private Sprite[] gunSpr;
    private int counter = 0, legCount = 0, shootCount = 0;
    
    [SerializeField]
    private GameObject obj;

    private NavMeshAgent EnemyRigid;
    private bool moving = false;
    private float timer = 0.05f, legTimer = 0.05f, shootTimer = 0.05f;
    private EnemyNavMesh shoot;
    private bool wait = false;

    [SerializeField] GameObject legsObj;

    private SpriteRenderer legs;
    private SpriteRenderer tors;
    // Start is called before the first frame update
    void Start()
    {
        EnemyRigid = obj.GetComponent<NavMeshAgent>();
        legs = legsObj.GetComponent<SpriteRenderer>();
        shoot = obj.GetComponent<EnemyNavMesh>();
        tors = obj.GetComponent<SpriteRenderer>();
        gunSpr = shoot.haveGun ? uziAt : stickAt;
        tors.sprite = gunSpr[0];
        wait = false;
    }

    // Update is called once per frame
    void Update()
    {
        moving = (EnemyRigid.velocity != Vector3.zero);
        animateLegs();
        StartCoroutine(animateShoot());
    }

    void animateLegs()
    {
        if (moving)
        {
            legs.sprite = legsSpr[legCount];
            legTimer -= Time.deltaTime;

            if (legTimer <= 0)
            {
                if (legCount < legsSpr.Length - 1)
                {
                    legCount++;
                }
                else
                {
                    legCount = 0;
                }

                legTimer = 0.05f;
            }
        }
        else
            legs.sprite = legsSpr[0];
        
    }

    IEnumerator animateShoot()
    {
        if (shoot.shoot && !wait)
        {
            tors.sprite = gunSpr[shootCount];
            shootTimer -= Time.deltaTime;

            if (shootTimer <= 0)
            {
                if (shootCount < gunSpr.Length - 1)
                {
                    shootCount++;
                }
                else
                {
                    shootCount = 0;
                    wait = true;
                    yield return new WaitForSeconds(shoot.haveGun ? 0.2f : 1f);
                    wait = false;
                }
                shootTimer = 0.05f;
            }
            
        }
        else
        {
            tors.sprite = gunSpr[0];
        }
        
        
    }
}
