using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField]
    MonsterData data=null;
    [SerializeField]
    SpriteRenderer rend;
    private void Start()
    {
        rend.sprite = data.image;
        rend.transform.localScale = data.default_size;
    }


}
