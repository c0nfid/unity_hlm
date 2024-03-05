using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item item;

    PlayerWeaponManager pw;
    // Start is called before the first frame update
    void Start()
    {
        pw = FindObjectOfType<PlayerWeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            collider.GetComponent<PlayerWeaponManager>().inTrigger = true;

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("OnTriggerButtonRight");
                StartCoroutine("wait");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            collider.GetComponent<PlayerWeaponManager>().inTrigger = false;
        }
    }

    IEnumerator wait()
    {
        if (pw.currentWeaponType != "Null")
        {
            pw.dropWeapon(pw.currentWeaponType);
        }
        yield return new WaitForSeconds(0.05f);
        Debug.Log(item.weaponType.ToString());
        pw.currentWeaponType = item.weaponType.ToString();
        Destroy(gameObject);
    }
}
