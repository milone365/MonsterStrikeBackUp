using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnCombo : ComboBase
{

    [SerializeField]
    Bullet bullet = null;
    [SerializeField]
    Transform[] spawnPoints = null;
    int bulletAmount = 1;


    public override void Activate()
    {
        canActive = false;
        for(int i=0;i<bulletAmount;i++)
        {
            Bullet newBullet = Instantiate(bullet, spawnPoints[i].position,spawnPoints[i].rotation) as Bullet;
            Vector2 direction = spawnPoints[i].localPosition;
            newBullet.INIT(direction,this as ComboBase);
        }
    }


    public override void INIT(Entity owner)
    {
        base.INIT(owner);
        bulletAmount = spawnPoints.Length;
    }
}
