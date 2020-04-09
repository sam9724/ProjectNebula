using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Drone : DroneBehavior, IMinion
{

    public float playerDetectionRange = 225f;
    public float rotateSpeed = 5f;
    public float MovementSpeed = 5f;
    public float sphereRadius;
    public float sweepDistance;
    Transform player;

    public bool MoveWithBoss { get; set; }
    public bool SeekPlayer { get; set; }
    public bool ChasePlayer { get; set; }
    public bool Wander { get; set; }
    public CharacterStats CharStats { get; set; }
    public bool IsAlive { get; set; }

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
        CharStats = new CharacterStats(10, 0);
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

    public void RegenHP(float dt)
    {
        //throw new System.NotImplementedException();
    }

    public void TakeDamage()
    {
        //to be implemented
    }
    
    public void TakeDamage(float damage)
    {
        //throw new System.NotImplementedException();
    }

    bool PlayerInRange()
    {
        return (Vector3.SqrMagnitude(transform.position - player.position) < playerDetectionRange);
    }

    public void Update()
    {
        player = player ?? PlayerManager.Instance.pilot.transform;
        /*if (networkObject == null)
            return;

        // If we are not the owner of this network object then we should
        // move this cube to the position/rotation dictated by the owner
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            return;
        }*/
        Debug.Log("player" + player.name);

        
    }

    //AI Logic
    

    public override void ShootMissile(RpcArgs args)
    {
        throw new System.NotImplementedException();
    }
}

