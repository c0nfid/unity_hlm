using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public string currentWeaponType;
    public bool inTrigger = false, shoot;

    public Transform bullet_Spawn;
    // Start is called before the first frame update
    void Start()
    {
        currentWeaponType = "Null";
    }

    // Update is called once per frame
    void Update()
    {
        WeaponManager();
        AttackManager(GetComponent<PlayerAnimationController>().weaponID);
    }

    void WeaponManager()
    {
        if(Input.GetMouseButtonDown(1) && !inTrigger)
        {
            dropWeapon(currentWeaponType);
        }
    }
    public void dropWeapon(string weaponType)
    {   
        Debug.Log(weaponType);
        if(currentWeaponType != "Null")
        {
            Instantiate(Resources.Load("Prefabs/Items/" + weaponType), transform.position, Quaternion.identity);
            
            if (!inTrigger)
                currentWeaponType = "Null";
        }
        else
        {
            Debug.Log("Weapon is null");
        }
    }

    void AttackManager(int ID)
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (ID)
            {
                case 0:
                    break;
                case 1:
                    if (shoot)
                    {
                        Debug.Log("wwww1");
                        StartCoroutine("shootingUZI", 0.2f);
                    }
                    break;
                case 2:
                    if (shoot)
                    {
                        Debug.Log("wwww2");
                        break; //fsdfsd
                    }
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator shooting(float time)
    {
        Instantiate(Resources.Load("Prefabs/Items/" + currentWeaponType + "_Bullet"), bullet_Spawn.position, bullet_Spawn.rotation);
        shoot = false;

        yield return new WaitForSeconds(time);
        shoot = true;
    }
    IEnumerator shootingUZI(float time)
    {
        Instantiate(Resources.Load("Prefabs/Items/" + currentWeaponType + "_Bullet"), bullet_Spawn.position, bullet_Spawn.rotation);
        
        yield return new WaitForSeconds((float)0.1);
        Instantiate(Resources.Load("Prefabs/Items/" + currentWeaponType + "_Bullet"), bullet_Spawn.position, bullet_Spawn.rotation);
        yield return new WaitForSeconds((float)0.1);
        Instantiate(Resources.Load("Prefabs/Items/" + currentWeaponType + "_Bullet"), bullet_Spawn.position, bullet_Spawn.rotation);
        shoot = false;

        yield return new WaitForSeconds(time);
        shoot = true;
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

