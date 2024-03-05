using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsWeaponTrigger : MonoBehaviour
{
    public float Damage;
        private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<EnemyOptionsScript>())
        {
            Debug.Log("Enemy hit!");
        }
        
        if (collider.tag == "other")
        {
            Debug.Log("other hit!");
        }
    }
}
