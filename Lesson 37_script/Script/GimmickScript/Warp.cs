using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    bool Actived = false;
    GIMMICK gimmick_type = GIMMICK.warp;
    [SerializeField]
    Transform other_warp = null;
    Monster monster;
    public bool WARPOFF = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            if (WARPOFF) return;
            if (Actived) return;
            collision.GetComponent<HitController>().TouchGimmick(gimmick_type,0,ELEMENT.none,this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            Actived = false;
            WARPOFF = false;
        }
    }

    public void OnWarpEnter(Monster m)
    {
        other_warp.GetComponent<Warp>().WARPOFF = true;
        monster = m;
        Actived = true;
        m.Stop();
        m.transform.SetParent(this.transform);
        m.transform.localPosition = Vector2.zero;
        m.GetAnimator().CrossFade("WarpIN", 1.5f);
        StartCoroutine(WarpTransition());
    }

    IEnumerator WarpTransition()
    {
        yield return new WaitForSeconds(1);
        monster.transform.SetParent(other_warp);
        monster.transform.localPosition = Vector2.zero;
        monster.GetAnimator().CrossFade("WarpOut", 1.5f);
        yield return new WaitForSeconds(1.5f);
        monster.transform.SetParent(null);
        yield return new WaitForSeconds(0.2f);
        monster.RestoreVelocity();
    }
   
}
