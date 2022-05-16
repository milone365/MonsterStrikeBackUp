using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class MonsterGestView : MonoBehaviour
{
    public List<MonsterData> SellList = new List<MonsterData>();
    public List<MonsterViewBox> monstersBoxs = new List<MonsterViewBox>();
    [SerializeField]
    GameObject monsterBoxPrefab = null;
    [SerializeField]
    GameObject MonsterAreaPrefab = null;
    [SerializeField]
    GameObject shopPanel = null;
    ViewBoxType viewType;
    [SerializeField]
    Transform content=null;
    Vector3 monstAreaPos;
    private void Start()
    {
        //monstAreaPos = Inventory.instance.monsterArea.position;
        
    }
    private void OnDisable()
    {
        ClearAll();
    }

    public void ClearAll()
    {
        //destroy
        Destroy(Inventory.instance.monsterArea.gameObject);
        //create
        GameObject g = Instantiate(MonsterAreaPrefab,content);
        //g.transform.position = monstAreaPos;
        //reset
        Inventory.instance.monsterArea = g.transform;
        //clear all
        SellList.Clear();
        monstersBoxs.Clear();
    }
    public void BuildView(ViewBoxType v)
    {
        List<MonsterData> monsters = Inventory.instance.AllMonsters;
        shopPanel.SetActive(v==ViewBoxType.Shop);
        

        foreach (var item in monsters)
        {
            GameObject g = Instantiate(monsterBoxPrefab,Inventory.instance.monsterArea) as GameObject;
            MonsterViewBox box = g.GetComponent<MonsterViewBox>();
            box.Init(item,v);
            monstersBoxs.Add(box);
        }
        Menu.instance.navi.OpenPage(this.gameObject);
    }

    public void ClearAllSelectedMonsters()
    {
        foreach(var item in monstersBoxs)
        {
            item.DeselectMonster();
        }
    }
}
