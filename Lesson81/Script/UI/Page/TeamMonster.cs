﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TeamMonster:MonoBehaviour
{
    public MonsterViewBox Box;
    public Image type;
    public Text luck;
    public GameObject luckyIcon;
    public GameObject[] min;
    public Text ssCount;
    public Text ssSAddCount;
    public Text family;
    public Text warType;
    public Text abilityText;
    public Text chargeAbilityText;
    public Sprite penetrate, reflect;
    [SerializeField]
    GameObject[] infos = null;
    public Text abilities;
    public Text chargeAbilities;
    public GameObject ChargeParent;
    public ComboView[] combos;
    public Text connectSkill;

    public void ActiveInfo(int index)
    {
        foreach (var item in infos)
        {
            item.SetActive(false);
        }
        infos[index].SetActive(true);
    }
    public void Build(MonsterData data)
    {
        foreach (var item in infos)
        {
            item.SetActive(false);
        }
        if (data==null)
        {
            Box.Init(data);
            
            return;
        }
        
        infos[0].SetActive(true);
        Box.Init(data, ViewBoxType.Team);
        if(data.group.shot_type==SHOTTYPE.reflect)
        {
            type.sprite = reflect;
        }
        else
        {
            type.sprite = penetrate;
        }
        ssCount.text = data.ss_turn.ToString();
        if(data.ss_add_turn>0)
        {
            ssSAddCount.text = "+ " + data.ss_add_turn;
        }

        foreach(var item in min)
        {
            item.SetActive(false);
        }
        if(data.WakuminCount()>0)
        {
            for (int i = 0; i < data.WakuminCount(); i++)
            {
                min[i].gameObject.SetActive(true);
            }
        }
        family.text = data.group.family.ToString();
        warType.text = data.group.wartype.ToString();
        if(data.SuperWarType)
        {
            warType.color = Inventory.instance.getColor(ELEMENT.light);
        }

        luck.text= data.Fortune()? "極": data.LuckAmount.ToString();
        //
        AbilityListNullCheck(data.abilities);
        AbilityListNullCheck(data.charge_abilities);
        AbilityListNullCheck(data.connect_skill);
        //
        List<string> ab = data.abilities.Select(x => x.NAME).ToList();
        SetString(abilityText, ab);
        ab=data.charge_abilities.Select(x => x.NAME).ToList();
        SetString(chargeAbilityText, ab);
        ab=data.connect_skill.Select(x => x.NAME).ToList();
        SetString(connectSkill, ab);
        foreach(var item in combos)
        {
            item.game_object.SetActive(false);
        }
        for(int i=0;i<data.friendCombo.Count;i++)
        {
            combos[i].game_object.SetActive(true);
            if(data.friendCombo[i]!=null)
            {
                //
                ComboBase b = data.friendCombo[i].GetComponent<ComboBase>();
                if (b != null)
                    combos[i].Set(b);
            }

        }
    }

    void AbilityListNullCheck(List<BaseAbility> list)
    {
        for(int i=0;i<list.Count;i++)
        {
            if(list[i]==null)
            {
                BaseAbility a = list[i];
                list.Remove(a);
            }
        }
    }

    void SetString(Text s, List<string> list)
    {
        s.text = "";
        foreach(var item in list)
        {
            s.text += item + "\n";
        }
    }

}

[System.Serializable]
public class ComboView
{
    public Image comobIMG;
    public Text ComboPower;
    public Text ComboDescription;
    public GameObject game_object;
    public void Set(ComboBase c)
    {
        comobIMG.sprite = c.combo_sprite;
        ComboDescription.text = c.Description;
        ComboPower.text = "威力: " + c.Amount.ToString();
    }
}