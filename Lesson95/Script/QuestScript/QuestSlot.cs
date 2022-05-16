using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestSlot : MonoBehaviour
{
    QuestData data;
    Animator anim;
    MonsterViewBox box;
    bool selected = false;
    Events eventsClass;
    [SerializeField]
    GameObject subQuest = null;

   public void Initialize(QuestData data,Events eventclass)
    {
        this.data = data;
        anim = GetComponent<Animator>();
        box = GetComponentInChildren<MonsterViewBox>();
        box.InitQuest(data,this);
        eventsClass = eventclass;
        Button b= subQuest.transform.GetChild(0).GetComponent<Button>();
        if(b!=null)
        {
            b.onClick.AddListener(OpenGameType);
        }
    }

    void OpenGameType()
    {
        eventsClass.OpenAccess();
    }
    public void Selected()
    {
        selected = !selected;
        if(selected)
        {
            eventsClass.SetCurrentQuest(data);
            subQuest.SetActive(true);
            anim.Play("QuestSlotShowMarker");
        }
        else
        {
            anim.Play("Empty");
            eventsClass.ShowAllQuest();
            subQuest.SetActive(false);
        }
    }
}
