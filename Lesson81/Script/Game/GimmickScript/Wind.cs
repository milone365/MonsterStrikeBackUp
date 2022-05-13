using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wind : MonoBehaviour
{
    Text turntext;
    [SerializeField]
    bool Actived = false;
    [SerializeField]
    GIMMICK gimmick_type = GIMMICK.block;
    Transform blade;
    [SerializeField]
    float speed = 4;
    [SerializeField]
    float radius = 5;
    [SerializeField]
    List<Monster> monster_inArea = new List<Monster>();
    [SerializeField]
    float attractForce = 5;
    [SerializeField]
    float minDistance = 0.5f;
    [SerializeField]
    float activationTime = 2;
    float counter = 2;
    [SerializeField]
    int actionTurn = 2;
    int actionCounter = 2;
    Action windAction;
    private void Start()
    {
        blade = transform.GetChild(0);
        counter = activationTime;
        windAction = new Action(ACTION);
        windAction.action_time = activationTime;
        StageManager.instance.pcTurn.per_start_action_List.Add(windAction);
        turntext = GetComponentInChildren<Text>();
        actionCounter = actionTurn;
        turntext.text = actionCounter.ToString();
    }

    private void Update()
    {
        if(Actived)
        {
            blade.transform.Rotate(0, 0, speed * Time.deltaTime);
            if(monster_inArea.Count<1)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
                foreach(var item in colliders)
                {
                    if(item.tag==Helper.MONSTER)
                    {
                        monster_inArea.Add(item.GetComponent<Monster>());
                    }
                }
            }
            else
            {
                foreach(var item in monster_inArea)
                {
                    bool HaveAbility = item.getHitController().HaveAbility(gimmick_type);
                    if(!HaveAbility)
                    {
                        float distance = Vector2.Distance(item.transform.position, transform.position);
                        if(distance>minDistance)
                            item.transform.position = Vector3.MoveTowards(item.transform.position, this.transform.position, attractForce * Time.deltaTime);
                    }
                    else
                    {
                        item.getHitController().TouchGimmick(gimmick_type);
                    }
                }
                
            }
            counter -= Time.deltaTime;
            if(counter<0)
            {
                Actived = false;
                counter = activationTime;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void ACTION()
    {
        
        actionCounter--;
        if(actionCounter<=0)
        {
            actionCounter = actionTurn;
            Actived = true;
        }
        turntext.text = actionCounter.ToString();
    }

    private void OnDestroy()
    {
        StageManager.instance.pcTurn.per_start_action_List.Remove(windAction);
    }
}
