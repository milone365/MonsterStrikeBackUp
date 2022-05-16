using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    [SerializeField]
    QuestData[] allQuest=null;
    [SerializeField]
    Transform content = null;
    [SerializeField]
    GameObject quest_lot = null;
    List<GameObject> spawnedQuest = new List<GameObject>();
    QuestData currentQuest;
    [SerializeField]
    GameObject questAcces = null;
    [SerializeField]
    GameObject questView = null,FriendCharacterSetView=null,LastCharacterSetView=null;


    private void Start()
    {
        allQuest = Resources.LoadAll<QuestData>("Quest/Events");
        for(int i=0;i<allQuest.Length;i++)
        {
            GameObject g = Instantiate(quest_lot, content);
            spawnedQuest.Add(g);
            QuestSlot slot = g.GetComponent<QuestSlot>();
            if(slot!=null)
            {
                slot.Initialize(allQuest[i],this);
            }
        }
    }

    public void SetCurrentQuest(QuestData dat)
    {
        currentQuest = dat;
        foreach(var item in spawnedQuest)
        {
            item.SetActive(false);
        }
        spawnedQuest[GetQuestIndex()].SetActive(true);
    }
    public void ShowAllQuest()
    {
        foreach (var item in spawnedQuest)
        {
            item.SetActive(true);
        }
    }
    int GetQuestIndex()
    {
        if (currentQuest == null) return 0;

        for(int i=0;i<allQuest.Length;i++)
        {
            if(currentQuest==allQuest[i])
            {
                return i;
            }
        }
        return 0;
    }

    public void OpenAccess()
    {
        if(!questAcces.activeInHierarchy)
         questAcces.SetActive(true);
    }
    public void CloseAccess()
    {
        questAcces.SetActive(false);
    }
    public void MakeSingle()
    {
        CloseAccess();
        Menu.instance.navi.SetOutPages(questView,FriendCharacterSetView);

    }
    public void MakeMultiple()
    {
        
        
    }
    public void InitForStartGame()
    {
        Menu.instance.navi.SetOutPages(FriendCharacterSetView, LastCharacterSetView);
    }
    public void StartGame()
    {
        SaveParameters();
        SceneManager.LoadScene(Helper.GameScene);
    }

    public void SaveParameters()
    {

    }
    
}
