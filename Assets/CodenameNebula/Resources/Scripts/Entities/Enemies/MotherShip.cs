using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MotherShip : MonoBehaviour, IBaseEnemy, IManagable
{
    public bool ChasePlayer { get; set; }
    public bool Wander { get; set; }
    public CharacterStats CharStats { get; set; }

    float shieldCooldown;
    float cloakCooldown;
    Transform player;

    public void Die()
    {
        //to be implemented
    }

    public void Initialize()
    {
        
    }

    public void PhysicsRefresh()
    {
        
    }

    public void PostInitialize()
    {
        
    }

    public void Refresh()
    {
        
    }

    public void TakeDamage()
    {
        
    }

    //bool PlayerInRange()
    //{
        
    //}

    void FollowPlayer()
    {
        
    }
    //AI Logic
}

