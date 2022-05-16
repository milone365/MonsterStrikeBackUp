using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField]
    Transform [] characters = null;
    Camera cam;
    [SerializeField]
    Transform pivot = null;
    const int rotAmount = 120;
    int rotY = 0;
    bool Moving = false;
    Quaternion newRotation;
    [SerializeField]
    float rotationSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var item in characters)
        {
            item.rotation = Quaternion.LookRotation(cam.transform.position);
            float rotY = item.rotation.y;
            item.rotation = Quaternion.Euler(0, rotY * -1f, 0);
        }
        if(Moving)
        {
            pivot.rotation = Quaternion.Slerp(pivot.rotation, newRotation, rotationSpeed * Time.deltaTime);
            if(pivot.rotation==newRotation)
            {
                Moving = false;
            }
        }
    }

    public void ScrollLeft()
    {
        if (Moving) return;
        rotY -= rotAmount;
        newRotation = Quaternion.Euler(0,rotY,0);
        Moving = true;
    }
    public void ScrollRight()
    {
        if (Moving) return;
        rotY += rotAmount;
        newRotation = Quaternion.Euler(0,rotY, 0);
        Moving = true;
    }

    public void GoToQuestPage()
    {
        Menu.instance.OpenQuest();
        gameObject.SetActive(false);
    }
}
