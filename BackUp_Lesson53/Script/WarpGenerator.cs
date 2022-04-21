using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGenerator : MonoBehaviour
{
    bool generated = false;
    List<Transform> warp_trans = new List<Transform>();
    List<Vector2> destinations = new List<Vector2>();
    [SerializeField]
    float velocity = 1;
    [SerializeField]
    float min_dist = 0.1f;

    private void Start()
    {
        Invoke("INIT",2);
    }
    public void INIT()
    {
        GameObject warp = Resources.Load<GameObject>("SpawnPrefab/Warp");
        destinations = StageManager.instance.map.warp_points;
        int half = destinations.Count / 2;
        
        for(int i=0;i<half;i++)
        {
            GameObject g = Instantiate(warp, transform);
            g.transform.localPosition = Vector2.zero;
            Transform w0 = g.transform.GetChild(0);
            Transform w1= g.transform.GetChild(1);
            warp_trans.Add(w0);
            warp_trans.Add(w1);
        }
        generated = true;
    }

    private void Update()
    {
        if (!generated) return;
        if (warp_trans.Count < 1) return;
        for (int i = 0; i < destinations.Count; i++)
        {
            float distance = Vector2.Distance(warp_trans[i].position, destinations[i]);
            if(distance>min_dist)
              warp_trans[i].position =Vector2.MoveTowards(warp_trans[i].position, destinations[i],velocity*Time.deltaTime);

        }
    }
}
