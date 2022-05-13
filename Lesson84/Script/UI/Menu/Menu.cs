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
    [SerializeField]
    GameObject TeamPage = null;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
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
        NavigationMenu.SetActive(false);
        BackButton.SetActive(false);
    }
    public void OpenMonsterMenu()
    {
        NavigationMenu.SetActive(true);
        navi.Show(PageGroup.MonsterPage);
        BackButton.SetActive(true);
    }
    public void OpenShop()
    {
        NavigationMenu.SetActive(true);
        navi.Show(PageGroup.ShopPage);
        BackButton.SetActive(true);
    }
    public void OpenGacha()
    {
        NavigationMenu.SetActive(true);
        navi.Show(PageGroup.GachaPage);
        BackButton.SetActive(true);
    }
    public void OperFriend()
    {
        NavigationMenu.SetActive(true);
        navi.Show(PageGroup.FriendPage);
        BackButton.SetActive(true);
    }
    public void OpenOther()
    {
        NavigationMenu.SetActive(true);
        navi.Show(PageGroup.OtherPage);
        BackButton.SetActive(true);
    }
    
}
