using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewPlayer",menuName ="PlayerData")]
public class PlayerData : ScriptableObject
{
    public string UniqueID;
    public int orbCount;
    public int questOrbCount;
    public int coin;
    public int rank=1;
    public int stamina;
    public string playerName;
    public string title;
    public int currentPoint = 0;

    public MonsterData friendCharacter;
    public Team current_team;
    public List<Team> teams = new List<Team>();
}
