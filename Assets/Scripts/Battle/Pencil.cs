using System.Collections.Generic;
using UnityEngine;

public class Pencil
{
    public int MaxHp { get; }
    public int Hp { get; private set; }
    public string Name { get; }
    public List<string> ActionList { get; }

    public GameObject Obj;



    public Pencil(int maxHp, string name, List<string> actionList, GameObject obj)
    {
        MaxHp = maxHp;
        Name = name;
        ActionList = new List<string>(actionList);
        Obj = obj;
    }

}
