using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class TeamViewPage : MonoBehaviour
{
  
   
    [SerializeField]
    TeamMonsterPanel currentPanel = null;
    int parameterIndex = 0;
    int max = 4;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        currentPanel = GetComponentInChildren<TeamMonsterPanel>();
        Menu.instance.navi.OpenPage(this.gameObject);
        currentPanel.Draw();
        max = Menu.instance.data.teams.Count;
    }

    public void ViewReset()
    {
        foreach(var item in currentPanel.CurrentMonsterSlots)
        {
            item.Build(null);
        }
    }

    public void ScrollParametes()
    {
        parameterIndex++;
        if(parameterIndex>=max)
        {
            parameterIndex = 0;
        }
        foreach(var item in currentPanel.CurrentMonsterSlots)
        {
            item.ActiveInfo(parameterIndex);
        }
    }

    public void SetMonsterOrder()
    {
        Menu.instance.MonsterGestionPage(ViewBoxType.Team);
        gameObject.SetActive(false);
    }

    public void ChangeName()
    {

    }

    public void ShowAllTeams()
    {

    }
    public void GoToMinionPage()
    {

    }
    public void ScrollRight()
    {
        parameterIndex++;
        if(parameterIndex>max-1)
        {
            parameterIndex = 0;
        }
        anim.Play("Right");
    }
    public void ScrollLeft()
    {
        parameterIndex--;
        if (parameterIndex < 0)
        {
            parameterIndex = max-1;
        }
        anim.Play("Left");
    }

    public void SetTeamInfo()
    {
        PlayerData data = Menu.instance.data;
        data.current_team = data.teams[parameterIndex];
        currentPanel.Draw();
    }

}


