using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;

public class Gunner : GunnerBehavior, IBasePlayer, IManagable
{
    public float ShieldCooldown { get; set; }
    public bool StalkEnemy { get; set; }
    public CharacterStats CharStats { get; set; }

    public float gunCooldown;

    public void Initialize()
    {
        
    }

    // Use this for initialization
    public void PostInitialize()
    {

    }

    public void PhysicsRefresh()
    {
        
    }

    // Update is called once per frame
    public void Refresh()
    {
        //gun controls
    }
}
