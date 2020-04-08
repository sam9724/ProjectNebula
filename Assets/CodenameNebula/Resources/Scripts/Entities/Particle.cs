using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour, IPoolable
{
    public ParticleFactory.ParticleType partType;

    public void Initialize()
    {
        
    }

    public void PhysicsRefresh(float fdt)
    {
        
    }

    public void PostInitialize()
    {
        
    }

    public void Refresh(float dt)
    {
        
    }

    public void Died()
    {
        gameObject.SetActive(false);
        ParticleManager.Instance.ParticleDied(this);
    }

    public void OnParticleSystemStopped()
    {
        Died();
    }
}
