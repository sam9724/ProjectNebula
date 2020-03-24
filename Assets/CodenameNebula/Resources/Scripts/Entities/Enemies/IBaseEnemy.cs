using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IBaseEnemy : IBaseCharacter, IDamagable
{
    bool ChasePlayer { get; set; }
    bool Wander { get; set; }

    //Base Enemy Logic
}

