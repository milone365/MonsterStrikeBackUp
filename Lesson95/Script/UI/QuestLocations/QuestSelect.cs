using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSelect : MonoBehaviour
{
    public List<LocationData> events = new List<LocationData>();
    //normal
    public List<LocationData> normal = new List<LocationData>();
    //challenge
    public List<LocationData> challenge = new List<LocationData>();
    //event
    List<LocationData> currentLocations = new List<LocationData>();
    Location[] locationsObject = null;
    [SerializeField]
    Animator questMover;
    Transform mover;
    int rotationAmount = 0;
    bool Moving = false;
    [SerializeField]
    float rotationSpeed = 2;
    int newRotationZ=0;
    int current_index;

    private void Start()
    {
        locationsObject = GetComponentsInChildren<Location>();
        currentLocations = events;
        UpdateLocations();
        mover = questMover.transform;
        if(currentLocations.Count==0)
        {
            rotationAmount = 0;
        }
        else
        {
            rotationAmount = 360 / currentLocations.Count;
        }
    }

    private void Update()
    {
        for(int i=0;i<locationsObject.Length;i++)
        {
            float z = questMover.rootRotation.z;
            locationsObject[i].transform.rotation = Quaternion.Euler(0, 0, z);
        }
        if (Moving)
        {
            Quaternion newRotation = Quaternion.Euler(0, 0, newRotationZ);
            mover.rotation = Quaternion.Slerp(mover.rotation, newRotation, rotationSpeed * Time.deltaTime);
            if (mover.rotation == newRotation)
            {
                Moving = false;
                GameObject g = currentLocations[current_index].menuToOpen;
                if(g!=null)
                {
                    Menu.instance.navi.SetOutPages(this.gameObject, g);
                }

            }
        }
    }
    private void UpdateLocations()
    {
            if (currentLocations.Count < 1) return;

            for (int i = 0; i < locationsObject.Length; i++)
            {
                locationsObject[i].gameObject.SetActive(false);
                if (i < currentLocations.Count)
            {
                LocationData data = currentLocations[i];

                if (data != null)
                {
                    locationsObject[i].Set(data.spirte, data.locationName,i,this);
                    locationsObject[i].gameObject.SetActive(true);
                }
            }

        }
    }

    public void RotateAt(int index)
    {
        current_index = index;
        newRotationZ = rotationAmount * index;
        Moving = true;
    }
}

