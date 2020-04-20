using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ProjectileManager :GenericSetManager<Projectile>
{
    #region Singleton
    private static ProjectileManager instance;
    private ProjectileManager() { }
    public static ProjectileManager Instance { get { return instance ?? (instance = new ProjectileManager()); } }
    #endregion

    public Transform projParent;
    
    public Transform spawnLocation;
    public Vector3 spawnDirection;

    public Transform pooledProjParent;


    public override void Initialize()
    {
        base.Initialize();
        projParent = new GameObject("ProjParent").transform;
        pooledProjParent = new GameObject("DeadProj").transform;
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

    public void ProjectileDied(Projectile p)
    {
        toRemove.Push(p);
        p.transform.SetParent(pooledProjParent);
        GenericObjectPool.Instance.PoolObject(p.projType.GetType(), p);
    }
}
