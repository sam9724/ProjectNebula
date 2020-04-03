using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ProjectileManager : MonoBehaviour
{
    #region Singleton
    private static ProjectileManager instance;
    private ProjectileManager() { }
    public static ProjectileManager Instance { get { return instance ?? (instance = new ProjectileManager()); } }
    #endregion

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
        projParent = new GameObject("ProjParent").transform;
        pooledBulletParent = new GameObject("DeadProj").transform;
        //prefabDict = new Dictionary<string, GameObject>();
        //prefabDict = Resources.LoadAll<GameObject>("Resources/Projectiles/").ToDictionary(x => x.name, x => x);
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

        //Fire(dt);
    }


    public void PhysicsRefresh(float dt)
    {

    }

    public void ProjectileDied(Projectile p)
    {
        projectiles.Remove(p);
        p.transform.SetParent(pooledBulletParent);
        GenericObjectPool.Instance.PoolObject(p.projType.GetType(), p);
    }
}
