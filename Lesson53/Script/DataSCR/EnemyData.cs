using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public Sprite image;
    public string ID = "00";
    public int hp = 100;
    public int attackPower = 10;
    public int Defence = 10;
    public ELEMENT element;
    public GameObject attack = null;
    public GameObject death_attack = null;
    public int counter = 5;
    public int deathAttackCounter = 10;
    public BossPhase phase=BossPhase.yellow;
    public EnemyType type = EnemyType.mob;
    public Vector2 scale = new Vector2(0.2f, 0.2f);
    public float radius = 0.5f;
}
