using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    bool activate = false;
    [SerializeField]
    PickUpType type = PickUpType.hearth;
    [SerializeField]
    float amount = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activate) return;
        if(collision.tag==Helper.MONSTER)
        {
            if(PlayerController.instance.IS_RunningMonster(collision.transform))
            {
                activate = true;
                Monster m = collision.GetComponent<Monster>();
                if(m!=null)
                {
                    Activation(m);
                }
                Destroy(gameObject);
            }
        }
    }

    void Activation(Monster m)
    {
        switch (type)
        {
            case PickUpType.hearth:
                m.getHealthManager().Healing(amount);
                break;
            case PickUpType.guide:
                m.HaveGuide = true;
                break;
            case PickUpType.sword:
                //power no 20 %
                m.AddTempBonus(Stats.Power,(int)amount);
                break;
            case PickUpType.shield:
                m.AddTempBonus(Stats.Defence,(int)amount);
                break;
            case PickUpType.boot:
                m.board.Show(true, Stats.Speed);
                m.AddTempBonus(Stats.Speed,0);
                m.ChangeSpeed(amount);
                break;
        }
    }
}
