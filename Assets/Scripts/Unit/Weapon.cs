using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public DamageType DamageType;
    public int DamageValue;
    public int MinimalDamageValue;
    public int AttackRange;
    public int AttackSpeed;
    public int Crit;

    public List<WeaponEffect> effects = new List<WeaponEffect>();

    [System.Serializable]
    public class WeaponEffect
    {
        public string id;
        public int chacnce;
        public int power;
    }
}
