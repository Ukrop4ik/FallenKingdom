using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class JsonMobsCreator : MonoBehaviour {



    [ContextMenu("Create")]
    public void Test()
    {
        JsonData data;

        UnitData unitA = new UnitData("Test1", 1000, 100, 50, 2, 5, 1, 1, 1);
        UnitData unitB = new UnitData("Test2", 500, 100, 25, 3, 5, 1, 1, 5);

        List<UnitData> ud = new List<UnitData>();

        ud.Add(unitA);
        ud.Add(unitB);

        Data s = new Data(ud);

        data = JsonMapper.ToJson(s);

        File.WriteAllText(Application.dataPath + "/StreamingAssets" + "/" + "TEST" + ".json", data.ToString());
    }

    [ContextMenu("Load")]
    public void Test2()
    {
        JsonData loadeddata = DataLoad("TEST");
        Debug.Log(loadeddata["units"][0]["HP"]);
    }

    private static JsonData DataLoad(string filename)
    {
        string jsonstring = File.ReadAllText(Application.dataPath + "/StreamingAssets" + "/" + filename + ".json");
        return JsonMapper.ToObject(jsonstring);
    }

    public class Data
    {
        public List<UnitData> units;

        public Data(List<UnitData> a)
        {
            units = a;
        }
    }
    public class UnitData
    {
        public string UnitName;
        public int HP;
        public int Hungry;
        public int Damage;
        public int MoveSpeed;
        public int Defence;
        public int Level;
        public int AttackSpeed;
        public int AttackRange;

        public UnitData(string s, int a, int b, int c, int d, int e, int f, int g, int r)
        {
            UnitName = s;
            HP = a;
            Hungry = b;
            Damage = c;
            MoveSpeed = d;
            Defence = e;
            Level = f;
            AttackSpeed = g;
            AttackRange = r;
        }
 
    }
}
