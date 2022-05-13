using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGestView : MonoBehaviour
{
    public List<MonsterData> SellList = new List<MonsterData>();
    public List<MonsterViewBox> monstersBoxs = new List<MonsterViewBox>();
    [SerializeField]
    GameObject sellItem = null;
    [SerializeField]
    Transform sellViewParent = null;
    [SerializeField]
    GameObject shopPanel = null;
    ViewBoxType viewType;

    public void BuildView(ViewBoxType v)
    {
        List<MonsterData> monsters = Inventory.instance.AllMonsters;
        shopPanel.SetActive(v==ViewBoxType.Shop);
        

        foreach (var item in monsters)
        {
            GameObject g = Instantiate(sellItem, sellViewParent) as GameObject;
            MonsterViewBox box = g.GetComponent<MonsterViewBox>();
            box.Init(item,v);
            monstersBoxs.Add(box);
        }
        Menu.instance.navi.OpenPage(this.gameObject);
    }
}
