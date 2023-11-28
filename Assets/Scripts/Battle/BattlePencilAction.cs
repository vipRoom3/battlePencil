using UnityEngine;
using System;

public class BattlePencilAction : MonoBehaviour
{
    public void BattleCalculation(Player myPlayer,Player enemyPlayer, int pencilNum)
    {
        string[] arr =  myPlayer.pencil.ActionList[pencilNum - 1].Split(", ", StringSplitOptions.None);
        if(arr[0] == "attack")
        {
            enemyPlayer.pencil.setHp(enemyPlayer.pencil.Hp - int.Parse(arr[1]));
        }
        else if(arr[0] == "heal")
        {
            myPlayer.pencil.setHp(myPlayer.pencil.Hp - int.Parse(arr[1]));
        }
        else if(arr[0] == "miss")
        {
            enemyPlayer.pencil.setHp(enemyPlayer.pencil.Hp - int.Parse(arr[1]));
        }
    }
}
