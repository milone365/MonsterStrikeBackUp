using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewQuest",menuName ="Quest/Sample")]
public class QuestData : ScriptableObject
{
    public string QuestName = "";
    public string MapName="Map";
    public Sprite questImage;
    [Range(1,5)]
    public int RequardOrb;
    public string description;
    public int Time;

}
