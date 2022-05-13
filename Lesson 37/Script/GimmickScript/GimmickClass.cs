using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickClass : MonoBehaviour,IResetable
{
    [SerializeField]
    GIMMICK gimmick_type=GIMMICK.block;
    [SerializeField]
    int damage = 0;
    [SerializeField]
    ELEMENT element = ELEMENT.none;
    [SerializeField]
    bool only_one_time = false;
    bool Actived = false;


    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Reset()
    {
        Actived = false;
        if(anim!=null)
        {
            anim.SetBool("Activate", Actived);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Monster")
        {
            if (Actived) return;
            collision.GetComponent<HitController>().TouchGimmick(gimmick_type,damage,element,gameObject);
            if(only_one_time)
            {
                Actived = true;
                if (anim != null)
                {
                    anim.SetBool("Activate", Actived);
                }
            }
        }
    }
}
