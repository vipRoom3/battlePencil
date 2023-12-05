using UnityEngine;
using System;

public class BattlePencilAction : MonoBehaviour
{
    public void BattleCalculation(Player myPlayer,Player enemyPlayer, int pencilNum)
    {
        string[] arr =  myPlayer.pencil.ActionList[pencilNum - 1].Split(", ", StringSplitOptions.None);
        if(arr[0] == "attack")
        {
            Debug.Log("attack");
            // 0を下回らないようにする
            enemyPlayer.pencil.setHp(Mathf.Max(enemyPlayer.pencil.Hp - int.Parse(arr[1]), 0));
        }
        else if(arr[0] == "heal")
        {
            Debug.Log("heal");
            // maxHpを超えないようにする
            myPlayer.pencil.setHp(Mathf.Min(myPlayer.pencil.Hp + int.Parse(arr[1]), myPlayer.pencil.MaxHp));
        }
        else if(arr[0] == "miss")
        {
            Debug.Log("miss");
            enemyPlayer.pencil.setHp(enemyPlayer.pencil.Hp - int.Parse(arr[1]));
        }
    }
}
