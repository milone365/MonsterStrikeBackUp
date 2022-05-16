using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerDataUpdate : MonoBehaviour
{
    [SerializeField]
    Text stamina=null, playertitle = null, playername=null;
    [SerializeField]
    Slider staminaBar = null;
    [Header("Rank")]
    [SerializeField]
    Image rankFillImage = null;
    [SerializeField]
    Text rankText = null,expPoint=null;
    [Header("Orb")]
    [SerializeField]
    Text questObrCounter = null;
    [SerializeField]
    Text orbCounter = null;
    [SerializeField]
    Text coin = null;
    PlayerData data;
    int current_Stamina = 0;
    [SerializeField]
    int nextRankPoint()
    {
        if (data == null) return 1000;

        int temp = 0;
        temp = data.rank * 1000;
        return temp;
    }

    public void UpdateData(PlayerData data)
    {
        this.data = data;
        stamina.text =current_Stamina + "/" + data.stamina.ToString();
        playertitle.text = data.title;
        playername.text = data.playerName;
        //
        staminaBar.maxValue = data.stamina;
        staminaBar.value = current_Stamina;
        //
        rankText.text = data.rank.ToString();
        //ato
        expPoint.text = "more " + (nextRankPoint() - data.currentPoint);
        questObrCounter.text = data.questOrbCount.ToString();
        orbCounter.text = data.orbCount.ToString();
        coin.text = data.coin.ToString();
        //FillImage
        if(data.currentPoint==0)
        {
            rankFillImage.fillAmount = 0;
        }
        else
        {
            rankFillImage.fillAmount = (float)data.currentPoint/(float)nextRankPoint();
        }

    }
}
