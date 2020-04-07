using UnityEngine;
using System.Collections;

public interface IShielded
{

    // Use this for initialization
    void TakeShieldDamage(float damage);

    void BreakShield();

    void RegenShield(float dt);
}
