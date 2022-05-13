using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<MonsterData> AllMonsters = new List<MonsterData>();
    public MonsterData selectedMonster = null;
    public MonsterData nextMonster = null;
    public int selectIndex = 0;
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
    public Sprite penetrate, reflect;
    [SerializeField]
    GameObject ChangePopUp = null;
    [SerializeField]
    Transform mosnterArea=null;
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

    public void ChangeMonster()
    {
        selectedMonster = nextMonster;
        current_team().monster[selectIndex] = selectedMonster;
    }
    public void InitForChange()
    {
        GameObject g = Instantiate(ChangePopUp, mosnterArea);
    }
}

[System.Serializable]
public class Team
{
    public MonsterData[] monster = null;
    public string TeamName = "Sample";

}