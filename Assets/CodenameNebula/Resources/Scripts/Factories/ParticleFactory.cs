using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ParticleFactory : GenericFactory<Particle, ParticleFactory.ParticleType>
{

    #region Singleton
    private static ParticleFactory instance;
    private ParticleFactory() { }
    public static ParticleFactory Instance { get { return instance ?? (instance = new ParticleFactory()); } }
    #endregion

    public enum ParticleType { RailExplosion, LaserExplosion, DestructionBeamExplosion, HomingMissileExplosion };
    

    public void Initialize()
    {
        prefabDict = Resources.LoadAll<GameObject>("Prefabs/Particles/").ToDictionary(x => x.name);
    }

    public Particle CreateParticle(ParticleType pType, Vector3 pos, Quaternion rot)
    {
        Particle particle = CreateObject(pType, pos, rot, ParticleManager.Instance.partParent);

        particle.Initialize();
        ParticleManager.Instance.managingSet.Add(particle);
        particle.partType = pType;

        return particle;
    }
}
