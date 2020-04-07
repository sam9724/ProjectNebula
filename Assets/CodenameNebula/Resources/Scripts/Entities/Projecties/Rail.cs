using UnityEngine;
using System.Collections;

public class Rail : Projectile
{
    float railDamage = 5;
    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
    }

    public override void PhysicsRefresh()
    {
        base.PhysicsRefresh();
    }

    public override void PostInitialize()
    {
        base.PostInitialize();
    }

    public override void Refresh(float dt)
    {
        base.Refresh(dt);
    }

    protected override void HitTarget(IDamagable targetHit, string layerName)
    {
        targetHit.TakeDamage(railDamage);
    }
}
