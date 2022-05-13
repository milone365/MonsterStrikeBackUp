using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    [SerializeField]
    Sprite[] elements = null;
    public Sprite frog;
    public Sprite super_frog;
    [SerializeField]
    Text hp_text = null;
    [SerializeField]
    Slider hp_bar = null;
    [SerializeField]
    GameObject SpeedBar = null;
    Slider speed_bar;
    [SerializeField]
    Image speedBarHightPoint = null;
    Color default_color;
    bool bar_active = false;
    [SerializeField]
    Color red, blue, green, yellow, violet;
    [SerializeField]
    List<CharacterBox> characters = new List<CharacterBox>();
    [SerializeField]
    CharacterBox box = null;
    [SerializeField]
    Transform box_parent = null;

    private void Awake()
    {
        instance = this;
        speed_bar = SpeedBar.GetComponentInChildren<Slider>();
        default_color = speedBarHightPoint.color;
        ShowSpeedBar(false);
    }

    public Sprite GetElementSprite(ELEMENT e)
    {
        Sprite s = null;

        switch (e)
        {
            case ELEMENT.fire:
                return elements[0];
                break;
            case ELEMENT.water:
                return elements[1];
                break;
            case ELEMENT.wood:
                return elements[2];
                break;
            case ELEMENT.light:
                return elements[3];
                break;
            case ELEMENT.dark:
                return elements[4];
                break;
            case ELEMENT.none:
                return null;
                break;
        }

        return s;
    }

    public void SetHP(int max, int current)
    {
        hp_bar.maxValue = max;
        hp_bar.value = current;
        hp_text.text = current + "/" + max;
    }

    public void ShowSpeedBar(bool val,float time=0)
    {
        bar_active = val;
        Invoke("ActiveSpeedBar", time);
    }

    void ActiveSpeedBar()
    {
        SpeedBar.gameObject.SetActive(bar_active);
    }

    public void SetSpeedBar(float max,float current)
    {
        speed_bar.maxValue = max;
        speed_bar.value = current;
        if(current>=max*0.98)
        {
            speedBarHightPoint.color =red;
        }
        else
        {
            speedBarHightPoint.color = default_color;
        }
    }

    public Color getColor(ELEMENT element)
    {
        Color c = Color.white;
        switch (element)
        {
            case ELEMENT.fire:
                c= red;
                break;
            case ELEMENT.water:
                c = blue;
                break;
            case ELEMENT.wood:
                c = green;
                break;
            case ELEMENT.light:
                c = yellow;
                break;
            case ELEMENT.dark:
                c = violet;
                break;
        }
        return c;
    }

    public void SetCharacters(List<Monster>monsters)
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(box, box_parent);
        }
        characters = box_parent.GetComponentsInChildren<CharacterBox>().ToList();

        for (int i=0;i<characters.Count;i++)
        {
            characters[i].SetBox(monsters[i]);
        }
    }
}
