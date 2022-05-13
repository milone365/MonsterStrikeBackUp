using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="NewData",menuName ="MonsterData")]
public class MonsterData : ScriptableObject
{
    public int Level = 1;
    [Range(100, 500)]
    public int atk;
    [Range(100, 500)]
    public int hp;
    [Range(1, 5)]
    public float speed;
    public Sprite image;
    public string Name;
    public int ID;
    public Group group;
    public List<BaseAbility> abilities = new List<BaseAbility>();
    public List<BaseAbility> charge_abilities = new List<BaseAbility>();
    //joken ...
    public List<BaseAbility> connect_skill = new List<BaseAbility>();
    public LuckySkill lucky_skill;
    public List<GameObject> friendCombo = new List<GameObject>();
    [Header("Strike Shot")]
    public int ss_turn = 12;
    public int ss_add_turn = 8;
    public GameObject strikeshotPrefab = null;
    public Vector2 default_size = new Vector2(0.2f, 0.2f);
    [Range(1, 5)]
    public float bar_progressSpeed;
    public List<GameObject> wakuMinlist = new List<GameObject>();
    public int LuckAmount = 1;
    public bool Fortune()
    {
        return LuckAmount >= 99;
    }
    public bool SuperWarType = false;
    public int WakuminCount()
    {
        if(wakuMinlist.Count>0)
        {
            return wakuMinlist.Count;
        }
        return 0;
    }
    public void SetData(MonsterData data)
    {
        atk = data.atk;
        hp = data.hp;
        speed = data.speed;
        image = data.image;
        Name = data.Name;
        ID = data.ID;
        group= new Group(data.group);
        abilities = data.abilities;
        charge_abilities = data.charge_abilities;
        connect_skill = data.connect_skill;
        friendCombo = data.friendCombo;
        lucky_skill = data.lucky_skill;
        ss_turn = data.ss_turn;
        ss_add_turn = data.ss_add_turn;
        strikeshotPrefab = data.strikeshotPrefab;
        default_size = data.default_size;
        bar_progressSpeed = data.bar_progressSpeed;
    }
    
}
