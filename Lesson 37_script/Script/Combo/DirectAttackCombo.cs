using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectAttackCombo : ComboBase
{
    [SerializeField]
    GameObject bullet = null;
    [SerializeField]
    List<Transform>spawnPoints = null;
    [SerializeField]
    bool allEnemies = false;
    List<Enemy> enemies=new List<Enemy>();

    public override void Activate()
    {
        canActive = false;
        spawnPoints.Clear();
        enemies.Clear();
        if (allEnemies)
        {
            foreach (var item in StageManager.instance.GetEnemies())
            {
                spawnPoints.Add(item.transform);
                enemies.Add(item);
            }
        }
        else
        {
            Enemy enemy = StageManager.instance.NearestEnemy(owner.transform);
            if (enemy != null)
            {
                spawnPoints.Add(enemy.transform);
                enemies.Add(enemy);
            }

        }

        if (spawnPoints.Count < 1)
        {
            return;
        }
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject newBullet = Instantiate(bullet, spawnPoints[i].position, Quaternion.identity);
            IDirectDamage d = newBullet.GetComponent<IDirectDamage>();
            if (d != null)
            {
                d.enemy = enemies[i];
                d.monster = owner;
                d.combo = this;
            }
        }
    }

}
