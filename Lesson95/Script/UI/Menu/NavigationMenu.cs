using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationMenu : MonoBehaviour
{
    [SerializeField]
    GameObject MonsterPage = null,ShopPage=null,GachaPage=null,FriendPage=null,OtherPage=null;
    public List<GameObject> OpenPages = new List<GameObject>();
    public GameObject beforePG=null, currentPG=null;
    Menu menu;
    public GameObject objectToDestroy;
    public void Init(Menu menu)
    {
        this.menu = menu;
    }
    public void Show(PageGroup page)
    {
        GameObject pg = GetPage(page);
        pg.SetActive(true);
        beforePG = pg;
        currentPG = null;
    }
    GameObject GetPage(PageGroup page)
    {
        GameObject g = null;
        switch (page)
        {
            case PageGroup.MonsterPage:
                g= MonsterPage;
                break;
            case PageGroup.ShopPage:
                g = ShopPage;
                break;
            case PageGroup.GachaPage:
                g = GachaPage;
                break;
            case PageGroup.FriendPage:
                g = FriendPage;
                break;
            case PageGroup.OtherPage:
                g = OtherPage;
                break;
        }
        return g;
    }
    public void CloseAll()
    {
        foreach(var item in OpenPages)
        {
            item.SetActive(false);
        }
        OpenPages.Clear();
        MonsterPage.SetActive(false);
        ShopPage.SetActive(false);
        GachaPage.SetActive(false);
        FriendPage.SetActive(false);
        OtherPage.SetActive(false);
        Menu.instance.home.gameObject.SetActive(false);
        Menu.instance.QuestSelectPage.SetActive(false);
    }

    public void OpenPage(GameObject g)
    {
        bool exist = false;
        currentPG = g;
        foreach(var item in OpenPages)
        {
            if (item == g)
                exist = true;
        }
        if (!exist)
            OpenPages.Add(g);
    }

    public void GoBack()
    {
        if(beforePG==null)
        {
            return;
        }
        if(currentPG==null)
        {
            beforePG = null;
            currentPG = null;
            menu.GoToHome();
        }
        else
        {
            currentPG.SetActive(false);
            beforePG.SetActive(true);
            currentPG = null;
        }
        if(objectToDestroy!=null)
        {
            Destroy(objectToDestroy);
        }
        menu.GetBackButton().SetActive(false);
    }

    public void SetOutPages(GameObject before,GameObject current)
    {
        this.beforePG = before;
        currentPG = current;
        beforePG.SetActive(false);
        currentPG.SetActive(true);
        OpenPage(currentPG);
        menu.GetBackButton().SetActive(true);
    }
}
