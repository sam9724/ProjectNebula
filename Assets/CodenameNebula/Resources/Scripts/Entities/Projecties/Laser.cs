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

    protected override void Explode()
    {
        ParticleFactory.Instance.CreateParticle(ParticleFactory.ParticleType.LaserExplosion, transform.position, transform.rotation);
    }

    protected override void HitTarget(IDamagable targetHit, string layerName)
    {
        if (layerName == "Player" || layerName == "Asteroid")
            targetHit.TakeDamage(laserDamage);
    }
}
