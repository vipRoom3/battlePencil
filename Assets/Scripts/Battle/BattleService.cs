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

    public bool EndGame()
    {
        bool isEndGame = true;
        endGame.OnNext(isEndGame);
        return false;
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
