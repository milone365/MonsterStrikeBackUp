using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DefaultMonsterPanelView : MonoBehaviour
{
    [SerializeField]
    Image shotTipeImg=null;
    [SerializeField]
    Slider hpbar = null, powerBar = null, speedBar = null;
    [SerializeField]
    Text hp_text = null, powerText = null, speed_text = null;
    [SerializeField]
    Text strikeShotDescription = null, strikeShotTurn = null;
    [SerializeField]
    Text luck = null;
    [SerializeField]
    Text comboText = null, wakumin_text = null;
    MonsterData data;
    [SerializeField]
    Image combImage = null;
    MonsterViewBox box;

    public void SetMonster(MonsterData data)
    {
        
        this.data = data;
        if (data == null) return;
        shotTipeImg.sprite = (data.group.shot_type == SHOTTYPE.reflect ? Inventory.instance.reflect : Inventory.instance.penetrate);
        SetBar(hpbar, hp_text, data.hp);
        SetBar(powerBar, powerText, data.atk);
        SetBar(speedBar, speed_text, data.speed);
        strikeShotDescription.text = data.strikeshotPrefab.name;
        strikeShotTurn.text = data.ss_turn.ToString()+" ターン";
        if(data.friendCombo.Count>0 && data.friendCombo[0]!=null)
        {
            ComboBase combo = data.friendCombo[0].GetComponent<ComboBase>();
            if(combo!=null)
            {
                comboText.text = data.friendCombo[0].gameObject.name;
                combImage.sprite = combo.combo_sprite;
            }

        }

        if(data.wakuMinlist.Count>0)
        {
            if (data.wakuMinlist[0] != null)
                wakumin_text.text = data.wakuMinlist[0].name;
        }
        box = GetComponentInChildren<MonsterViewBox>();
        if(box!=null)
        {
            box.Init(data, ViewBoxType.Show);
        }
    }

    void SetBar(Slider slider,Text t,float valuer)
    {
        int val = Helper.ByLevel(data.Level, valuer);
        hpbar.maxValue = val;
        hpbar.value = val;
        t.text = val.ToString();
    }
}
