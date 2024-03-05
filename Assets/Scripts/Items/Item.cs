using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public enum WeaponType
    {
        Null,
        UZI,
        STICK
    }
    public WeaponType weaponType;
}
