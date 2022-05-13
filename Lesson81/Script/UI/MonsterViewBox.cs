using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterViewBox : MonoBehaviour
{
    [HideInInspector]
    public MonsterData data;
    [SerializeField]
    Image Border = null;
    [SerializeField]
    Image MonsterImage = null;
    [SerializeField]
    GameObject SellMark = null;
    bool selling = false;
    [SerializeField]
    ViewBoxType type=ViewBoxType.Shop;
    Button button;
    [SerializeField]
    Text levelText = null;
    [SerializeField]
    Sprite EmptyIMG = null;

    private void Start()
    {
        if (button == null)
            button = GetComponent<Button>();
    }
    public void Init(MonsterData data,ViewBoxType t=ViewBoxType.none)
    {
        if(data==null)
        {
            MonsterImage.sprite = EmptyIMG;
            button.onClick.RemoveAllListeners();
            return;
        }
        this.data = data;
        type = t;
        MonsterImage.sprite = data.image;
        Border.color = Inventory.instance.getColor(data.group.element);
        if(data.Fortune())
        {
            levelText.text= "極";
        }
        else
        {
            levelText.text = data.Level.ToString();
        }
        button = GetComponent<Button>();
        switch (type)
        {
            case ViewBoxType.Team:
                button.onClick.AddListener(ChangeMonster);
                break;
            case ViewBoxType.Shop:
                button.onClick.AddListener(SelectedForSell);
                break;
            case ViewBoxType.Menu:
                break;
            case ViewBoxType.none:
                break;
        }
    }
    public void SelectedForSell()
    {
        selling = !selling;
        SellMark.SetActive(selling);
        if(selling)
        {
            Menu.instance.monsterGestion.SellList.Add(this.data);
        }
        else
        {
            Menu.instance.monsterGestion.SellList.Remove(this.data);
        }
    }

    public void ChangeMonster()
    {

    }

}
public enum ViewBoxType
{
    Team, Shop, Menu, none
}