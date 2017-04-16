using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour {


    public void UnitMoveTo(List<Unit> units, Vector3 position)
    {

        foreach (Unit unit in units)
        {
          //  Debug.Log("Unit: " + unit.DefaultUnitId + " " + "command move to: " + position);
            unit.UnitMoveTo(position);
        }
    }

    public void UnitMoveToUnit(List<Unit> units, GameObject targetunit)
    {

        foreach (Unit unit in units)
        {
           // Debug.Log("Unit: " + unit.DefaultUnitId + " " + "command move to unit : " + targetunit);
            unit.UnitMoveToUnit(targetunit);
        }
    }
}

