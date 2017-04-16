using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public string ItemName;
    public ItemType ItemType;
    public string ItemInfo;
    public bool isInHero;
    private Unit Unit;
    private Weapon Weapon;
    public bool equip = false;
    void Start()
    {

    }

    [ContextMenu("Equip")]
    public void Equip()
    {
        switch (ItemType)
        {
            case ItemType.Weapon:
                EqipWeapon();
                break;
            default:
                break;
        }

    }
    [ContextMenu("Uneqip")]
    public void UnEquip()
    {
        switch (ItemType)
        {
            case ItemType.Weapon:
                UnEqipWeapon();
                break;
            default:
                break;
        }
    }

    private void EqipWeapon()
    {
        Weapon = gameObject.GetComponent<Weapon>();
        Unit = gameObject.transform.root.GetComponent<Unit>();
        Unit.weapon = Weapon;
        Unit.DamageType = Weapon.DamageType;
        Unit._Damage = Weapon.DamageValue;
        Unit._AttackRange = Weapon.AttackRange;
        Unit.DefaultAttackSpeed = Weapon.AttackSpeed;
        Unit._AttackSpeed = Weapon.AttackSpeed;
        Unit.agent.stoppingDistance = Weapon.AttackRange;
        Unit.Crit += Weapon.Crit;
        Unit._MinDamage = Weapon.MinimalDamageValue;

        equip = true;
    }
    private void UnEqipWeapon()
    {
        Unit = gameObject.transform.root.GetComponent<Unit>();

        Unit.DamageType = DamageType.Crushing;
        Unit._Damage = Unit.DefaultDamage;
        Unit._AttackRange = Unit.DefaultAttackRange;
        Unit.DefaultAttackSpeed = 2;
        Unit.Crit = Unit.DefaultCrit;
        Unit.agent.stoppingDistance = Unit.DefaultAttackRange;
        Unit._MinDamage = Unit.DefaultMinDamage;

        equip = false;
    }
}
