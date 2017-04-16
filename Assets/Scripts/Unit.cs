using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using System.IO;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

    public DamageType DamageType;
    public Faction Faction;
    public NavMeshAgent agent;
    public List<Effect> effects = new List<Effect>();
    public Weapon weapon;

    //Default
    public int DefaultUnitId;
    public string DefaultUnitClass;
    public string DefaultUnitName;
    public int DefaultHP;
    public int DefaultHungry;
    public int DefaultDamage;
    public int DefaultMoveSpeed;
    public double DefaultPrickingDefence;
    public double DefaultSlashingDefence;
    public double DefaultCrushingDefence;
    public int Level;
    public double DefaultAttackSpeed;
    public int DefaultAttackRange;
    public int DefaultCrit;
    public int DefaultMinDamage;

    //Modifay
    public int _HP;
    public int _Hungry;
    public int _Damage;
    public int _MoveSpeed;
    public double _PrickingDefence;
    public double _SlashingDefence;
    public double _CrushingDefence;
    public double _AttackSpeed;
    public int _AttackRange;
    public int _MinDamage;
    public int Crit;
    public int Power;

    bool autoattack = false;
    bool isTarget = false;
    bool NavigationIsUnit = false;
    public Unit target;
    public float targetdist;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        DamageType = DamageType.Crushing;
        JsonData loadeddata = DataLoad("DefaultUnitStats");
        DefaultUnitClass = loadeddata["units"][DefaultUnitId]["UnitClass"].ToString();
        DefaultHP = (int)loadeddata["units"][DefaultUnitId]["HP"];
        DefaultHungry = (int)loadeddata["units"][DefaultUnitId]["Hungry"];
        DefaultDamage = (int)loadeddata["units"][DefaultUnitId]["Damage"];
        DefaultMoveSpeed = (int)loadeddata["units"][DefaultUnitId]["MoveSpeed"];
        DefaultPrickingDefence = (double)loadeddata["units"][DefaultUnitId]["PrickingDefence"];
        DefaultSlashingDefence = (double)loadeddata["units"][DefaultUnitId]["SlashingDefence"];
        DefaultCrushingDefence = (double)loadeddata["units"][DefaultUnitId]["CrushingDefence"];
        Level = (int)loadeddata["units"][DefaultUnitId]["Level"];
        DefaultAttackSpeed = (double)loadeddata["units"][DefaultUnitId]["AttackSpeed"];
        DefaultAttackRange = (int)loadeddata["units"][DefaultUnitId]["AttackRange"];
        DefaultCrit = (int)loadeddata["units"][DefaultUnitId]["Crit"];
        DefaultMinDamage = (int)loadeddata["units"][DefaultUnitId]["MinDamage"];
        // Debug.Log("Unit " + loadeddata["units"][DefaultUnitId]["UnitClass"] + " " + "Create!");

        Invoke("AddUnitToList", 0.5f);

        _HP = DefaultHP;
        _Hungry = DefaultHungry;
        _Damage = DefaultDamage;
        _MoveSpeed = DefaultMoveSpeed;
        _PrickingDefence = DefaultPrickingDefence;
        _SlashingDefence = DefaultSlashingDefence;
        _CrushingDefence = DefaultCrushingDefence;
        _AttackSpeed = 0;
        _AttackRange = DefaultAttackRange;
        Crit = DefaultCrit;
        _MinDamage = DefaultMinDamage;

        agent.speed = _MoveSpeed;

        InvokeRepeating("CheckEffects", 0 , 1f);
    }

    void Update()
    {
        Power = StaticMetods.GetPower(this);

        ReloadAutoattack();
        AttackTarget();
    }

    [ContextMenu("TestEffect")]
    public void TestEffect()
    {
        GameObject effectobj = Instantiate(Resources.Load("UnitEffects/TestEffect")) as GameObject;
        effectobj.transform.SetParent(transform);
        Effect script = effectobj.GetComponent<Effect>();
        script.effecttime = 5;
        effects.Add(script);

        GameObject effectobj2 = Instantiate(Resources.Load("UnitEffects/TestEffect1")) as GameObject;
        effectobj2.transform.SetParent(transform);
        Effect script2 = effectobj2.GetComponent<Effect>();
        script2.effecttime = 10;
        effects.Add(script2);
    }

    private void CheckEffects()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].effecttime <= 0)
            {
                GameObject obj = effects[i].gameObject;
                effects.RemoveAt(i);
                Destroy(obj);
                i--;
            }
            else
            {
                effects[i].EffectUse();
            }
        }
    }

    private static JsonData DataLoad(string filename)
    {
        string jsonstring = File.ReadAllText(Application.dataPath + "/StreamingAssets" + "/" + filename + ".json");
        return JsonMapper.ToObject(jsonstring);
    }

    public void UnitMoveTo(Vector3 position)
    {
        NavigationIsUnit = false;
        target = null;
        agent.destination = position;

        agent.stoppingDistance = 2;
    }
    public void UnitMoveToUnit(GameObject unit)
    {
        target = unit.GetComponent<Unit>();
        NavigationIsUnit = true;

        agent.stoppingDistance = _AttackRange;
    }
    public void AddUnitToList()
    {

        if (Faction == Faction.Player)
        {
            Context.Instance().allplayerunits.Add(this);
        }
    }

    [ContextMenu("ShowStats")]
    public void ShowStats()
    {
        Debug.Log("HP: " + _HP);
        Debug.Log("Hungry: " + _Hungry);
        Debug.Log("Damage: " + _Damage);
        Debug.Log("MoveSpeed: " + _MoveSpeed);
        Debug.Log("PrickingDefence: " + _PrickingDefence);
        Debug.Log("SlashingDefence: " + _SlashingDefence);
        Debug.Log("CrushingDefence: " + _CrushingDefence);
        Debug.Log("Level: " + Level);
        Debug.Log("AttackSpeed: " + _AttackSpeed);
        Debug.Log("AttackRange: " + _AttackRange);
        Debug.Log("Power: " + Power);
    }

    public void SetDamage(DamageType type, int damage, int Crit, int mindamage, Weapon weapon = null)
    {
        int finaldamage = Random.Range(mindamage, damage);
        double _damage;
        int critchance = Random.Range(Crit, 100);
        string damagetype;
        double absorbeddamage;

        if (weapon != null)
        {
            if (weapon.effects.Count > 0)
            {
                foreach (Weapon.WeaponEffect eff in weapon.effects)
                {
                    int rand = Random.Range(eff.chacnce, 100);
                    if (rand > 95)
                    {
                        GameObject effectobj = Instantiate(Resources.Load("UnitEffects/" + eff.id)) as GameObject;
                        effectobj.transform.SetParent(transform);
                        Effect script = effectobj.GetComponent<Effect>();
                        script.effecttime = eff.power;
                        effects.Add(script);
                    }
                }
            }
        }

        if (critchance > 95)
        {
            finaldamage = finaldamage * 2;
            Debug.Log("Critical Hit!");
        } 
        switch (type)
        {
            case DamageType.Pricking:
                absorbeddamage = finaldamage * (_PrickingDefence / 100);
                _damage = finaldamage - absorbeddamage;
                _HP -= (int)_damage;
                damagetype = "Pricking";
                break;
            case DamageType.Slashing:
                absorbeddamage = finaldamage * (_SlashingDefence / 100);
                _damage = finaldamage - absorbeddamage;
                _HP -= (int)_damage;
                damagetype = "Slashing";
                break;
            case DamageType.Crushing:
                absorbeddamage = finaldamage * (_CrushingDefence / 100);
                _damage = finaldamage - absorbeddamage;
                _HP -= (int)_damage;
                damagetype = "Crushing";
                break;
            default:
                absorbeddamage = 0;
                _damage = damage;
                _HP -= damage;
                damagetype = "Magick";
                break;
        }

        //Debug.Log(this.gameObject.name + " (Say): " + " Im damaged for: " + _damage + " My HP is: " + _HP + " DamageType = " + damagetype + " Damage absorbed: " + absorbeddamage);

        if (_HP <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        switch (Faction)
        {
            case Faction.Player:
                if (Context.Instance().allplayerunits.Contains(this))
                {
                    Context.Instance().allplayerunits.Remove(this);
                }
                if (Context.Instance().SelectionManager.selectedunits.Contains(this))
                {
                    Context.Instance().SelectionManager.selectedunits.Remove(this);
                }
                break;
            default:
                break;
        }

        Destroy(this.gameObject);
    }

    void ReloadAutoattack()
    {
        if (_AttackSpeed <= 0)
        {
            _AttackSpeed = 0;
            autoattack = true;
            return;
        }

        if (_AttackSpeed > 0)
        {
            autoattack = false;
            _AttackSpeed -= Time.deltaTime;          
        } 
    }

    void AttackTarget()
    {
        if (target == null)
        {
            isTarget = false;
            return;
        }

        if (autoattack)
        {
            if (Vector3.Distance(this.gameObject.transform.position, target.gameObject.transform.position) < _AttackRange + 1)
            {
                target.SetDamage(DamageType, _Damage, Crit, _MinDamage, weapon);
                _AttackSpeed = DefaultAttackSpeed;
            }

        }

        targetdist = Vector3.Distance(this.gameObject.transform.position, target.gameObject.transform.position);
    }

    void NavigationToUnit()
    {
        if (!NavigationIsUnit) return;
        if (target == null)
        {
            NavigationIsUnit = false;
            return;
        }
        agent.destination = target.transform.position;
    }

    [ContextMenu("EqipAll")]
    public void EqipAll()
    {
        List<Item> items = new List<Item>();

        items.AddRange(transform.GetComponentsInChildren<Item>());

        foreach (Item item in items)
        {
           if (!item.equip) item.Equip();
        }    
    }

    [ContextMenu("UnEqipAll")]
    public void UnEqipAll()
    {
        List<Item> items = new List<Item>();

        items.AddRange(transform.GetComponentsInChildren<Item>());

        foreach (Item item in items)
        {
            if (item.equip) item.UnEquip();
        }
    }


}
