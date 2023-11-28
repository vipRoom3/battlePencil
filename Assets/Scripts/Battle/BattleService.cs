using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BattleService : MonoBehaviour
{

    private Subject<bool> endGame = new Subject<bool>();
    [SerializeField] private GameObject pencilPrefab;

    private const string PENCIL_LIST_PATH = "json/pencilList";

    private bool isGameStarted = false;

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

        public Pencil CreatePencil(GameObject pencilPrefab, float pencilPosVector, int playerNum)
        {
            System.Random r = new System.Random();
            JsonPencil newJsonPencil = allJsonPencils[r.Next(0, allJsonPencils.Count)];
            //ペンシル生成
            GameObject pencilObj = Instantiate(pencilPrefab);
            //タグを付与
            pencilObj.tag = "Pencil";
            //ポジション変更
            pencilObj.transform.position = new Vector3(pencilPosVector, 25.0f, 0.0f);
            Pencil newPencil = new Pencil(
                newJsonPencil.MaxHp,
                newJsonPencil.Name,
                newJsonPencil.ActionList,
                pencilObj
            );

            // pencilObjをPencilNumManagerに渡す
            PencilNumManager pencilNumManager = GameObject.Find("Plane").GetComponent<PencilNumManager>();
            //PencilNumManagerにGameObjectをセット
            pencilNumManager.SetRigidbody(pencilObj.GetComponent<Rigidbody>(), playerNum);
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
        float pencilPosPlayer1 = 12.5f;
        // プレイヤーの鉛筆を決める
        Pencil player1Pencil = pencilListManager.CreatePencil(pencilPrefab, pencilPosPlayer1, 1);
        float pencilPosPlayer2 = -12.5f;
        // プレイヤー2の鉛筆を決める
        Pencil player2Pencil = pencilListManager.CreatePencil(pencilPrefab, pencilPosPlayer2, 2);

        // TODO: プレイヤーの初期化処理を行う
        Player player1 = new Player(player1Pencil);

        // TODO: プレイヤー2の初期化処理を行う
        Player player2 = new Player(player2Pencil);

        Debug.Log(player1.pencil.Name);
        Debug.Log(player2.pencil.Name);
    }

    private float pencil1StoppedTime = 0f;
    private float pencil2StoppedTime = 0f;
    private const float STOPPED_THRESHOLD = 2f;

    void FixedUpdate()
    {
        if (!isGameStarted)
        {
            return;
        }
        // pencilを探す
        GameObject[] pencilTag = GameObject.FindGameObjectsWithTag("Pencil");
        //  両方のペンシルが停止しているかチェック
        bool[] pencilStopped = new bool[2];
        int i = 0;
        foreach (GameObject pentag in pencilTag)
        {
            Rigidbody rb = pentag.GetComponent<Rigidbody>();
            if (rb.IsSleeping())
            {
                pencilStopped[i] = true;
            }
            else
            {
                pencilStopped[i] = false;
            }
            i++;
        }
        // 両方のペンシルが2秒間停止していたら
        if (pencilStopped[0] && pencilStopped[1])
        {
            pencil1StoppedTime += Time.deltaTime;
            pencil2StoppedTime += Time.deltaTime;
            if (pencil1StoppedTime > STOPPED_THRESHOLD && pencil2StoppedTime > STOPPED_THRESHOLD)
            {
                Debug.Log("両方のペンシルが停止している");
                // ペンシルの角度を取得
                float pencil1Angle = pencilTag[0].transform.rotation.eulerAngles.z;
                float pencil2Angle = pencilTag[1].transform.rotation.eulerAngles.z;
                Debug.Log(pencil1Angle);
                Debug.Log(pencil2Angle);
            }
        }
        else
        {
            pencil1StoppedTime = 0f;
            pencil2StoppedTime = 0f;
        }
    }

    // RollPencil
    public void OnClick()
    {
        Debug.Log("OnClick");
        // TODO: ここでペンシルを振る処理を行う
        GameObject[] pencilTag = GameObject.FindGameObjectsWithTag("Pencil");
        var rand = new System.Random();
        //力の加える向き
        float addDirection = 1f;
        //力を加える位置
        Vector3 powerPoint = new Vector3(0.0f, 1.0f, 0.0f);

        //200f~850f
        float forceXnum = 200.0f;
        foreach (GameObject pentag in pencilTag)
        {
            Rigidbody rb = pentag.GetComponent<Rigidbody>();
            rb.useGravity = true;
            Vector3 penPos = pentag.transform.position;
            //偶数、奇数で力の加える方向変更
            addDirection = addDirection * -1.0f;
            forceXnum = rand.Next(minValue: 200, maxValue: 850) * addDirection;
            rb.AddForceAtPosition(new Vector3(forceXnum, 1.0f, 0.0f), penPos + powerPoint);
            // Vector3 force = new Vector3 (forceXnum,0.0f,0.0f);    // 力を設定
            // rb.AddForce (force);  // 力を加える
        }
        GameObject rollButton = GameObject.FindGameObjectWithTag("Roll");
        rollButton.SetActive(false);
        isGameStarted = true;
        // TODO: ここでペンシルを振った後の処理を行う
        // EndGame();
    }
}
