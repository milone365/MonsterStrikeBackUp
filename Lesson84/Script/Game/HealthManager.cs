using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    protected int hp;
    public int maxHp = 600;
    bool sharedHP = true;
    bool is_dead = false;

    public void INIT(Monster m,int level)
    {
        maxHp = m.get_data().hp * level;
        hp = maxHp;
    }

    public void TakeDamage(int dmg)
    {
        if (is_dead) return;
        if(sharedHP)
        {
            PlayerController.instance.TakeDamage(dmg);
        }
        else
        {
            hp -= dmg;
            if(hp<=0)
            {
                hp = 0;
                is_dead = true;
            }
        }
    }
    
    public void Healing(float h)
    {
        if(sharedHP)
        {
            PlayerController.instance.Healing((int)h);
        }
        else
        {

            hp += (int)h;
            //check
            hp = Mathf.Clamp(hp, 0, maxHp);
        }
    }
}
