using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BattleService : MonoBehaviour
{

    private Subject<bool> endGame = new Subject<bool>();
    [SerializeField] private GameObject pencilPrefab;

    public class PencilManager
    {

        [System.Serializable]
        public class JsonPencil
        {
            public int MaxHp;
            public string Name;
            public List<string> ActionList;
        }

        [System.Serializable]
        public class JsonPencils
        {
            public List<JsonPencil> pencils;
        }
        public List<Pencil> All = new List<Pencil>();
        public void Load(GameObject pencilPrefab)
        {
            string json = Resources.Load<TextAsset>("json/pencilList").ToString();
            // jsonをパースする
            JsonPencils jsonPencils = JsonUtility.FromJson<JsonPencils>(json);
            GameObject pencilObj = Instantiate(pencilPrefab);
            foreach (JsonPencil jsonpencil in jsonPencils.pencils)
            {
                Pencil p = new Pencil(
                    jsonpencil.MaxHp,
                    jsonpencil.Name,
                    jsonpencil.ActionList,
                    pencilObj
                );
                All.Add(p);
            }
        }
    }
    public IObservable<bool> IsEndGame
    {
        get { return endGame; }
    }

    public void EndGame()
    {
        // ここでゲーム終了の処理を行う
        bool isEndGame = false;
        // TODO: ゲーム終了の条件を満たしたらtrueにする
        endGame.OnNext(isEndGame);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Service Start");
        // EndGame();

        PencilManager pd = new PencilManager();
        pd.Load(pencilPrefab);


        // プレイヤーの鉛筆を決める
        System.Random r = new System.Random();
        Pencil player1Pencil = pd.All[r.Next(0, pd.All.Count)];

        // TODO: プレイヤーの初期化処理を行う
        Player player1 = new Player(player1Pencil);

        Debug.Log(player1.pencil.Name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // RollPencil
    public void OnClick()
    {
        Debug.Log("OnClick");
        // TODO: ここでペンシルを振る処理を行う

        // TODO: ここでペンシルを振った後の処理を行う
        EndGame();
    }
}
