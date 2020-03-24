using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;


public class Pilot : PilotBehavior, IBasePlayer, IDamagable
{

    public float ShieldCooldown { get; set; }
    public bool StalkEnemy { get; set; }
    public CharacterStats CharStats { get; set; }

    public float boost;
    float boostCooldown;

    public void Initialize()
    {

    }

    // Use this for initialization
    public void PostInitialize()
    {

    }

    public void PhysicsRefresh()
    {
        //controls
    }

    // Update is called once per frame
    public void Refresh()
    {

    }

    public void TakeDamage()
    {
        //to be implemented
    }

    public void Die()
    {
        //to be implemented
    }
}
