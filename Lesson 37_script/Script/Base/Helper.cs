using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public static string MONSTER = "Monster";
    public static string ENEMY = "Enemy";
    public static string WALL = "Wall";
    public static LayerMask MonsterBullet = 8;
    public static LayerMask EnemyBullet = 9;
    public static string Active = "Active";


    public static void Attack(Entity attacker,Entity defender)
    {
        /////
        int damage = 1+attacker.GetMaxAtk();
        /////
        if(attacker is Monster)
        {
            defender.TakeDamage(damage);
        }

    }

    //
    public static void DamagePlayer(Monster m,ELEMENT element,GIMMICK gimmick,int damage)
    {
        int calc_damage = 1;
        calc_damage += damage;

        m.getHealthManager().TakeDamage(calc_damage);
    }

    public static void ComboAttack(ComboBase combo,Entity entity)
    {
        if(combo==null)
        {
            Debug.Log("COMBO IS NULL");
        }
        int damage = combo.Amount;
        int bonus = 0;
        if (entity is Monster)
        {
            Monster m = entity as Monster;
            m.getHealthManager().TakeDamage(damage);
        }
        else
        {
            entity.TakeDamage(damage);
        }
    }
}
