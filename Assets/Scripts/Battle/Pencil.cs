using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil
{
    public string Name { get; }
    // public int MaxHp { get; } = 100;
    // private int Hp { get; set; } = 100;
    // private List<BattlePencilAction> actions = new List<BattlePencilAction>();

    public class JsonData
    {
        public PencilInfo[] pencilInfos;
    }

    [System.Serializable]
    public class PencilInfo
    {
        // TODO: ここにPencilの情報を追加する
        public int MaxHp;
        public string ActionList;
    }

    public Pencil(string name)
    {
        this.Name = name;
        // jsonを読み込む
        string json = Resources.Load<TextAsset>("json/pencilList").text;
        Debug.Log(json);
        // jsonをパースする
        JsonData jsonData = new JsonData();
        JsonUtility.FromJsonOverwrite(json, jsonData);
        Debug.Log(jsonData);
        // TODO: ここでjsonから読み込んだデータをもとにPencilを生成する
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
