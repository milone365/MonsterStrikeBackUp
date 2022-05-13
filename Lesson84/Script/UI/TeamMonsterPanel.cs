using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class TeamMonsterPanel : MonoBehaviour
{
    [SerializeField]
    Text Hp_text = null;
    [SerializeField]
    Text TeamName = null;
    [SerializeField]
    MinionView minion = null;
    public List<TeamMonster> CurrentMonsterSlots = new List<TeamMonster>();
    List<MonsterData> currentTeam = new List<MonsterData>();

    public void Draw()
    {
        CurrentMonsterSlots.Clear();
        //panel set
        currentTeam = Inventory.instance.current_team().monster.ToList();
        //
        TeamName.text = Inventory.instance.current_team().TeamName;
        //panel info box tory
        CurrentMonsterSlots = transform.GetComponentsInChildren<TeamMonster>().ToList();
        //parameters setting
        for (int i = 0; i < CurrentMonsterSlots.Count; i++)
        {
            MonsterData data = null;
            if (currentTeam[i] != null)
            {
                data = currentTeam[i];
            }
            CurrentMonsterSlots[i].Build(data);
        }
        //set hp

        Hp_text.text = getAllHp().ToString();
    }


    int getAllHp()
    {
        int temp = 0;
        for (int i = 0; i < CurrentMonsterSlots.Count; i++)
        {
            MonsterData data = currentTeam[i];
            if (data == null) continue;
            temp += data.Level * data.hp;
        }
        return temp;
    }
}
