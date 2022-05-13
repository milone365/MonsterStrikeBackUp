using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Group
{
    public STARS stars;
    public ELEMENT element;
    public WARTYPE wartype;
    public SHOTTYPE shot_type;
    public EVOLUTION evolution;
    public FAMILY family;
}
public enum STARS
{
    one,
    two,
    three,
    four,
    five,
    six
}

public enum ELEMENT
{
    fire,
    water,
    wood,
    light,
    dark,
    none
}

public enum SHOTTYPE
{
    reflect,
    penetrate
}
public enum EVOLUTION
{
    SHINKAMAE,
    SHINKA,
    KAMIKA,
    JUSHINKA,
    JUSHINKAKAI
}

public enum WARTYPE
{
    balance,
    power,
    speed,
    bombing
}

public enum FAMILY
{
    dragon,
    beast,
    robot
}

public enum GIMMICK
{
    barrierForce,
    warp,
    damage_wall,
    decellerate_wall,
    wind,
    mine,
    magicCircle,
    block,
    Spike
}

public enum Size
{
    none,
    S,
    M,
    L,
    EL
}

public enum LuckySkill
{
    none,
    critical,
    shield,
    guide,
    combo_critical
}

public enum BossPhase
{
    yellow=1,
    green,
    lightblue,
    blue
}

public enum EnemyType
{
    mob,
    boss,
    lastBoss
}