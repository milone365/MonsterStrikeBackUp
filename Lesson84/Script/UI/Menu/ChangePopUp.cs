using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePopUp : MonoBehaviour
{
    [SerializeField]
    DefaultMonsterPanelView upPanel, downPanel;

    private void Start()
    {
        MonsterData selected = Inventory.instance.selectedMonster;
        if(selected!=null)
         upPanel.SetMonster(selected);
        MonsterData next = Inventory.instance.nextMonster;
        if(next!=null)
         downPanel.SetMonster(next);
    }
    public void Change()
    {
        Inventory.instance.ChangeMonster();
        Menu.instance.navi.GoBack(this.gameObject);
    }
    public void Delete()
    {
        Destroy(gameObject);
    }
    public void PowerUp()
    {

    }
}
