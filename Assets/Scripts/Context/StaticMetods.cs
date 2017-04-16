using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMetods : MonoBehaviour {


    public static int GetPower(Unit unit)
    {  
      return (int)(((unit.DefaultHP * (unit.DefaultPrickingDefence + unit.DefaultCrushingDefence + unit.DefaultSlashingDefence + 1)) + ((unit.DefaultAttackSpeed * 10) * unit.DefaultDamage * unit.DefaultAttackRange))) * (unit.DefaultHungry / 100) + (unit.DefaultMoveSpeed * 100);
    }
}
