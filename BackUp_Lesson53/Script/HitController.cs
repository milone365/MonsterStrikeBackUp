using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{

    Monster monster;
    SHOTTYPE currentShotType;
    SpriteRenderer rend;
    Sprite original_IMG;
    bool isFrog = false;
    Vector2 original_Size = new Vector2();
    [SerializeField]
    Vector2 frogSize = new Vector2(0.15f, 0.15f);
    [SerializeField]
    List<AntiAbility> antyAbility = new List<AntiAbility>();
    bool super_boost = false;
    [SerializeField]
    GameObject[] bombos = null;
    int bomb_count = 0;
    AntiAbility founded_ability;
    public bool CanMakeDamage = false;
    public Transform lasthit;

    public void INIT(Monster m,SpriteRenderer r)
    {
        monster = m;
        rend = r;
        original_IMG = rend.sprite;
        original_Size = r.transform.localScale;
        antyAbility.AddRange(FindAntiAbility(m.get_data().abilities));
        antyAbility.AddRange(FindAntiAbility(m.get_data().charge_abilities));
        antyAbility.AddRange(FindAntiAbility(m.get_data().connect_skill));
    }

    List<AntiAbility>FindAntiAbility(List<BaseAbility> abilities)
    {
        List<AntiAbility> a = new List<AntiAbility>();
        foreach(var item in abilities)
        {
            if(item is AntiAbility)
            {
                a.Add(item as AntiAbility);
            }
        }
        return a;
    }

    public void SetType(SHOTTYPE s)
    {
        currentShotType = s;
    }

    //speed low->
    private void OnCollisionEnter2D(Collision2D collision)
    {
        lasthit = collision.transform;
        if (collision.transform.tag == Helper.ENEMY)
        {
            if(CanMakeDamage)
            {
                Enemy e = collision.transform.GetComponent<Enemy>();
                Helper.Attack(monster, e);
                if (currentShotType == SHOTTYPE.penetrate)
                {
                    collision.collider.isTrigger = true;
                    return;
                }
            }

        }
        bool hit_monster = false;

        if(collision.transform.tag==Helper.MONSTER)
        {
            
            if(collision.transform!=PlayerController.instance.CurrentMonsterT())
            {
                collision.collider.isTrigger = true;
                collision.transform.GetComponent<Monster>().UseCombo();     
            }
            hit_monster = true;
        }
        if (hit_monster) return;
        var disired_direction = Vector2.Reflect(monster.current_direction, collision.contacts[0].normal);
        monster.current_direction = disired_direction;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == Helper.MONSTER)
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

    public void TouchGimmick(GIMMICK type,int damage=0, ELEMENT element=ELEMENT.none,GameObject gim=null)
    {
        super_boost = false;
        bool anti_ability = HaveAbility(type);
        switch (type)
        {
            case GIMMICK.barrierForce:
                break;
            case GIMMICK.warp:
                if(anti_ability)
                {
                    if(super_boost)
                    {
                        //power up

                    }
                }
                else
                {
                    gim.GetComponent<Warp>().OnWarpEnter(monster);
                }
                break;
            case GIMMICK.damage_wall:
                if(!anti_ability)
                {
                    Helper.DamagePlayer(monster, element, type, damage);
                }
                else
                {
                    if(super_boost)
                    {

                    }
                }
                break;
            case GIMMICK.decellerate_wall:
                break;
            case GIMMICK.wind:
                if(super_boost)
                {
                    //Speed Bonus

                }
                break;
            case GIMMICK.mine:
                if(anti_ability)
                {
                    if(founded_ability is MineSweaper)
                    {
                        if (bomb_count < 4)
                        {
                            bomb_count++;
                            SetBombActive();
                        }
                        else
                        {
                            //power up
                        }
                        Destroy(gim);
                    }
                    else
                    {
                        Debug.Log("FLY IS ACTIVED");
                    }
                    
                }
                else
                {
                    Helper.DamagePlayer(monster, element, type, damage);
                    Destroy(gim);
                }
                break;
            case GIMMICK.magicCircle:               
                if(anti_ability)
                {
                    if(super_boost)
                    {
                        if (!isFrog)
                        {
                            isFrog = true;
                            rend.sprite = UI_Manager.instance.super_frog;
                            rend.transform.localScale = frogSize;
                        }
                        else
                        {
                            isFrog = false;
                            rend.sprite = original_IMG;
                            rend.transform.localScale = original_Size;
                        }
                    }
                }
                else
                {
                    if(!isFrog)
                    {
                        isFrog = true;
                        rend.sprite = UI_Manager.instance.frog;
                        rend.transform.localScale = frogSize;
                    }
                    else
                    {
                        isFrog = false;
                        rend.sprite = original_IMG;
                        rend.transform.localScale = original_Size;
                    }
                }
                break;
            case GIMMICK.block:
                break;
            case GIMMICK.Spike:
                Helper.DamagePlayer(monster, element, type, damage);
                break;
        }
    }

    public bool HaveAbility(GIMMICK g)
    {
        foreach(var item in antyAbility)
        {
            if(item.anti_gimmick==g)
            {
                super_boost = item.Super_boost;
                founded_ability = item;
                return true;
            }
        }
        return false;
    }

    public void SetBombActive()
    {
        if (bomb_count <= 0) return;
        for(int i=0;i<bomb_count;i++)
        {
            bombos[i].SetActive(true);
        }
    }
}
