using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ParticleManager 
{
    #region Singleton
    private static ParticleManager instance;
    private ParticleManager() { }
    public static ParticleManager Instance { get { return instance ?? (instance = new ParticleManager()); } }
    #endregion

    public Stack<Particle> toRemove;
    public Stack<Particle> toAdd;
    public HashSet<Particle> particles;
    public Transform partParent;
    
    public Transform spawnLocation;
    public Vector3 spawnDirection;

    public Transform pooledPartParent;


    public void Initialize()
    {
        particles = new HashSet<Particle>();
        toRemove = new Stack<Particle>();
        toAdd = new Stack<Particle>();
        partParent = new GameObject("PartParent").transform;
        pooledPartParent = new GameObject("DeadPart").transform;
    }

    public void PostInitialize()
    {

    }


    public void Refresh(float dt)
    {
        foreach (Particle p in particles)
        {
            p.Refresh(dt);
        }

        while (toRemove.Count > 0)
            particles.Remove(toRemove.Pop());

        while (toAdd.Count > 0)
            particles.Add(toAdd.Pop());
    }


    public void PhysicsRefresh(float dt)
    {

    }

    public void ParticleDied(Particle p)
    {
        toRemove.Push(p);
        p.transform.SetParent(pooledPartParent);
        GenericObjectPool.Instance.PoolObject(p.partType.GetType(), p);
    }
}
