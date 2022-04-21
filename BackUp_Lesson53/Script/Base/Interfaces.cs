using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface StrikeShot
{
    void Activate(Transform target = null);
}

public interface IResetable
{
    void Reset();
}

public interface IDirectDamage
{
    Entity monster { get; set; }
    Entity enemy { get; set; }
    ComboBase combo { get; set; }
}