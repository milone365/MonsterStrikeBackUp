using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UI_Manager : MonoBehaviour
{
    #region Value
    public static UI_Manager instance;
    [SerializeField]
    Sprite[] elements = null;
    public Sprite frog;
    public Sprite super_frog;
    [SerializeField]
    Text hp_text = null;
    [SerializeField]
    Slider hp_bar = null;
    [SerializeField]
    GameObject SpeedBar = null;
    Slider speed_bar;
    [SerializeField]
    Image speedBarHightPoint = null;
    Color default_color;
    bool bar_active = false;
    [SerializeField]
    Color red, blue, green, yellow, violet;
    [SerializeField]
    List<CharacterBox> characters = new List<CharacterBox>();
    [SerializeField]
    CharacterBox box = null;
    [SerializeField]
    Transform box_parent = null;
    [SerializeField]
    Bar enemy_bar=new Bar();
    public GameObject stageClear = null;
    [SerializeField]
    public GameObject next_turn = null;
    [SerializeField]
    GameObject LastBossPanel = null;
    #endregion
    [SerializeField]
    GameObject ssScreen = null;
    [SerializeField]
    Image ssMonsterIMG = null;

    private void Awake()
    {
        instance = this;
        speed_bar = SpeedBar.GetComponentInChildren<Slider>();
        default_color = speedBarHightPoint.color;
        ShowSpeedBar(false);
    }

    public Sprite GetElementSprite(ELEMENT e)
    {
        Sprite s = null;

        switch (e)
        {
            case ELEMENT.fire:
                return elements[0];
                break;
            case ELEMENT.water:
                return elements[1];
                break;
            case ELEMENT.wood:
                return elements[2];
                break;
            case ELEMENT.light:
                return elements[3];
                break;
            case ELEMENT.dark:
                return elements[4];
                break;
            case ELEMENT.none:
                return null;
                break;
        }

        return s;
    }

    public void SetHP(int max, int current)
    {
        hp_bar.maxValue = max;
        hp_bar.value = current;
        hp_text.text = current + "/" + max;
    }

    public void ShowSpeedBar(bool val,float time=0)
    {
        bar_active = val;
        Invoke("ActiveSpeedBar", time);
    }

    void ActiveSpeedBar()
    {
        SpeedBar.gameObject.SetActive(bar_active);
    }

    public void SetSpeedBar(float max,float current)
    {
        speed_bar.maxValue = max;
        speed_bar.value = current;
        if(current>=max*0.98)
        {
            speedBarHightPoint.color =red;
        }
        else
        {
            speedBarHightPoint.color = default_color;
        }
    }

    public Color getColor(ELEMENT element)
    {
        Color c = Color.white;
        switch (element)
        {
            case ELEMENT.fire:
                c= red;
                break;
            case ELEMENT.water:
                c = blue;
                break;
            case ELEMENT.wood:
                c = green;
                break;
            case ELEMENT.light:
                c = yellow;
                break;
            case ELEMENT.dark:
                c = violet;
                break;
        }
        return c;
    }

    public void SetCharacters(List<Monster>monsters)
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(box, box_parent);
        }
        characters = box_parent.GetComponentsInChildren<CharacterBox>().ToList();

        for (int i=0;i<characters.Count;i++)
        {
            characters[i].SetBox(monsters[i]);
        }
    }

    public void SetEnemyBar()
    {
        Enemy boss=null;
        foreach(var item in StageManager.instance.map.GetEnemies())
        {
            if(item.data.type==EnemyType.boss|| item.data.type == EnemyType.lastBoss)
            {
                boss = item;
                break;
            }
        }
        if(boss==null)
        {
            enemy_bar.barObject.SetActive(false);
            return;
        }
        enemy_bar.barObject.SetActive(true);

        bool  lastBoss = (boss.data.type==EnemyType.lastBoss);
        enemy_bar.icon.enabled =lastBoss;
        enemy_bar.elementIMG.sprite = GetElementSprite(boss.getElement());
        if(lastBoss)
        {
            enemy_bar.enemyText.text = "BOSS";
        }
        else
        {
            enemy_bar.enemyText.text = "ENEMY";
        }
        enemy_bar.SliderBackground.color = BackGroundColor(boss.data.phase);
        enemy_bar.SliderFillArea.color = fillAreaColor(boss.data.phase);
        UpdateEnemyBar(boss);
    }

    public void UpdateEnemyBar(Enemy boss)
    {
        enemy_bar.slider.maxValue = boss.maxHp;
        enemy_bar.slider.value = boss.hp;
    }

    Color fillAreaColor(BossPhase phase)
    {
        Color color = Color.black;
        switch (phase)
        {
            case BossPhase.yellow:
                color = Color.yellow;
                break;
            case BossPhase.green:
                color = Color.green;
                break;
            case BossPhase.lightblue:
                color = Color.cyan;
                break;
            case BossPhase.blue:
                color = Color.blue;
                break;
        }
        return color;
    }

    Color BackGroundColor(BossPhase phase)
    {
        Color color = Color.black;
        switch (phase)
        {
            case BossPhase.yellow:
                color = Color.black;
                break;
            case BossPhase.green:
                color = Color.yellow;
                break;
            case BossPhase.lightblue:
                color = Color.green;
                break;
            case BossPhase.blue:
                color = Color.cyan;
                break;
        }
        return color;
    }

    public void ShowPlayerTurn(bool valuer)
    {
        next_turn.SetActive(valuer);
    }

    public void ShowBossPanel(bool t)
    {
        LastBossPanel.SetActive(t);
    }

    public void TurnMatenance()
    {
        foreach(var item in characters)
        {
            item.ssCountObj.ShowAnimation();
        }
    }

    public void EraseTargetSS(Monster m)
    {
        foreach (var item in characters)
        {
            if(item.monster=m)
            {
                item.ssCountObj.ShowAnimation();
            }
        }
    }

    public void ShowSSScreen(bool val, Sprite sprite=null)
    {
        if(val==true)
        {
            ssScreen.SetActive(true);
            ssMonsterIMG.sprite = sprite;
        }
        else
        {
            ssScreen.SetActive(false);
        }
    }
}
/*
 * 
    public Slider slider;
 * */
