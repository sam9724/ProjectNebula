using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Drone : Enemy, IMinion, IManagable
{
    public bool MoveWithBoss { get; set; }
    public bool SeekPlayer { get; set; }
    public bool ChasePlayer { get; set; }
    public bool Wander { get; set; }
    public CharacterStats CharStats { get; set; }

    /*public IPoolable Dequeue()
    {
        // stub implementation
        return this;
    }*/

    public void Die()
    {
        //to be implemented
    }

    /*public void Enqueue()
    {
        
    }*/

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
        //to be implemented
    }

    //AI Logic
}

