using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ParticleManager : GenericSetManager<Particle>
{
    #region Singleton
    private static ParticleManager instance;
    private ParticleManager() { }
    public static ParticleManager Instance { get { return instance ?? (instance = new ParticleManager()); } }
    #endregion

    public Transform partParent;
    
    public Transform spawnLocation;
    public Vector3 spawnDirection;

    public Transform pooledPartParent;


    public override void Initialize()
    {
        base.Initialize();
        partParent = new GameObject("PartParent").transform;
        pooledPartParent = new GameObject("DeadPart").transform;
    }

    public override void PostInitialize()
    {
        base.PostInitialize();
    }


    public override void Refresh(float dt)
    {
        base.Refresh(dt);
    }


    public override void PhysicsRefresh(float dt)
    {
        base.PhysicsRefresh(dt);
    }

    public void ParticleDied(Particle p)
    {
        toRemove.Push(p);
        p.transform.SetParent(pooledPartParent);
        GenericObjectPool.Instance.PoolObject(p.partType.GetType(), p);
    }
}
