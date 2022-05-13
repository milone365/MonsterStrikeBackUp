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

    public const string Shield = "SH";
    public const string Block = "BL";
    public const string MagicCircle = "CI";
    public const string Wind = "WI";
    public const string Bitton = "BT";
    public const string Spike = "SP";
    public const string Spike2 = "SP2";
    public const string Warp = "WA";
    public const string Mine = "MI";

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
    
   public static string MapElementToString(string s)
    {
        string temp = s;

        switch(s)
        {
            case Shield:
                temp = "Shield";
                break;
            case Block:
                temp = "Block";
                break;
            case MagicCircle:
                temp = "MagicCircle";
                break;
            case Wind:
                temp = "Wind";
                break;
            case Bitton:
                temp = "Bitton";
                break;
            case Spike:
                temp = "Spike";
                break;
            case Spike2:
                temp = "Spike2";
                break;
            case Mine:
                temp = "Mine";
                break;
        }

        return temp;
    }

}
