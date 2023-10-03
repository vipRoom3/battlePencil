using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class BattleService : MonoBehaviour
{

    private Subject<bool> endGame = new Subject<bool>();

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

        // TODO: プレイヤーの初期化処理を行う
        Player player = new Player();
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
