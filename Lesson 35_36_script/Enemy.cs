using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    Slider slider;
    [SerializeField]
    Image element_image=null;
    protected int hp;
    [SerializeField]
    protected int maxHp = 600;
    [SerializeField]
    protected ELEMENT element = ELEMENT.wood;

    private void Start()
    {
        element_image.sprite= UI_Manager.instance.GetElementSprite(element);
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = maxHp;
        hp = maxHp;
        slider.value = hp;
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Monster")
        {
            getCollider().isTrigger= false;
        }
    }

    public override void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            slider.value = 0;
            hp = 0;
            //oto, effecto
            Destroy(gameObject);
            return;
        }
        slider.value = hp;
    }

}
