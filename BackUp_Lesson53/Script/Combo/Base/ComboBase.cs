using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboBase : MonoBehaviour
{
    public bool canActive { get ; set ; }
    public Entity owner;
    public LayerMask layer;
    public ELEMENT element = ELEMENT.none;
    [Range(500,500000)]
    public int Amount = 1;
    public Sprite combo_sprite;

    public virtual void Activate()
    {
        
    }

    public virtual void INIT(Entity owner)
    {
        this.owner = owner;
        canActive = true;
        Monster m = owner.GetComponent<Monster>();
        if (m != null)
        {
            layer = Helper.MonsterBullet;
        }
        else
        {
            layer = Helper.EnemyBullet;
        }
    }
}
