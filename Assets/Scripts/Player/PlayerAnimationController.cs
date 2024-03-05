using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator anim;

    PlayerWeaponManager pw;

    public int weaponID = 0;

    bool shoot;

    void Start()
    {
        anim = GetComponent<Animator>();
        pw = GetComponent<PlayerWeaponManager>();
        pw.shoot = true;
    }

    void Update()
    {
        WeaponAnimation(pw.currentWeaponType);
        anim.SetInteger("weapons", weaponID);
        AttackAnimation();
    }

    void WeaponAnimation(string weapon)
    {
        switch (weapon)
        {
            case "Null":
                weaponID = 0;
                break;
            case "UZI":
                weaponID = 1;
                break;
            case "STICK":
                weaponID = 2;
                break;
            default:
                break;
        }
    }

    void AttackAnimation()
    {
        shoot = pw.shoot;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button left" + weaponID.ToString() + " " + shoot.ToString());
            switch (weaponID)
            {
                case 0:
                    anim.SetTrigger("attack");
                    break;
                case 1:
                    if (shoot)
                    {
                        StartCoroutine("shooting", 0.2f);
                    }
                    break;
                case 2:
                    if (shoot)
                    {
                        StartCoroutine("shooting", 1f);
                    }
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator shooting(float time)
    {
        anim.SetTrigger("attack");
        shoot = false;

        yield return new WaitForSeconds(time);
        shoot = true;
    }
}