using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponManager : MonoBehaviour
{
    public string currentWeaponType;
    public enum WeaponType
    {
        STICK,
        UZI,
        Gun
    }

    
    public WeaponType weaponType;

    private EnemyOptionsScript HP_O;
    [SerializeField] private int weaponID;
    public Transform bullet_Spawn;
    
    private EnemyNavMesh shoot;

    private bool waitShoot = false;
    // Start is called before the first frame update
    void Start()
    {
        currentWeaponType = weaponType.ToString();
        HP_O = gameObject.GetComponent<EnemyOptionsScript>();
        if (weaponType == WeaponType.STICK)
            weaponID = 0;
        if (weaponType == WeaponType.UZI)
            weaponID = 1;
        if (weaponType == WeaponType.Gun)
            weaponID = 2;
        shoot = GetComponent<EnemyNavMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        AttackManager(weaponID);
    }

    void AttackManager(int ID)
    {
        switch (ID)
        {
            case 0:
                if (shoot.shoot && HP_O.HP > 0)
                {
                    Debug.Log("Bite");
                    if (!waitShoot) HandedTrigger();
                }
                break;
            case 1:
                if (shoot.shoot && HP_O.HP > 0)
                {
                    Debug.Log("wwww1");
                    if (!waitShoot) StartCoroutine("shooting", 0.2f);
                }
                break;
            case 2:
                if (shoot.shoot && HP_O.HP > 0)
                {
                    Debug.Log("wwww2");
                    break; //fsdfsd
                }
                break;
            default:
                break;
        }
    }
    IEnumerator shooting(float time)
    {
        Instantiate(Resources.Load("Prefabs/Items/" + currentWeaponType + "_Bullet"), bullet_Spawn.position, bullet_Spawn.rotation);
        waitShoot = true;
        
        yield return new WaitForSeconds(time);
        waitShoot = false;
    }
    IEnumerator shootingUZI(float time)
    {
        Instantiate(Resources.Load("Prefabs/Items/" + currentWeaponType + "_Bullet"), bullet_Spawn.position, bullet_Spawn.rotation);
        
        yield return new WaitForSeconds((float)0.1);
        Instantiate(Resources.Load("Prefabs/Items/" + currentWeaponType + "_Bullet"), bullet_Spawn.position, bullet_Spawn.rotation);
        yield return new WaitForSeconds((float)0.1);
        Instantiate(Resources.Load("Prefabs/Items/" + currentWeaponType + "_Bullet"), bullet_Spawn.position, bullet_Spawn.rotation);
        waitShoot = true;
        
        yield return new WaitForSeconds(time);
        waitShoot = false;
    }


    public void HandedTrigger()
    {
        StartCoroutine("waitHanded", 0.2f);
    }

    IEnumerator waitHanded(float time)
    {
        bullet_Spawn.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(time);
        bullet_Spawn.GetComponent<BoxCollider2D>().enabled = false;
    }
}
