using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    // Pencilを持つ
    // playerが持ってるペンシルの名前？からペンシルを生成する
    public Pencil pencil;

    public Player(Pencil pencil)
    {
        this.pencil = pencil;
        // Debug.Log("Player Start");
        // Debug.Log(this.pencil.Name);
        // Pencilを生成する
        // Pencil pencil = new Pencil("test");
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
