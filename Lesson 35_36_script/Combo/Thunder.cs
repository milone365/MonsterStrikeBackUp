using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : Bullet,IDirectDamage
{
    public Entity monster { get ; set ; }
    public Entity enemy { get ; set; }

    public void DestroyGO()
    {
        Destroy(gameObject);
    }

    public void GetDamage()
    {
        if (monster == null || enemy == null) return;
        Helper.Attack(monster, enemy);
    }
}
