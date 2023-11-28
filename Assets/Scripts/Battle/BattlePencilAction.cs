using UnityEngine;

public class BattlePencilAction : MonoBehaviour
{
    public void BattleCalculation(Player myPlayer,Player enemyPlayer, int pencilNum)
    {
        string[] arr =  myPlayer.pencil.ActionList[pencilNum - 1].Split(', ');
        if(arr == "attack")
        {
            enemyPlayer.pencil.Hp = enemyPlayer.pencil.Hp - int.Parse(arr);
        }
        else if(arr == "heal")
        {
            myPlayer.pencil.Hp = myPlayer.pencil.Hp - int.Parse(arr);
        }
        else if(arr == "miss")
        {
            enemyPlayer.pencil.Hp = enemyPlayer.pencil.Hp - int.Parse(arr);
        }
    }
}
