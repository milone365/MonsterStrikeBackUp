using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Transform screenpoint = null;

    public static PlayerController instance;
    List<Monster> monsters = new List<Monster>();
    public List<Monster> AllMonsters()
    {
        return monsters;
    }
    [SerializeField]
    List<MonsterData> monsters_data = new List<MonsterData>();
    public Monster current_monster;
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
    [SerializeField]
    Guide guide=null;

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
    Camera main;
    public bool getIsRunning()
    {
        return is_runnig;
    }
    public bool IS_RunningMonster(Transform t)
    {
        if(!is_runnig)
        {
            return false;
        }
        if(t==monsterT)
        {
            return true;
        }
        return false;
    }
    float borderOffset = 0f;
    int comboIndex = 0;
    float elapsed_time = 1;
    [SerializeField]
    float on_time = 1, off_time = 0.5f;
    float whait = 0.5f;
    float activeCounter = 1;
    Image combo_icon;
    bool infield;
    bool InField(Vector2 clickpoint)
    {
        screenpoint.transform.position = clickpoint;
        if(screenpoint.localPosition.x<topLeft.localPosition.x)
        {
            return false;
        }
        if (screenpoint.localPosition.x > bottomRight.localPosition.x)
        {
            return false;
        }
        if (screenpoint.localPosition.y > topLeft.localPosition.y)
        {
            return false;
        }
        if (screenpoint.localPosition.y < bottomRight.localPosition.y)
        {
            return false;
        }
        return true;
    }

    #region START
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        monsters = GetComponentsInChildren<Monster>().ToList();

        for (int i = 0; i < monsters_data.Count; i++)
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
        foreach(var item in monsters)
        {
            if(item.GetCombo().Count>0)
            {
                combo_icon = item.ComboIcoParent.transform.GetChild(1).GetComponent<Image>();
                combo_icon.sprite = item.GetCombo()[0].combo_sprite;
            }
        }
        main = Camera.main;
    }

    internal void ResetOnEndTurn()
    {
        foreach(var item in monsters)
        {
            item.Reset();
        }
    }
    #endregion

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
            start_pos = Input.mousePosition;
            Vector2 worldPosition = main.ScreenToWorldPoint(start_pos);
            infield = InField(worldPosition);
            if(infield)
            {
                elapsed_time = 1;
                in_load = true;


                arrow = current_monster.arrow_R;
                arrow_image().gameObject.SetActive(true);
                UI_Manager.instance.ShowSpeedBar(true);
                speed = 0;
                IconOn_Off(true);
                if (current_monster.HaveGuide)
                {
                    guide.gameObject.SetActive(true);
                    guide.SetGuidePosition(monsterT.position);
                }
            }

        }

        if (!infield) return;
        if (Input.GetMouseButton(0))
        {
            current_position = Input.mousePosition;
            Vector3 dir = start_pos - current_position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrow.rotation=Quaternion.Euler(0,0,angle);
            float size = 0;
            Vector3 normal = dir.normalized;
            if(current_monster.HaveGuide)
            {
                guide.TICK(dir);
            }
            else
            {
                size = Mathf.Abs(normal.x * x_multipler);
                SetArrowSize(size);
            }

        }
        if(in_load)
        {
            Blink();
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
            IconOn_Off(false);
            end_pos = Input.mousePosition;
            in_load = false;
            Vector2 lanchdirect_ = start_pos - end_pos;
            lanchdirect_.Normalize();
            //
            ActiveAllCombo();
            //
            current_monster.InitForLaunch(lanchdirect_, speed, multipler);
            UI_Manager.instance.ShowSpeedBar(false,0.4f);
            arrow_image().gameObject.SetActive(false);
            turnIndex++;
            if(turnIndex>=4)
            {
                turnIndex = 0;
            }
            is_runnig = true;
            activeCounter = on_time;
            whait = off_time;
            guide.gameObject.SetActive(false);
            infield = false;
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
        if(progress_speed<1)
        {
            progress_speed = 1;
        }
        current_monster.GetAnimator().SetBool("Move",true);
    }
    void Swipe()
    {

    }

    public void TurnMantenance()
    {
        foreach(var item in monsters)
        {
            item.OnTurnStart();
            item.getHitController().TurnReset();
        }
        UI_Manager.instance.TurnMatenance();
        SetMonsterTurn(monsters[turnIndex]);
        current_monster.GetAnimator().SetBool("Move",true);
        is_runnig = false;       
    }
    public void StopAllMonsters()
    {
        foreach (var item in monsters)
        {
            item.Stop();
            item.getCollider().isTrigger = false;
            item.getHitController().CanMakeDamage = false;
            item.GetAnimator().SetBool("Move", false);
        }
    }

    void IconOn_Off(bool value)
    {
        if(value==true)
        {
            foreach (var item in monsters)
            {
                if(item!=current_monster)
                {
                    if(item.GetCombo().Count>0)
                        item.ComboIcoParent.SetActive(true);
                }

            }
        }
        else
        {
            foreach(var item in monsters)
            {
                item.ComboIcoParent.SetActive(false);
            }
        }
    }

    void Blink()
    {


        activeCounter -= Time.deltaTime;
        if(activeCounter<=0)
        {
            IconOn_Off(false);
            activeCounter = 0;
            whait -= Time.deltaTime;
            if(whait<=0)
            {
                activeCounter = on_time;
                whait = off_time;
                IconOn_Off(true);
                foreach(var item in monsters)
                {
                    if (item.GetCombo().Count<=0)
                    {
                        item.ComboIcoParent.SetActive(false);
                    }
                    else
                    {
                        if(combo_icon==null)
                         combo_icon = item.ComboIcoParent.transform.GetChild(1).GetComponent<Image>();
                        if (item.GetCombo().Count == 1)
                        {
                            combo_icon.sprite = item.GetCombo()[0].combo_sprite;
                        }
                        else
                        {
                            combo_icon.sprite = item.GetCombo()[comboIndex].combo_sprite;
                        }
                    }

                }
                if(comboIndex==0)
                {
                    comboIndex = 1;
                }
                else
                {
                    comboIndex = 0;
                }
            }
        }
    }

    void ActiveAllCombo()
    {
        foreach(var item in monsters)
        {
            item.Can_activateCombo(true);
        }
    }

    #region HEALTH
    public void Healing(int h)
    {
        hp += (int)h;
        //check
        hp = Mathf.Clamp(hp, 0, maxHP);
        UI_Manager.instance.SetHP(maxHP, hp);
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
    #endregion

    public void OnEndTurn()
    {
        foreach(var item in monsters)
        {
            item.Reset();
        }
    }
}
