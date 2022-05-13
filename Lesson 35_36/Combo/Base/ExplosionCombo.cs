using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCombo : ComboBase
{
    [SerializeField]
    float radius =1;
    [SerializeField]
    GameObject effect = null;
    Animator anim;

    public override void Activate()
    {
        canActive = false;
        if (anim != null)
        {
            anim.Play(Helper.Active);
        }
        Collider2D[] arr = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var item in arr)
        {
            if (item.tag == Helper.MONSTER)
            {
                Monster m = item.GetComponent<Monster>();
                if (m != null)
                {
                    m.UseCombo();
                }
            }
        }
    }

    public override void INIT(Entity owner)
    {
        base.INIT(owner);
        if (effect != null)
            anim = effect.GetComponent<Animator>();
    }

}
