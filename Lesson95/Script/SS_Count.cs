using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_Count : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    CharacterBox characterBox=null;
    private void Start()
    {
        anim = GetComponent<Animator>();
        characterBox.ssCountObj = this;
    }

    public void ShowAnimation()
    {
        anim.Play("SS_CountShow");
    }

    public void Update_SSCount()
    {
        characterBox.UpdateSSCount();
    }
}
