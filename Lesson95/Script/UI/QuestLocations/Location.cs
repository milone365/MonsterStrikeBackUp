using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Location : MonoBehaviour
{
    [SerializeField]
    Image icon = null;
    [SerializeField]
    Text LocationName=null;
    int index = 0;
    QuestSelect select;
    Button button;


    private void Start()
    {
        button = GetComponentInChildren<Button>();
        if(button==null)
        {
            Debug.Log("BUTTON NOT FOUND");
            return;
        }
        button.onClick.AddListener(Rotate);
    }
    public void Set(Sprite sprite, string location,int id,QuestSelect select)
    {
        icon.sprite = sprite;
        LocationName.text = location;
        index = id;
        this.select = select;
    }
    
    public void Rotate()
    {
        select.RotateAt(index);
    }
}

[System.Serializable]
public class LocationData
{
    public string locationName;
    public Sprite spirte;
    public GameObject menuToOpen;
}