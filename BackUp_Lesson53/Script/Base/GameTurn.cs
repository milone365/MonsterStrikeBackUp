using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameTurn 
{
    public delegate void Action();
    public  List<Action> end_action_List = new List<Action>();
    public List<Action> start_action_List = new List<Action>();

    //permanent action
    public List<Action> per_end_action_List = new List<Action>();
    public List<Action> per_start_action_List = new List<Action>();

    public float TIME = 0;

}
