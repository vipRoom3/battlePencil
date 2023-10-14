using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilNumManager : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private void OnTriggerStay(Collider col)
    {
        if (_rb.IsSleeping())
        {
            switch (col.gameObject.name)
            {
                case "1":
                    Debug.Log("6");
                    break;
                case "2":
                    Debug.Log("5");
                    break;
                case "3":
                    Debug.Log("4");
                    break;
                case "4":
                    Debug.Log("3");
                    break;
                case "5":
                    Debug.Log("2");
                    break;
                case "6":
                    Debug.Log("1");
                    break;
            }
        }
    }
}
