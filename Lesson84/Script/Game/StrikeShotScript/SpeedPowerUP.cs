using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeedPowerUP : BaseStrikeShot
{
    [SerializeField]
    float percentage = 0.2f;

    public override void Activate()
    {
         Action speed = new Action(AddSpeed,actionTime);
         Action power = new Action(AddPower,actionTime);
         LaunchActions.Add(speed);
         LaunchActions.Add(power);
         base.Activate();
    }

    void AddSpeed()
    {
        Monster m = PlayerController.instance.current_monster;

        float speed_bonus = percentage * m.GetMaxSpeed();
        m.AddTempBonus(Stats.Speed, (int)speed_bonus);
    }
    void AddPower()
    {
        Monster m = PlayerController.instance.current_monster;
        float atk_bonus = percentage * m.GetMaxAtk();
        m.AddTempBonus(Stats.Power, (int)atk_bonus);
    }
    
}
