using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="NewData",menuName ="MonsterData")]
public class MonsterData : ScriptableObject
{
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
    
}
