using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour {

    public GameObject stats;

    //UnitStats
    public Text name;
    public Text level;
    public Text HP;
    public Text Damage;
    public Text AttackSpeed;
    public Text AttackRange;
    public Text PDef;
    public Text SDef;
    public Text CDef;
    public Text MoveSpeed;


    void Update()
    {
        if (Context.Instance().SelectionManager.selectunit != null)
        {
            stats.SetActive(true);
            ShowUnitStats();
        }
        else
        {
            stats.SetActive(false);
        }
    }

    void ShowUnitStats()
    {
        Unit unit = Context.Instance().SelectionManager.selectunit;
        name.text = unit.DefaultUnitName;
        level.text = unit.Level.ToString();
        HP.text = unit._HP.ToString();
        Damage.text = unit._Damage.ToString();
        AttackSpeed.text = unit.DefaultAttackSpeed.ToString();
        AttackRange.text = unit._AttackRange.ToString();
        PDef.text = unit._PrickingDefence.ToString() + " %";
        SDef.text = unit._SlashingDefence.ToString() + " %";
        CDef.text = unit._CrushingDefence.ToString() + " %";
        MoveSpeed.text = unit._MoveSpeed.ToString();
    }
}
