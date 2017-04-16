using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {

    public List<Unit> selectedunits = new List<Unit>();
    public Unit selectunit;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse 0");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, 1 << 8))
            {
               // Debug.Log("Hit to unit");
                Unit unit = hit.collider.gameObject.GetComponent<Unit>();
                selectunit = unit;
                if (unit.Faction == Faction.Player)
                {
                    selectedunits.Clear();
                    selectedunits.Add(unit);
                }         
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Mouse 1");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, 1 << 9))
            {
              //  Debug.Log("Hit to ground");
                if (selectedunits.Count < 1) return;
                Context.Instance().NavigationManager.UnitMoveTo(selectedunits, hit.point);
            }
            if (Physics.Raycast(ray, out hit, 1000f, 1 << 8))
            {
               // Debug.Log("Hit to unit");
                Unit unit = hit.collider.gameObject.GetComponent<Unit>();
                if (selectedunits.Count < 1) return;
                if (unit.Faction == Faction.Player) return;

                Context.Instance().NavigationManager.UnitMoveToUnit(selectedunits, hit.collider.gameObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            selectedunits.Clear();
            selectedunits.AddRange(Context.Instance().allplayerunits);
        }

    }
}
