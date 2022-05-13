using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterPage : MonoBehaviour
{
    [SerializeField]
    Page thisPage;
    [SerializeField]
    GameObject[] Pages = null;
    void Start()
    {
       thisPage.SetUp();
      for(int i=0;i<thisPage.Buttons.Count;i++)
        {
            int var = i;
            thisPage.GetButton(i).onClick.AddListener(delegate { PushButton(var); });
        }
    }

    public void PushButton(int index)
    {
        if (Pages.Length-1 < index) return;
        Pages[index].SetActive(true);
        Menu.instance.navi.OpenPage(Pages[index]);
        gameObject.SetActive(false);
    }
}

[System.Serializable]
public class Page
{
    public List<GameObject> Buttons = new List<GameObject>();
    
    public Button GetButton(int index)
    {
        Button b =null;
        b = Buttons[index].GetComponent<Button>();
        return b;
    }
    public void SetUp()
    {
         for(int i=0;i<Buttons.Count;i++)
        {

            Button b = Buttons[i].GetComponent<Button>();
            if(b==null)
            {
                b = Buttons[i].AddComponent<Button>();
            }
        }
    }

}