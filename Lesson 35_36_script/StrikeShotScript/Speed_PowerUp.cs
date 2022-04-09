using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_PowerUp : MonoBehaviour, StrikeShot
{
    float percentage = 0.2f;
    public void Activate(Transform target = null)
    {
        Monster m = target.GetComponent<Monster>();
        float atk_bonus = percentage * m.GetMaxAtk();
        float speed_bonus = percentage * m.GetMaxSpeed();
        m.temp_power_Bonus.Add((int)atk_bonus);
        m.temp_speed_Bonus.Add(speed_bonus);
    }
}
