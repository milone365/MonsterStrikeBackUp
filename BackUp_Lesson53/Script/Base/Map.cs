using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class Map : MonoBehaviour
{
    [SerializeField]
    Transform[] lines = null;
    [SerializeField]
    List<Transform> node_point = new List<Transform>();
    [SerializeField]
    List<string> mapToText = new List<string>();
    List<GameObject> object_database = new List<GameObject>();
    [SerializeField]
    List<MapPhase> phases = new List<MapPhase>();
    int phaseIndex = 0;
    bool lastPhase = false;
    List<GameObject> spawnedOBJ = new List<GameObject>();
    [SerializeField]
    GameObject enemy = null;
    List<EnemyData> enemy_database = new List<EnemyData>();
    public List<Vector2> warp_points = new List<Vector2>();
    List<Enemy> enemyList = new List<Enemy>();
    [SerializeField]
    GameObject BossPanel = null;
    bool bossEntry = false;

    public List<Enemy>GetEnemies()
    {
        return GetComponentsInChildren<Enemy>().ToList();
    }


    // Start is called before the first frame update
    void Start()
    {
        object_database = Resources.LoadAll<GameObject>("SpawnPrefab").ToList();
        enemy_database = Resources.LoadAll<EnemyData>("Enemy").ToList();
        foreach(var line in lines)
        {
            List<Transform> childs = line.GetComponentsInChildren<Transform>().ToList();
            childs.Remove(line);
            foreach(var c in childs)
            {
                node_point.Add(c);
            }
        }
        //test
        LoadMap("Sample", "Map");
        ActivateAllEnemy();
    }

    public void LoadMap(string folder, string filename)
    {
        string filepath = Application.streamingAssetsPath + "/" + folder + "/" + filename + ".txt";
        string[] read = File.ReadAllLines(filepath);
        for(int i=0;i<read.Length;i++)
        {
            bool mapEnd = false;
            if(i==(read.Length-1))
            {
                mapEnd = true;
            }
            if(string.IsNullOrEmpty(read[i]))
            {
                mapEnd = true;
            }
            else
            {
                string[] chars = read[i].Split(',');
                mapToText.AddRange(chars);
            }
            if(mapEnd)
            {
                MapPhase p = new MapPhase();
                p.mapdata.AddRange(mapToText);
                mapToText.Clear();
                phases.Add(p);
            }
        }
        ScrollMap();
    }
    

    public void SpawnObject(string s,int pos)
    {
        string convert = Helper.MapElementToString(s);
        foreach(var item in object_database)
        {
            if(item.name==convert)
            {
                GameObject g = Instantiate(item, transform);
                g.transform.position = node_point[pos].position;
                spawnedOBJ.Add(g);
            }
        }
        //enemy spawn
        foreach(var item in enemy_database)
        {
            if(item.ID==convert)
            {
                GameObject g = Instantiate(enemy,node_point[pos]);
                g.transform.localPosition = Vector2.zero;
                spawnedOBJ.Add(g);
                Enemy e = g.GetComponent<Enemy>();
                if(e!=null)
                {
                    e.INIT(item);
                    enemyList.Add(e);
                    if(e.data.type==EnemyType.lastBoss)
                    {
                        bossEntry = true;
                        StageManager.instance.boss = e;
                    }
                }
            }
        }
        if(s==Helper.Warp)
        {
            warp_points.Add(node_point[pos].position);
        }
    }

    public void GoToNextPhase()
    {
        if(lastPhase)
        {
            Debug.Log("GAME END");
        }
        else
        {
            phaseIndex++;
            //index check
            if(phaseIndex>=phases.Count-1)
            {
                lastPhase = true;
            }
            ScrollMap();
            StartCoroutine(EndTurn());
        }
    }

    void ScrollMap()
    {
        ClearMap();
        mapToText = phases[phaseIndex].mapdata;
        for (int i = 0; i < node_point.Count; i++)
        {
            if (mapToText[i] != "00")
            {
                SpawnObject(mapToText[i], i);
            }
        }
    }

    IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(1);
        StageManager.instance.getWallAnim().SetBool("OFF",false);
        if(bossEntry && BossPanel!=null)
        {
            BossPanel.SetActive(true);
            yield return new WaitForSeconds(3);
            Destroy(BossPanel);
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
        }

        ActivateAllEnemy();
        StageManager.instance.ScrollField.SetBool("UP",false);
        StageManager.instance.ChangeTurn();
    }

    void ClearMap()
    {
        foreach(var item in spawnedOBJ)
        {
            Destroy(item);
        }
        spawnedOBJ.Clear();
        warp_points.Clear();
        enemyList.Clear();
    }

    void ActivateAllEnemy()
    {
        foreach(var item in enemyList)
        {
            item.gameObject.SetActive(true);
        }
    }
}

[System.Serializable]
public class MapPhase
{
    public List<string> mapdata = new List<string>();
}