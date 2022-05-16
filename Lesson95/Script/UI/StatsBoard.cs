using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsBoard : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    GameObject power = null;
    [SerializeField]
    GameObject speed = null, defence = null, combo = null;
    [Header("Bonus/Malus")]
    [SerializeField]
    GameObject UP = null;
    [SerializeField]
    GameObject Down = null;

    public void Show(bool value,Stats s,bool bonus=true)
    {
        switch (s)
        {
            case Stats.Power:
                power.SetActive(value);
                break;
            case Stats.Speed:
                speed.SetActive(value);
                break;
            case Stats.Defence:
                defence.SetActive(value);
                break;
            case Stats.Combo:
                combo.SetActive(value);
                break;
        }
        if(bonus)
        {
            UP.SetActive(value);
            Down.SetActive(false);
        }
        else
        {
            Down.SetActive(value);
            UP.SetActive(false);
        }

    }
}
