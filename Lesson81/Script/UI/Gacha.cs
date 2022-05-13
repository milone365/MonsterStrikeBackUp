using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    public string Gacha_name = "Path";
    public string Rare_name = "Path";
    MonsterData[] allmonsters = null;
    MonsterData[] rareMonsters = null;
    [SerializeField]
    int dropRate = 98;
    List<DroppedMonster> droppedMonsters = new List<DroppedMonster>();
    [SerializeField]
    Transform egg_parent = null;
    [SerializeField]
    GameObject egg = null;
     
    private void Start()
    {
        BuildGacha(Gacha_name,Rare_name);
    }
    public void BuildGacha(string gachaname,string rarename)
    {
        allmonsters = null;
        rareMonsters = null;
        allmonsters = Resources.LoadAll<MonsterData>("Gacha/"+ gachaname);
        rareMonsters = Resources.LoadAll<MonsterData>("Gacha/" + rarename);
    }

    public IEnumerator Show(int value)
    {
        for (int i = 0; i < value; i++)
        {
            int RareDrop = Random.Range(0, 100);
            MonsterData drop = null;
            if (RareDrop >= dropRate)
            {
                int rand = Random.Range(0, rareMonsters.Length);
                drop = rareMonsters[rand];
            }
            else
            {
                int rand = Random.Range(0, allmonsters.Length);
                drop = allmonsters[rand];
            }

            ScriptableObjectManager.CreateMonster<MonsterData>(drop);
            GameObject newegg = Instantiate(egg, egg_parent);
            DroppedMonster newDrop = new DroppedMonster(drop, newegg.transform);
            droppedMonsters.Add(newDrop);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1.5f);
        foreach (var item in droppedMonsters)
        {
            Egg egg = item.egg.GetComponent<Egg>();
            egg.PlayAnnimation(item.data);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void Round(int value)
    {
        StartCoroutine(Show(value));
    }
}

[System.Serializable]
public class DroppedMonster
{
    public Transform egg;
    public MonsterData data;
    public DroppedMonster(MonsterData data,Transform egg)
    {
        this.data = data;
        this.egg = egg;
    }
}