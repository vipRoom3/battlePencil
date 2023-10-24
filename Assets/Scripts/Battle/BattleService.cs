using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BattleService : MonoBehaviour
{

    private Subject<bool> endGame = new Subject<bool>();
    [SerializeField] private GameObject pencilPrefab;

    private const string PENCIL_LIST_PATH = "json/pencilList";

    public class PencilListManager
    {

        [Serializable]
        public class JsonPencil
        {
            public int MaxHp;
            public string Name;
            public List<string> ActionList;
        }

        [Serializable]
        public class JsonPencils
        {
            public List<JsonPencil> pencils;
        }
        private List<JsonPencil> allJsonPencils;
        public void Load()
        {
            string json = Resources.Load<TextAsset>(PENCIL_LIST_PATH).ToString();
            // jsonをパースする
            JsonPencils jsonPencils = JsonUtility.FromJson<JsonPencils>(json);
            allJsonPencils = jsonPencils.pencils;
        }

        public Pencil CreatePencil(GameObject pencilPrefab)
        {
            System.Random r = new System.Random();
            JsonPencil newJsonPencil = allJsonPencils[r.Next(0, allJsonPencils.Count)];

            GameObject pencilObj = Instantiate(pencilPrefab);

            Pencil newPencil = new Pencil(
                newJsonPencil.MaxHp,
                newJsonPencil.Name,
                newJsonPencil.ActionList,
                pencilObj
            );

            // pencilObjをPencilNumManagerに渡す
            PencilNumManager pencilNumManager = GameObject.Find("Plane").GetComponent<PencilNumManager>();
            pencilNumManager.SetRigidbody(pencilObj.GetComponent<Rigidbody>());
            return newPencil;
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

        PencilListManager pencilListManager = new PencilListManager();
        pencilListManager.Load();

        // プレイヤーの鉛筆を決める
        Pencil player1Pencil = pencilListManager.CreatePencil(pencilPrefab);

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
