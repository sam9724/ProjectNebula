using UnityEngine;
using System.Collections;

public interface IBasePlayer : IBaseCharacter
{

    float ShieldCooldown { get; set; }
    bool StalkEnemy { get; set; }
}
