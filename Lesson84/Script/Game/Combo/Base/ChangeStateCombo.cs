using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateCombo : Combo
{
    [SerializeField]
    Status status = new Status();

    public override void Activate()
    {
        base.Activate();
        target= PlayerController.instance.CurrentMonsterT();
        Monster m = target.GetComponent<Monster>();
        m.AddTempBonus(status.stat,status.amount,status.turn);
        if(status.stat==Stats.Speed)
        {
            m.ChangeSpeed(status.amount);
        }
    }

    public override void INIT(Entity owner)
    {
        base.INIT(owner);
    }

}
