using UnityEngine;
using System.Collections;

public class Laser : Projectile
{
    float laserDamage = 2;
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
        targetHit.TakeDamage(laserDamage);
    }
}
