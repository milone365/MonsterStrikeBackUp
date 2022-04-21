using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    Slider slider;
    [SerializeField]
    Image element_image=null;
    public int hp;
    public int maxHp = 600;
    [SerializeField]
    protected ELEMENT element = ELEMENT.wood;
    public ELEMENT getElement() { return element; }
    bool initialized = false;
    bool death = false;
    [SerializeField]
    GameObject Explosion = null;
    bool GameEnd = false;

    public bool ISDEATH()
    {
        return death;
    }
    public EnemyData data;
    CircleCollider2D circle= null;
    bool is_boss = false;
    public BossPhase phase=BossPhase.blue;
    float counter=0.25f;
    float SpawnTime = 0.25f;
    int spawnedCount = 0;

    public void INIT(EnemyData d)
    {
        data = d;
        phase = d.phase;
        circle = GetComponent<CircleCollider2D>();
        circle.radius = d.radius;
        rend.transform.localScale = d.scale;
        if (UI_Manager.instance != null)
            element_image.sprite = UI_Manager.instance.GetElementSprite(element);
        slider = GetComponentInChildren<Slider>();
        maxHp = d.hp;
        rend.sprite = d.image;
        slider.maxValue = maxHp;
        hp = maxHp;
        slider.value = hp;
        is_boss = (data.type != EnemyType.mob);
        if(is_boss)
        {
            Destroy(slider.gameObject);
        }
        initialized = true;
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
        if (!initialized || death) return;
        hp -= dmg;
        if (hp <= 0)
        {
            death = true;
            slider.value = 0;
            hp = 0;
            if(data.type==EnemyType.lastBoss)
            {
                if(phase==BossPhase.yellow)
                {
                    GameEnd = true;
                    //game shuryo
                    StageManager.instance.GameClear();
                }
                else
                {
                    //next turn
                }

            }
            else
            {
                if(Explosion!=null)
                {
                    Explosion.SetActive(true);
                }
                //oto, effecto
                Destroy(gameObject,0.7f);
            }
        }
        if(is_boss)
        {
            UI_Manager.instance.UpdateEnemyBar(this);
        }
        else
        {
            if (slider != null)
                slider.value = hp;
        }
    }

    private void Update()
    {
        if(GameEnd)
        {
            counter -= Time.deltaTime;
            if(counter<=0)
            {
                counter = SpawnTime;
                GameObject g = Instantiate(Explosion, transform);
                int randx = Random.Range(-2, 2);
                int randy = Random.Range(-2, 2);
                Vector2 v = new Vector2(randx, randy);
                g.transform.localPosition = v;
                g.transform.localScale *= 2;
                g.SetActive(true);
                spawnedCount++;
                if(spawnedCount>10)
                {
                    GameEnd = false;
                    //oto, effecto
                    Destroy(gameObject);
                }
            }
        }
    }
}
