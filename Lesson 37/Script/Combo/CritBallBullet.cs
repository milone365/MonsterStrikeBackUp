using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritBallBullet : Bullet
{
    Transform target;
    Animator anim;
    CircleCollider2D circle;
    bool laucend = false;

    public override void INIT(Vector2 d, ComboBase combo)
    {
        base.INIT(d, combo);
        target = PlayerController.instance.CurrentMonsterT();
        if(target==combo.owner.transform)
        {
            HitController hit = (combo.owner as Monster).getHitController();
            if (hit == null) return;
            target = hit.lasthit;
        }
        transform.SetParent(combo.owner.transform);
        StageManager.instance.AddEndAction(Launch);
        anim = GetComponent<Animator>();
        circle = GetComponent<CircleCollider2D>();
    }

    protected override void TICK()
    {
        base.TICK();
    }
    protected override void TRIGGEREVENT(Collider2D collision)
    {
        if (activate) return;
        if (!laucend) return;
       if(collision.tag==Helper.ENEMY)
        {
            activate = true;
            direction = Vector2.zero;
            if(anim!=null)
            anim.Play(Helper.Active);
        }
    }

    public void Launch()
    {
        laucend = true;
        Vector2 dir;
        if(target==null)
        {
            dir = transform.position * Vector2.up;
        }
        else
        {
            dir = target.position - transform.position;
        }

        dir.Normalize();
        direction = dir;
    }

    public void GetDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circle.radius);
        foreach(var item in colliders)
        {
            if(item.tag==Helper.ENEMY)
            {
                Entity e = item.GetComponent<Entity>();
                Helper.ComboAttack(combo, e);
            }
        }
    }
    public void DELETE()
    {
        Destroy(gameObject);
    }
}
