using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBox : MonoBehaviour
{
    [SerializeField]
    Image border = null, icon = null, wak_icon = null;
    [SerializeField]
    Text ss_count = null, ss_add_count = null;
    Monster monster;
    Button button;
    bool canActiveSS = false;
    bool ss_active = false;

    public void SetBox(Monster monster)
    {
        this.monster = monster;
        MonsterData data = monster.get_data();
        border.color = UI_Manager.instance.getColor(data.group.element);
        ss_count.text = monster.ss_counter.ToString();
        //active
        ss_add_count.text = monster.ss_add_counter.ToString();
        icon.sprite = data.image;
        button = GetComponent<Button>();
        button.onClick.AddListener(ActiveSS);
    }

    void ActiveSS()
    {
        if(canActiveSS)
        {
            ss_active = !ss_active;

            if(ss_active)
            {
                ss_count.text = "GO!";
            }
            else
            {
                ss_count.text = "OK";
            }

        }
    }
    
    public void UpdateSSCount()
    {
        if(monster.ss_counter>0)
        {
            canActiveSS = false;
            ss_count.text = monster.ss_counter.ToString();
        }
        else
        {
            canActiveSS = true;
            ss_count.text = "OK";
            ss_add_count.text = monster.ss_add_counter.ToString();
        }
        
    }
}
