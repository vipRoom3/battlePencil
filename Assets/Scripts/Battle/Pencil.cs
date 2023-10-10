using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil
{
    public int MaxHp { get; }
    public int Hp { get; private set; }
    public string Name { get; }
    public List<string> ActionList { get; }

    public Pencil(int maxHp, string name, List<string> actionList) {
        this.MaxHp = maxHp;
        this.Name = name;
        this.ActionList = new List<string>(actionList);
    }

}
