using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ParticleFactory
{

    #region Singleton
    private static ParticleFactory instance;
    private ParticleFactory() { }
    public static ParticleFactory Instance { get { return instance ?? (instance = new ParticleFactory()); } }
    #endregion

    public Dictionary<string, GameObject> prefabDict;
    public enum ParticleType { RailExplosion, LaserExplosion, DestructionBeamExplosion, HomingMissileExplosion };
    

    public void Initialize()
    {
        prefabDict = Resources.LoadAll<GameObject>("Prefabs/Particles/").ToDictionary(x => x.name);
    }

    public Particle CreateParticle(ParticleType pType, Vector3 pos, Quaternion rot)
    {
        Particle particle;

        if (GenericObjectPool.Instance.TryDepool(pType.GetType(), out IPoolable poolable))
        {
            particle = (poolable as Particle);
            particle.transform.position = pos;
            particle.transform.rotation = rot;
            particle.gameObject.SetActive(true);
        }
        else
        {
            particle = GameObject.Instantiate(prefabDict[pType.ToString()], pos, rot).GetComponent<Particle>();
            particle.partType = pType;
        }
        particle.Initialize();
        ParticleManager.Instance.particles.Add(particle);
        particle.transform.SetParent(ParticleManager.Instance.partParent);

        return particle;
    }
}
