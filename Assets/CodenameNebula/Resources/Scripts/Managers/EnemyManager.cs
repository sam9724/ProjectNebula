using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyType { Seeker, Drones }

public class EnemyManager 
{
    #region Singleton
    private static EnemyManager instance;
    private EnemyManager() { }
    public static EnemyManager Instance { get { return instance ?? (instance = new EnemyManager()); } }
    #endregion

    public Stack<Enemy> toRemove;
    public Stack<Enemy> toAdd;
    public HashSet<Enemy> enemies;//stacks to keep track of enemies
    public Transform enemyParent;
    float DroneHealth, seekerHealth;
    Transform MinionsSpawnLocation;
    Dictionary<EnemyType, GameObject> enemyPrefabDict = new Dictionary<EnemyType, GameObject>(); //all enemy prefabs


    public void Initialize()
    {
        DroneHealth = MotherShipClass.DroneHealth;
        seekerHealth = MotherShipClass.SeekerHealth;
        toRemove = new Stack<Enemy>();
        toAdd = new Stack<Enemy>();
        enemies = new HashSet<Enemy>();
        enemyParent = new GameObject("EnemyParent").transform;
        MinionsSpawnLocation = MotherShipClass.MinionsSpawnLocation;
        foreach (EnemyType etype in System.Enum.GetValues(typeof(EnemyType))) //fill the resource dictionary with all the prefabs
        {
            enemyPrefabDict.Add(etype, Resources.Load<GameObject>("Prefabs/Enemy/" + etype.ToString())); //Each enum matches the name of the enemy perfectly
        }
        Debug.Log("enemyPrefabDict: "+enemyPrefabDict.Count);
    }
    //\Assets\CodenameNebula\Resources\Prefabs\Enemy

    public void PostInitialize()
    {

    }

    public void PhysicsRefresh(float fdt)
    {

    }
    
    public void Refresh(float dt)
    {
        foreach (Enemy e in enemies)
        {
            if (e.isAlive)
            {
                e.Refresh();
            }
        }

        while (toRemove.Count > 0) //remove all dead ones
        {
            Enemy e = toRemove.Pop();
            GameObject o = e.gameObject;
            enemies.Remove(e);            
            o.GetComponent<Enemy>().enabled = false; //.gameObject.SetActive(false);
            GameObject.Destroy(e.gameObject, 15f);
        }

        while (toAdd.Count > 0) //Add new ones
            enemies.Add(toAdd.Pop());
    }


    public int minionInScene()
    {
        return enemies.Count;
    }

    public void NumberOfMinionsToSpawn(int drone, int seeker)
    {
        
        SpawnMinions(EnemyType.Drones, drone);;
        SpawnMinions(EnemyType.Seeker, seeker);;

    }
    public Enemy SpawnMinions(EnemyType etype, int qty)
    {
        Enemy e = null;
        
            for (int i = 0; i < qty; i++)
            {
            GameObject newEnemy = GameObject.Instantiate(enemyPrefabDict[etype],enemyParent);
            newEnemy.transform.position += MotherShipClass.MinionsSpawnLocation.position;
            Debug.Log("New Enemy Created");

                e = newEnemy.GetComponent<Enemy>();
            
            if (etype.Equals(EnemyType.Drones))
                    e.Initialize(DroneHealth);
                else if (etype.Equals(EnemyType.Seeker))
                    e.Initialize(seekerHealth);
                toAdd.Push(e);
            }           
        return e;
    }

    public void EnemyDied(Enemy enemyDied)
    {
        toRemove.Push(enemyDied);

    }

}
