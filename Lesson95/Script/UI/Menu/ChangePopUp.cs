using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ChangePopUp : MonoBehaviour
{
    [SerializeField]
    DefaultMonsterPanelView upPanel, downPanel;
    [SerializeField]
    bool multiple = false;

    private void Start()
    {
        if(!multiple)
        {
            MonsterData selected = Inventory.instance.selectedMonster;
            if (selected != null)
                upPanel.SetMonster(selected);
            MonsterData next = Inventory.instance.nextMonster;
            if (next != null)
                downPanel.SetMonster(next);
        }
        else
        {
            upPanel.SetTeam(Inventory.instance.current_team().monster);
            downPanel.SetTeam(Inventory.instance.teamOrderList.ToArray());
        }
    }
    public void Change()
    {
        if(multiple)
        {
            Inventory.instance.ChangeTeam();
        }
        else
        {
            Inventory.instance.ChangeMonster();
        }
        Menu.instance.navi.objectToDestroy = this.gameObject;
        Menu.instance.navi.GoBack();
    }
    public void Delete()
    {
        if (multiple)
            Inventory.instance.teamOrderList.Clear();
        Menu.instance.monsterGestion.ClearAllSelectedMonsters();
        Destroy(gameObject);
    }
    public void PowerUp()
    {

    }
}
