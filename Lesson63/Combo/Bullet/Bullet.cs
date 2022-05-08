using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 1;
    [SerializeField]
    protected Vector2 direction = Vector2.zero;
    [SerializeField]
    GameObject effect = null;
    [SerializeField]
    SpriteRenderer rend = null;
    [SerializeField]
    protected float destroyTime = 2;
    protected bool activate = false;
    protected ComboBase combo;

    void Update()
    {
        TICK();
    }
    
    public virtual void INIT(Vector2 d,ComboBase combo)
    {
        direction = d;
        this.combo = combo;
        Transform[] arr = GetComponentsInChildren<Transform>();
        foreach(var item in arr)
        {
            item.gameObject.layer = combo.layer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TRIGGEREVENT(collision);
    }

    protected virtual void TRIGGEREVENT(Collider2D collision)
    {
        if (activate) return;
        activate = true;
        if (effect != null)
        {
            if (rend != null)
            {
                rend.enabled = false;
            }
            effect.SetActive(true);
        }
        if (collision.tag == Helper.ENEMY || collision.tag == Helper.MONSTER)
        {
            Entity e = collision.GetComponent<Entity>();
            Helper.ComboAttack(combo, e);
        }
        Destroy(gameObject, destroyTime);
    }

    protected virtual void TICK()
    {
        if (activate) return;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
