using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<MonsterData> AllMonsters = new List<MonsterData>();
    [SerializeField]
    MonsterData selectedMonster = null;
    [SerializeField]
    Color red, blue, green, yellow, violet;
    PlayerData data;
    [SerializeField]
    MonsterData[] sampleTeam=new MonsterData[3];
    [SerializeField]
    MonsterData sampleFriendCharacter=null;
    public Team current_team()
    {
        return data.current_team;
    }
    public List<Team> teams()
    {
        return data.teams;
    }


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        AllMonsters = Resources.LoadAll<MonsterData>("Inventory").ToList();
        data = Menu.instance.data;
        if(data.current_team.monster.Length<1)
        {
            data.current_team.monster = new MonsterData[3];
            current_team().monster = sampleTeam;
        }
    }

    public Color getColor(ELEMENT element)
    {
        Color c = Color.white;
        switch (element)
        {
            case ELEMENT.fire:
                c = red;
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
}

[System.Serializable]
public class Team
{
    public MonsterData[] monster = null;
    public string TeamName = "Sample";

}