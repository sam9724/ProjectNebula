﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ProjectileFactory : MonoBehaviour
{

    #region Singleton
    private static ProjectileFactory instance;
    private ProjectileFactory() { }
    public static ProjectileFactory Instance { get { return instance ?? (instance = new ProjectileFactory()); } }
    #endregion

    public Dictionary<string, GameObject> prefabDict;
    public enum ProjectileType { Rail, Laser, DestructionBeam, HomingMissile };

    //static int bulletRecycleCount = 10;

    public void Initialize()
    {
        prefabDict = Resources.LoadAll<GameObject>("Resources/Projectiles/").ToDictionary(x => x.name, x => x);
    }

    public Projectile CreateProjectile(ProjectileType pType, Vector3 pos, Vector3 target, float speed = 10)
    {
        Projectile projectile;

        if (GenericObjectPool.Instance.TryDepool(pType.GetType(), out IPoolable poolable))
        {
            projectile = (poolable as Projectile);
            projectile.transform.position = pos;
            projectile.gameObject.SetActive(true);
        }
        else
        {
            projectile = GameObject.Instantiate(prefabDict[pType.ToString()], pos, Quaternion.identity).GetComponent<Projectile>();
            //
            projectile.projType = pType;
        }
        projectile.Initialize();
        ProjectileManager.Instance.projectiles.Add(projectile);
        projectile.transform.SetParent(ProjectileManager.Instance.projParent);
        projectile.speed = speed;
        projectile.target = target;

        return projectile;
    }
}
