using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Egg : MonoBehaviour
{
    [SerializeField]
    Image monsterIMG = null;
    [SerializeField]
    Image Border = null;
    Animator anim;
    Sprite monsterSprite;

    public void PlayAnnimation(MonsterData data)
    {
        anim = GetComponent<Animator>();
        Border.color = Inventory.instance.getColor(data.group.element);
        monsterSprite = data.image;
        anim.Play("EggAnimation");
    }

    public void ShowMonster()
    {
        Border.enabled = true;
        monsterIMG.sprite = monsterSprite;
        monsterIMG.color = Color.white;

    }
  
}
