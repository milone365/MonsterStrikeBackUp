using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    public Entity owner;
    public bool canActive { get; set; }
    public Sprite combo_sprite;
    protected Transform target;

    public virtual void Activate()
    {
        canActive = false;
    }
    public virtual void INIT(Entity owner)
    {
        this.owner = owner;
    }
}
