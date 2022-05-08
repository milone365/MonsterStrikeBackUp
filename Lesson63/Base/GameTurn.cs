using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameTurn 
{
   
    public  List<Action> end_action_List = new List<Action>();
    public List<Action> Launch_action_List = new List<Action>();
    public List<Action> start_action_List = new List<Action>();
    public List<Action> event_action_List = new List<Action>();

    //permanent action
    public List<Action> per_end_action_List = new List<Action>();
    public List<Action> per_start_action_List = new List<Action>();
}

[System.Serializable]
public class Action
{
    public delegate void ActionUpdate();
    ActionUpdate current_Action;
    public float action_time = 0;

    public Action(ActionUpdate a,float time=0)
    {
        current_Action = a;
        action_time = time;
    }
    public void SetActionUpdate(ActionUpdate a,float time = 0)
    {
        current_Action = a;
    }

    public void Invoke()
    {
        if (current_Action != null)
            current_Action();
    }
}