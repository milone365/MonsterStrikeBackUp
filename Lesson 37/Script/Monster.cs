using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    #region Values
    public Transform arrow_R;
    public HealthManager getHealthManager()
    {
        return GetComponent<HealthManager>();
    }
    
    [SerializeField]
    Transform Root = null;
    public float GetMaxSpeed()
    {
        return maxspeed;
    }
    float speed;
    float maxspeed;
    public float GetRunningSpeed() { return speed; }
    public int ss_add_counter = 0;
    bool ss_Active = false;
    [SerializeField]
    MonsterData data = null;
    [SerializeField]
    Vector2 launchDirection = new Vector2();
    Rigidbody2D rb;
    public Vector2 current_direction = new Vector2();
    const float decelleration = 50;
    bool Move = false;
    Animator anim;
    const float hit_decelleration= 0.5f;
    float multipler = 1;
    #endregion
    public MonsterData get_data() { return data; }
    Vector2 saved_velocity=new Vector2();
    public Animator GetAnimator() { return anim; }
    List<ComboBase> combos = new List<ComboBase>();
    public HitController getHitController() { return GetComponent<HitController>(); }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    
    public void SetMonster(MonsterData in_data)
    {
        data = in_data;
        getHealthManager().INIT(this, level);
        maxatk = in_data.atk * level;
        atk = maxatk;
        maxspeed = in_data.speed * level;
        speed = maxspeed;
        rend.sprite = in_data.image;
        rend.transform.localScale = in_data.default_size;
        ss_counter = data.ss_turn;
        ss_add_counter = data.ss_add_turn;
        GetComponent<HitController>().SetType(in_data.group.shot_type);
        SpawnElements();
        GetComponent<HitController>().INIT(this, rend);
    }

    void SpawnElements()
    {
        foreach(var item in data.friendCombo)
        {
            GameObject g= Instantiate(item, Root);
            g.transform.localPosition = Vector3.zero;
            ComboBase combo = g.GetComponent<ComboBase>();
            if (combo != null)
            {
                combo.INIT(this);
                combos.Add(combo);
            }
                
        }
        if(data.strikeshotPrefab!=null)
        Instantiate(data.strikeshotPrefab, Root);
    }

    protected override void On_Reset()
    {
        foreach (var item in combos)
        {
            item.canActive = true;
        }
    }

    public void Launch(Vector2 d,float in_speed,float m)
    {
        current_direction = d;
        speed = in_speed;
        Move = true;
        anim.SetBool("Move", Move);
        multipler = m;
        getHitController().CanMakeDamage = true;
    }

    private void Update()
    {

        //optional
        if (!Move)
        {
            return;
        }
        

        rb.velocity=current_direction;
        rb.velocity *= speed*multipler * Time.deltaTime;

        speed -= decelleration * Time.deltaTime;
        if (speed<=0)
        {
            speed = 0;
            Move = false;
            anim.SetBool("Move", Move);
            PlayerController.instance.StopAllMonsters();
            PlayerController.instance.GetStage().ChangeTurn();
        }
        
    }


    public void Decellate()
    {
        Vector2 lostForce_vector = current_direction.normalized * hit_decelleration;
        current_direction = (current_direction - lostForce_vector);
    }

    public void Stop()
    {
        Move = false;
        saved_velocity = rb.velocity;
        rb.velocity = Vector2.zero; 
    }

    public void RestoreVelocity()
    {
        rb.velocity = saved_velocity;
        Move = true;
    }

 

    public void UseCombo()
    {
        foreach(var item in combos)
        {
            if(item.canActive)
                item.Activate();
        }
    }
}
