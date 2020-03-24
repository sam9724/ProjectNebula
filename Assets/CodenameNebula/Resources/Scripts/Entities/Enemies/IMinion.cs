using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMinion : IBaseEnemy, IPoolable
{
    bool MoveWithBoss { get; set; }
    bool SeekPlayer { get; set; }
}

