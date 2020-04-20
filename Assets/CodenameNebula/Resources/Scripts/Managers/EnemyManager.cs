using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Unity;

public enum EnemyType { Seeker, Drones }

public class EnemyManager : GenericSetManager<IBaseEnemy>
{
    #region Singleton
    private static EnemyManager instance;
    private EnemyManager() { }
    public static EnemyManager Instance { get { return instance ?? (instance = new EnemyManager()); } }
    #endregion

    public Transform enemyParent;
    Transform MinionsSpawnLocation;
    Dictionary<EnemyType, GameObject> enemyPrefabDict = new Dictionary<EnemyType, GameObject>(); //all enemy prefabs

    public MotherShip mothership;

    public override void Initialize()
    {
        base.Initialize();
        enemyParent = new GameObject("EnemyParent").transform;
        MinionsSpawnLocation = MotherShipClass.MinionsSpawnLocation;
        foreach (EnemyType etype in System.Enum.GetValues(typeof(EnemyType))) //fill the resource dictionary with all the prefabs
        {
            enemyPrefabDict.Add(etype, Resources.Load<GameObject>("Prefabs/Enemies/" + etype.ToString())); //Each enum matches the name of the enemy perfectly
        }
    }

    public override void PostInitialize()
    {
        base.PostInitialize();
    }

    public override void PhysicsRefresh(float fdt)
    {
        base.PhysicsRefresh(fdt);
    }

    public override void Refresh(float dt)
    {
        foreach (IBaseEnemy e in managingSet)
        {
            if (e.IsAlive)
            {
                e.Refresh(dt);
            }
        }

        while (toRemove.Count > 0) //remove all dead ones
        {
            IBaseEnemy e = toRemove.Pop();
            GameObject o = (e as MonoBehaviour).gameObject;
            managingSet.Remove(e);
            GameObject.Destroy(o, 1f);
        }

        while (toAdd.Count > 0) //Add new ones
            managingSet.Add(toAdd.Pop());
    }


    public int MinionInScene()
    {
        return managingSet.Count;
    }


    public void EnemyDied(IBaseEnemy enemyDied)
    {
        toRemove.Push(enemyDied);
    }
}

