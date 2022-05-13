using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    List<Monster> monsters = new List<Monster>();
    [SerializeField]
    List<MonsterData> monsters_data = new List<MonsterData>();

    Monster current_monster;
    [SerializeField]
    Vector2 launchDirection = new Vector2();
    float speed = 0;
    float max_speed;
    [SerializeField]
    float multipler = 1;
    [SerializeField]
    Transform topLeft = null, bottomRight = null;
    Transform monsterT;
    public Transform CurrentMonsterT() { return monsterT; }
    int maxHP = 0;
    int hp = 0;
    bool LoseMatch = false;
    Vector3 start_pos, end_pos, current_position;
    bool in_load = false;
    Transform arrow;
    Transform arrow_image()
    {
        return arrow.GetChild(0);
    }
    [SerializeField]
    int x_multipler = 2;
    [SerializeField]
    float min_distanceAmount = 0.3f;
    float progress_speed;
    float resetOffset = 60;
    [SerializeField]
    StageManager manager = null;
    public StageManager GetStage() { return manager; }
    int turnIndex = 0;
    bool is_runnig = false;
    float borderOffset = 0f;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        monsters = GetComponentsInChildren<Monster>().ToList();

        for(int i=0;i<monsters_data.Count;i++)
        {
            monsters[i].SetMonster(monsters_data[i]);
        }
        SetMonsterTurn(monsters[turnIndex]);
        
        foreach (var item in monsters)
        {
            maxHP += item.getHealthManager().maxHp;
        }
        hp = maxHP;
        UI_Manager.instance.SetHP(maxHP, hp);
        UI_Manager.instance.SetCharacters(monsters);
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.isPlayerTurn())
        {
            if(!is_runnig)
            {
                MouseUPDATE();
                Swipe();
            }
            RemainInField();
        }
        
    }

    public void TakeDamage(int dmg)
    {
        if (LoseMatch) return;
        hp -= dmg;
        if (hp <= 0)
        {
            hp = 0;
            LoseMatch = true;
            OnLoseGame();
        }
        UI_Manager.instance.SetHP(maxHP, hp);
    }

    void OnLoseGame()
    {
        current_monster.Stop();
    }

    void RemainInField()
    {
        if (monsterT == null) return;

        for(int i=0;i<monsters.Count;i++)
        {
            if(monsters[i]!=monsterT)
            {
                Transform m = monsters[i].transform;
                if (m.position.x >= bottomRight.position.x)
                {
                    m.position = new Vector2(bottomRight.position.x - borderOffset, m.position.y);
                }
                if (m.position.x <= topLeft.position.x)
                {
                    m.position = new Vector2(topLeft.position.x + borderOffset, m.position.y);
                }
                //
                if (m.position.y >= topLeft.position.y)
                {
                    m.position = new Vector2(m.position.x, topLeft.position.y - borderOffset);
                }
                if (m.position.y <= bottomRight.position.y)
                {
                    m.position = new Vector2(m.position.x, bottomRight.position.y + borderOffset);
                }
            }
        }

    }

    void MouseUPDATE()
    {
        if (Input.GetMouseButtonDown(0))
        {
            in_load = true;
            start_pos = Input.mousePosition;
            arrow = current_monster.arrow_R;
            arrow_image().gameObject.SetActive(true);
            UI_Manager.instance.ShowSpeedBar(true);
            speed = 0;
        }
        if (Input.GetMouseButton(0))
        {
            current_position = Input.mousePosition;
            Vector3 dir = start_pos - current_position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrow.rotation=Quaternion.Euler(0,0,angle);
            float size = 0;
            Vector3 normal = dir.normalized;
            size = Mathf.Abs(normal.x * x_multipler);
            SetArrowSize(size);
        }
        if(in_load)
        {
            //speedbar load
            speed+=progress_speed;
            if(speed>=max_speed+resetOffset)
            {
                speed = 0;
            }
            UI_Manager.instance.SetSpeedBar(max_speed, speed);
        }
        if (Input.GetMouseButtonUp(0))
        {
            end_pos = Input.mousePosition;
            in_load = false;
            Vector2 lanchdirect_ = start_pos - end_pos;
            lanchdirect_.Normalize();
            current_monster.Launch(lanchdirect_, speed, multipler);
            UI_Manager.instance.ShowSpeedBar(false,0.4f);
            arrow_image().gameObject.SetActive(false);
            turnIndex++;
            if(turnIndex>=4)
            {
                turnIndex = 0;
            }
            is_runnig = true;
        }

    }

    void SetArrowSize(float size)
    {
        Transform image = arrow_image();
        image.localScale = new Vector3(size, image.localScale.y, image.localScale.z);
    }

    public void SetMonsterTurn(Monster m)
    {
        current_monster = m;
        max_speed = m.GetMaxSpeed();
        monsterT = current_monster.transform;
        progress_speed = m.get_data().bar_progressSpeed;
    }
    void Swipe()
    {

    }

    public void TurnMantenance()
    {
        foreach(var item in monsters)
        {
            item.OnTurnStart();
        }
        SetMonsterTurn(monsters[turnIndex]);
        is_runnig = false;
        foreach(var item in monsters)
        {
            item.getCollider().isTrigger = false;
            item.getHitController().CanMakeDamage = false;
        }
        
    }
    public void StopAllMonsters()
    {
        foreach (var item in monsters)
        {
            item.Stop();
        }
    }
}
