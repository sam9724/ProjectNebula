﻿using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Drone : DroneBehavior, IMinion
{

    public float playerDetectionRange = 100f;
    public float rotateSpeed = 5f;
    public float MovementSpeed = 5f;
    int TargetDistance = 20;
    Transform player;
    Transform mothership;
    Transform missileLocation;
    float missileCooldown = 0f;
    public bool MoveWithBoss { get; set; }
    public bool SeekPlayer { get; set; }
    public bool ChasePlayer { get; set; }
    public bool Wander { get; set; }
    public CharacterStats CharStats { get; set; }
    public bool IsAlive { get; set; }
    public float MaxHealth { get; set; }


    /*public IPoolable Dequeue()
    {
        // stub implementation
        return this;
    }*/

    public void Die()
    {
        gameObject.SetActive(false);
        IsAlive = false;
        EnemyManager.Instance.EnemyDied(this);
    }

    /*public void Enqueue()
    {
        
    }*/

    public void Start()
    {
        player =  PlayerManager.Instance.pilot.transform;
        mothership = EnemyManager.Instance.mothership.transform;
        missileLocation = transform.Find("missilePos");
        CharStats = new CharacterStats(10, 0);
        MaxHealth = CharStats.health;
        IsAlive = true;
        transform.SetParent(mothership);
    }

    public void Initialize()
    {
        
    }

    public void PhysicsRefresh(float dt)
    {
        
    }

    public void PostInitialize()
    {
        
    }

    public void Refresh(float dt)
    {

        if (networkObject == null)
            return;

        // If we are not the owner of this network object then we should
        // move this cube to the position/rotation dictated by the owner
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            return;
        }

            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - mothership.position), rotateSpeed * dt);
            //transform.RotateAround(mothership.transform.position, mothership.transform.forward, 50 * dt);



            transform.RotateAround(mothership.position, mothership.up, 10f * dt);
            Vector3 delta = transform.position - mothership.position;
            delta.y = 0; // Keep same Y level
            transform.position = mothership.position + delta.normalized * TargetDistance;




        //transform.position += transform.forward * MovementSpeed * dt;
            networkObject.position = transform.position;
            networkObject.rotation = transform.rotation;

        //Debug.Log("player" + player.name);
        if (PlayerInRange() && missileCooldown == 0)
        {
            networkObject.SendRpc(RPC_SHOOT_MISSILE, Receivers.All);
            missileCooldown = 20;
        }
        missileCooldown -= dt;
    }

    public void RegenHP(float dt)
    {
        //throw new System.NotImplementedException();
    }

    public void TakeDamage()
    {
        
    }
    
    public void TakeDamage(float damage)
    {
        CharStats.health -= damage;
        if(CharStats.health <= 0)
        {
            Explode();
            Die();
        }
    }

    bool PlayerInRange()
    {
        return (Vector3.SqrMagnitude(transform.position - player.position) < playerDetectionRange);
    }

    public void Update()
    {
        
        
    }

    //AI Logic
     void Explode()
    {
        // Explosion effect goes here. Sound line is put in first cause sound takes a bit to load
        AudioSource.PlayClipAtPoint(AudioManager.Instance.soundDict["explosionSound"], transform.position);
        ParticleFactory.Instance.CreateParticle(ParticleFactory.ParticleType.HomingMissileExplosion, transform.position, Quaternion.identity);
    }

    public override void ShootMissile(RpcArgs args)
    {
        AudioSource.PlayClipAtPoint(AudioManager.Instance.soundDict["missile"], transform.position);
        ProjectileFactory.Instance.CreateProjectile(ProjectileFactory.ProjectileType.HomingMissile, missileLocation.position, player.position, Quaternion.identity, 10);
    }
}

