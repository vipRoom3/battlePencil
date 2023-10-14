using UniRx;
using UnityEngine;


public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleService battleService;

    void Awake()
    {
        battleService.IsEndGame.Subscribe(isEndGame =>
        {
            Debug.Log(isEndGame);
        });
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Manager Start");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // phaseの管理
}
