using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ProjectileFactory : GenericFactory<Projectile, ProjectileFactory.ProjectileType>
{

    #region Singleton
    private static ProjectileFactory instance;
    private ProjectileFactory() { }
    public static ProjectileFactory Instance { get { return instance ?? (instance = new ProjectileFactory()); } }
    #endregion

    public enum ProjectileType { Rail, Laser, DestructorBeam, HomingMissile };
   

    public void Initialize()
    {
        prefabDict = Resources.LoadAll<GameObject>("Prefabs/Projectiles/").ToDictionary(x => x.name);
    }

    public Projectile CreateProjectile(ProjectileType pType, Vector3 pos, Vector3 target, Quaternion rot, float speed)
    {
        Projectile projectile = CreateObject(pType, pos, rot, ProjectileManager.Instance.projParent);
        
        projectile.Initialize();
        ProjectileManager.Instance.managingSet.Add(projectile);
        projectile.projType = pType;
        projectile.speed = speed;
        projectile.target = target;

        return projectile;
    }
}
