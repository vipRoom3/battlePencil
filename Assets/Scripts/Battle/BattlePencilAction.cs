using UnityEngine;
using UnityEngine.UI;

public class BattlePencilAction : MonoBehaviour
{


    [SerializeField] private Text MyHPText;
    [SerializeField] private Text EnemyHPText;
    private int MyHP;
    private int EnemyHP;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject[] pencilTag = GameObject.FindGameObjectsWithTag("Pencil");
        MyHP = pencilTag[0].setHp;
        EnemyHP = pencilTag[1].setHp;
        SetHpText();
    }

    void SetHpText()
    {
        MyHPText.text = "HP：" + MyHP.ToString();
        EnemyHPText.text = "HP：" + EnemyHP.ToString();
    }
}
