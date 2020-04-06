using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ProjectileManager 
{
    #region Singleton
    private static ProjectileManager instance;
    private ProjectileManager() { }
    public static ProjectileManager Instance { get { return instance ?? (instance = new ProjectileManager()); } }
    #endregion

    public Stack<Projectile> toRemove;
    public Stack<Projectile> toAdd;
    public HashSet<Projectile> projectiles;
    public Transform projParent;
    
    public Transform spawnLocation;
    public Vector3 spawnDirection;

    public Transform pooledBulletParent;

    bool shoot = true;

    float timer = 0;
    float cooldown = 1;

    public void Initialize()
    {
        projectiles = new HashSet<Projectile>();
        toRemove = new Stack<Projectile>();
        toAdd = new Stack<Projectile>();
        projParent = new GameObject("ProjParent").transform;
        pooledBulletParent = new GameObject("DeadProj").transform;
    }

    public void PostInitialize()
    {
        //shoot = true;
    }


    public void Refresh(float dt)
    {
        foreach (Projectile p in projectiles)
        {
            p.Refresh(dt);
        }

        while (toRemove.Count > 0)
            projectiles.Remove(toRemove.Pop());

        while (toAdd.Count > 0)
            projectiles.Add(toAdd.Pop());

        //Fire(dt);
    }


    public void PhysicsRefresh(float dt)
    {

    }

    public void ProjectileDied(Projectile p)
    {
        toRemove.Push(p);
        p.transform.SetParent(pooledBulletParent);
        GenericObjectPool.Instance.PoolObject(p.projType.GetType(), p);
    }
}
