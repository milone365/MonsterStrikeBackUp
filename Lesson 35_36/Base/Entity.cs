using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IResetable
{
    [SerializeField]
    protected int level = 1;
    [SerializeField]
    protected float atk=1;
    //wakuwaku min
    public List<int> power_Bonus = new List<int>();
    public List<float> speed_Bonus = new List<float>();
    public List<int> defence_Bonus = new List<int>();
    //
    public List<int> temp_power_Bonus = new List<int>();
    public List<float> temp_speed_Bonus = new List<float>();
    public List<int> temp_defence_Bonus = new List<int>();
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

    public void Reset()
    {
        temp_defence_Bonus.Clear();
        temp_power_Bonus.Clear();
        temp_defence_Bonus.Clear();
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
}
