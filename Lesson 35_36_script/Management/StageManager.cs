using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageManager : MonoBehaviour
{
    Turn current_turn = Turn.Player;
    public bool isPlayerTurn()
    {
        return current_turn == Turn.Player;
    }
    enum Turn
    {
        Player,
        Pc
    }
    [SerializeField]
    PlayerController player;
    public static  List<Enemy> enemies = new List<Enemy>();
    public static Enemy NearestEnemy(Transform target)
    {
        Enemy e = null;
        if(enemies.Count>0)
         e = enemies.OrderBy(x => (Vector3.Distance(x.transform.position, target.position))).First();

        return e;
    }
    public delegate void end_Action();
    static List<end_Action> end_action_List = new List<end_Action>();

    private void Start()
    {
        UpdateEnemy();
    }
    public void ChangeTurn()
    {
        StartCoroutine(EndTurnCo());
    }

    IEnumerator EndTurnCo()
    {
            foreach(var item in end_action_List)
            {
              item.Invoke();
            }
             end_action_List.Clear();

            yield return new WaitForSeconds(1);
            UpdateEnemy();
            if (isPlayerTurn())
            {
                current_turn = Turn.Pc;
                StartCoroutine(PcTurnUpdate());
            }
            else
            {
                current_turn = Turn.Player;
                player.TurnMantenance();
            }
        
    }

    IEnumerator PcTurnUpdate()
    {
        yield return new WaitForSeconds(1);
        ChangeTurn();
    }
    void UpdateEnemy()
    {
        enemies.Clear();
        enemies = GetComponentsInChildren<Enemy>().ToList();
    }

    public static void AddEndAction(end_Action a)
    {
        end_action_List.Add(a);
    }
}

