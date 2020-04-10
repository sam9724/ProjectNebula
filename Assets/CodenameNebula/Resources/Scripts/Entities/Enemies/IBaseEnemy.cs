using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IBaseEnemy : IBaseCharacter, IDamagable, IManagable
{
    bool ChasePlayer { get; set; }
    bool Wander { get; set; }

    bool IsAlive { get; set; }

    float MaxHealth { get; set; }

    //Base Enemy Logic
}

