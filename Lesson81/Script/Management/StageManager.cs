using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public GameTurn playerTurn = new GameTurn();
    public GameTurn pcTurn = new GameTurn();
    public Map map;
    GameTurn current_turn = null;
    bool gameEnd = false;
    [SerializeField]
    GameObject wall = null;
    Monster currentMonster;
    public Animator getWallAnim() { return wall.GetComponent<Animator>(); }

    public void GameOver()
    {
        gameEnd = true;
    }
    public void GameClear()
    {
        gameEnd = true;
        UI_Manager.instance.stageClear.SetActive(true);
    }
    public bool isPlayerTurn()
    {
        return current_turn == playerTurn;
    }
    public Animator ScrollField;
    public Enemy boss;
    [SerializeField]
    PlayerController player;

    public Enemy NearestEnemy(Transform target)
    {
        Enemy e = null;
        List<Enemy> enemies = map.GetEnemies();
        if(enemies.Count>0)
         e = enemies.OrderBy(x => (Vector3.Distance(x.transform.position, target.position))).First();

        return e;
    }
   
    float whaiting = 5;

    private void Awake()
    {
        instance = this;
        current_turn = playerTurn;
        if(UI_Manager.instance!=null)
        UI_Manager.instance.SetEnemyBar();
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
        if (gameEnd)
        {
            //grafic
            
            return;
        }
        StartCoroutine(EndTurnCo());
    }

    IEnumerator EndTurnCo()
    {
            foreach (var item in current_turn.end_action_List)
            {
               item.Invoke();
               yield return new WaitForSeconds(item.action_time);
            }
            current_turn.end_action_List.Clear();    
            if(boss!=null && boss.ISDEATH())
             {
                wall.GetComponent<Animator>().SetBool("OFF", true);
                ScrollField.SetBool("UP", true);
                map.GoToNextPhase();
             }
            else
            {
            if (isPlayerTurn())
            {
                current_turn = pcTurn;
                PlayerController.instance.OnEndTurn();
                StartCoroutine(PcTurnUpdate());
            }
            else
            {
                current_turn = playerTurn;
                UI_Manager.instance.ShowPlayerTurn(true);
                yield return new WaitForSeconds(1);
                player.TurnMantenance();
                UI_Manager.instance.ShowPlayerTurn(false);
            }
        }
        UI_Manager.instance.SetEnemyBar();
    }

    IEnumerator PcTurnUpdate()
    {
        if(map.GetEnemies().Count>0)
        {
            for (int i = 0; i < pcTurn.per_start_action_List.Count; i++)
            {
                Action a = pcTurn.per_start_action_List[i];
                a.Invoke();
                float time = a.action_time;
                yield return new WaitForSeconds(time);
            }
            ChangeTurn();
        }
        else
        {
            wall.GetComponent<Animator>().SetBool("OFF", true);
            ScrollField.SetBool("UP", true);
            map.GoToNextPhase();
        }
    }

    public IEnumerator LaunchCO(Vector2 d, float in_speed, float m ,Monster currentMonster)
    {

        UI_Manager.instance.ShowSSScreen(true, currentMonster.get_data().image);
        yield return new WaitForSeconds(1.5f);
        UI_Manager.instance.ShowSSScreen(false);

        this.currentMonster = currentMonster;
        for(int i=0;i<playerTurn.Launch_action_List.Count;i++)
        {
            Action a = playerTurn.Launch_action_List[i];
            float time = a.action_time;
            a.Invoke();
            yield return new WaitForSeconds(time);
        }
        playerTurn.Launch_action_List.Clear();

        if (in_speed <currentMonster.GetMaxSpeed())
        {
            in_speed = currentMonster.GetMaxSpeed();
        }
        currentMonster.Launch(d, in_speed, m);
    }
}

