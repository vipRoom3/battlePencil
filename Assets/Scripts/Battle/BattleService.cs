using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class BattleService : MonoBehaviour
{

    private Subject<bool> endGame = new Subject<bool>();
    [SerializeField] private GameObject pencilPrefab;
    [SerializeField] private Text MyHPText;
    [SerializeField] private Text EnemyHPText;
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
    Player player1;
    Player player2;
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
        player1 = new Player(player1Pencil);

        // TODO: プレイヤー2の初期化処理を行う
        player2 = new Player(player2Pencil);

        MyHPText.text = "HP：" + player1.pencil.Hp.ToString();
        EnemyHPText.text = "HP：" + player2.pencil.Hp.ToString();
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

        MyHPText.text = "HP：" + player1.pencil.Hp.ToString();
        EnemyHPText.text = "HP：" + player2.pencil.Hp.ToString();
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
                // 6角形の出目を計算
                int pencil1Result = CalculatePencilResult(pencil1Angle);
                int pencil2Result = CalculatePencilResult(pencil2Angle);
                // 6角形の出目を反転
                int pencil1ReverseResult = ReversePencilResult(pencil1Result);
                int pencil2ReverseResult = ReversePencilResult(pencil2Result);
                // 計算から出た出目をactionに渡す
                BattlePencilAction battlePencilAction = GameObject.Find("GameObject").GetComponent<BattlePencilAction>();
                battlePencilAction.BattleCalculation(player1, player2, pencil1ReverseResult);

                // ペンシルの位置を戻す
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

    float[] angles = new float[] { 0, 60, 120, 180, 240, 300 };
    float tolerance = 30; // 角度の許容範囲
    private int CalculatePencilResult(float angle)
    {
        angle = NormalizeAngle(angle);
        for (int i = 0; i < angles.Length; i++)
        {
            if (angle >= angles[i] - tolerance && angle <= angles[i] + tolerance)
            {
                return i + 1;
            }
        }
        return 0;
    }

    private float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle < 0)
        {
            angle += 360;
        }
        return angle;
    }

    private int ReversePencilResult(int result)
    {
        switch (result)
        {
            case 1:
                return 4;
            case 2:
                return 5;
            case 3:
                return 6;
            case 4:
                return 1;
            case 5:
                return 2;
            case 6:
                return 3;
            default:
                return 0;
        }
    }
}
