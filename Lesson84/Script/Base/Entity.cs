using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IResetable
{
    public int level = 1;
    [SerializeField]
    protected float atk=1;
    //wakuwaku min
    public List<int> power_Bonus = new List<int>();
    public List<float> speed_Bonus = new List<float>();
    public List<int> defence_Bonus = new List<int>();
    //
    public List<Status> temp_power_Bonus = new List<Status>();
    public List<Status> temp_speed_Bonus = new List<Status>();
    public List<Status> temp_defence_Bonus = new List<Status>();
    public List<Status> temp_combo_Bonus = new List<Status>();

    //malus
    public List<Status> temp_power_Malus = new List<Status>();
    public List<Status> temp_speed_Malus = new List<Status>();
    public List<Status> temp_defence_Malus = new List<Status>();

    bool ss_canBeActive = false;
    public int ss_counter = 0;
    public int GetMaxAtk()
    {
        return maxatk;
    }
    protected int maxatk;

    [SerializeField]
    protected SpriteRenderer rend = null;
    public SpriteRenderer getSprite() { return rend; }
    public Collider2D getCollider() { return GetComponent<Collider2D>(); }
    public StatsBoard board = null;

    public void Reset()
    {
        StatusMantenance(temp_combo_Bonus);
        StatusMantenance(temp_defence_Bonus);
        StatusMantenance(temp_power_Bonus);
        StatusMantenance(temp_speed_Bonus);
        StatusMantenance(temp_defence_Malus, false);
        StatusMantenance(temp_power_Malus, false);
        StatusMantenance(temp_speed_Malus, false);
        On_Reset();
    }

 
    protected virtual void On_Reset()
    {

    }
    public void OnTurnStart()
    {
        EraseSS();
    }
    public void EraseSS()
    {
        ss_counter--;
        if (ss_counter <= 0)
        {
            ss_counter = 0;
            ss_canBeActive = true;
        }
        else
        {
            ss_canBeActive = false;
        }
    }
    public virtual void TakeDamage(int dmg)
    {
       
    }

    public void AddTempBonus(Stats stat, int a,int t=1)
    {
        Status newBonus = new Status(a,t,stat);
        switch (stat)
        {
            case Stats.Power:
                temp_power_Bonus.Add(newBonus);
                if(temp_power_Malus.Count>0)
                {
                    temp_power_Malus.Clear();
                }
                break;
            case Stats.Speed:
                temp_speed_Bonus.Add(newBonus);
                if (temp_speed_Malus.Count >0)
                {
                    temp_speed_Malus.Clear();
                }
                break;
            case Stats.Defence:
                temp_defence_Bonus.Add(newBonus);
                if (temp_defence_Malus.Count >0)
                {
                    temp_defence_Malus.Clear();
                }
                break;
            case Stats.Combo:
                temp_combo_Bonus.Add(newBonus);
                break;
        }
        board.Show(true, stat);
    }

    public void AddTempMalus(Stats stat,int value,int turn)
    {

        Status newBonus = new Status(value,turn,stat);

        switch (stat)
        {
            case Stats.Power:
                temp_power_Malus.Add(newBonus);
                if (temp_power_Bonus.Count > 0)
                {
                    temp_power_Bonus.Clear();
                }
                break;
            case Stats.Speed:
                temp_speed_Malus.Add(newBonus);
                if (temp_speed_Bonus.Count > 0)
                {
                    temp_speed_Bonus.Clear();
                }
                break;
            case Stats.Defence:
                temp_defence_Malus.Add(newBonus);
                if (temp_defence_Bonus.Count > 0)
                {
                    temp_defence_Bonus.Clear();
                }
                break;
        }
        board.Show(true, stat,false);
    }
    void StatusMantenance(List<Status> bonus,bool is_bonus=true)
    {
        if(bonus.Count<1)
        {
            return;
        }
        Stats stat=Stats.Combo;
        for(int i=0;i<bonus.Count;i++)
        {
            stat = bonus[i].stat;
            bonus[i].turn--;
            if(bonus[i].turn<=0)
            {
                bonus.Remove(bonus[i]);
            }
        }
        if(bonus.Count<1)
        {
            bonus.Clear();
            board.Show(false,stat,is_bonus);
        }
    }
}

[System.Serializable]
public class Status
{
    public int amount;
    public int turn;
    public Stats stat;

    public Status() { }

    public Status(int amount,int turn, Stats stat)
    {
        this.amount = amount;
        this.turn = turn;
        this.stat = stat;
    }
}