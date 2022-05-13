using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStrikeShot : MonoBehaviour
{
    public List<Action> LaunchActions = new List<Action>();
    public List<Action> EndTurnActions = new List<Action>();
    public List<Action> OnEventActions = new List<Action>();
    [SerializeField]
    protected float actionTime = 0.75f;
    public virtual void Activate()
    {
        GameTurn turn = StageManager.instance.playerTurn;

        foreach(var item in LaunchActions)
        {
            turn.Launch_action_List.Add(item);
        }
        foreach (var item in EndTurnActions)
        {
            turn.end_action_List.Add(item);
        }
        foreach (var item in OnEventActions)
        {
            turn.event_action_List.Add(item);
        }
    }

}
