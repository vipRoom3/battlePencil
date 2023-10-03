using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleManager : MonoBehaviour
{
    private BattleService battleService;
    // Start is called before the first frame update
    void Start()
    {
        // endgameをsubscribeする
        battleService.IsEndGame.Subscribe(endGame =>
        {
            // ここにゲーム終了時の処理を書く
            Debug.Log(endGame);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}

