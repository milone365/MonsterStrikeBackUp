using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField]
    MonsterData data;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Gacha gacha = FindObjectOfType<Gacha>();
            gacha.Round(10);
        }
    }

}
