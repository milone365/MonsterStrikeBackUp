using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public PlayerData data;
    public MonsterGestView monsterGestion = null;
    public static Menu instance;
    PlayerDataUpdate playerUpdate;
    [SerializeField]
    GameObject NavigationMenu = null;
    public NavigationMenu navi;
    [SerializeField]
    GameObject BackButton = null;
    public GameObject GetBackButton() { return BackButton; }
    [SerializeField]
    GameObject TeamPage = null;
    public Home home = null;
    public GameObject QuestSelectPage = null;
    [SerializeField]
    bool showMonsters = false;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if(showMonsters)
         home.gameObject.SetActive(true);
        playerUpdate = GetComponent<PlayerDataUpdate>();
        playerUpdate.UpdateData(data);
        navi = NavigationMenu.GetComponent<NavigationMenu>();
        navi.Init(this);
        BackButton.SetActive(false);
    }
    public void MonsterGestionPage(ViewBoxType v)
    {
        if (TeamPage != null)
        {
            if (TeamPage.activeInHierarchy)
            {
                navi.beforePG = TeamPage;
            }
        }
        navi.CloseAll();
        monsterGestion.gameObject.SetActive(true);
        monsterGestion.BuildView(v);
        navi.OpenPage(monsterGestion.gameObject);
    }

    public void GoToHome()
    {
        navi.CloseAll();
        NavigationMenu.SetActive(false);
        BackButton.SetActive(false);
        home.gameObject.SetActive(true);
    }
    public void OpenMonsterMenu()
    {
        navi.CloseAll();
        NavigationMenu.SetActive(true);
        navi.Show(PageGroup.MonsterPage);
        BackButton.SetActive(true);
    }
    public void OpenShop()
    {
        navi.CloseAll();
        NavigationMenu.SetActive(true);
        navi.Show(PageGroup.ShopPage);
        BackButton.SetActive(true);
    }
    public void OpenGacha()
    {
        navi.CloseAll();
        NavigationMenu.SetActive(true);
        navi.Show(PageGroup.GachaPage);
        BackButton.SetActive(true);
    }
    public void OperFriend()
    {
        navi.CloseAll();
        NavigationMenu.SetActive(true);
        navi.Show(PageGroup.FriendPage);
        BackButton.SetActive(true);
    }
    public void OpenOther()
    {
        navi.CloseAll();
        NavigationMenu.SetActive(true);
        navi.Show(PageGroup.OtherPage);
        BackButton.SetActive(true);
    }
   
    public void OpenQuest()
    {
        navi.CloseAll();
        NavigationMenu.SetActive(false);
        BackButton.SetActive(false);
        QuestSelectPage.SetActive(true);
    }

}
