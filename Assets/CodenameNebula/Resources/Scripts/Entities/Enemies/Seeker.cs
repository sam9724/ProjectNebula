using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class Seeker : SeekerBehavior, IMinion
{

    public float playerDetectionRange = 225f;
    public float rotateSpeed = 5f;
    public float MovementSpeed = 5f;
    Transform player;
    public float sphereRadius = 1.5f;
    public float sweepDistance = 2f;
    Transform barrel;
    float laserCooldown = 10f;
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

    void Start()
    {
        
    }

    public void Die()
    {
        gameObject.SetActive(false);
        EnemyManager.Instance.EnemyDied(this);
    }

    /*public void Enqueue()
    {

    }*/

    public void Initialize()
    {
        barrel = transform.Find("Barrel");
        CharStats = new CharacterStats(10, 0);
    }

    public void PhysicsRefresh(float dt)
    {
        
    }

    public void PostInitialize()
    {
        
    }

    public void Refresh(float dt)
    {
        player = player ?? PlayerManager.Instance.pilot.transform;

        Debug.Log("player" + player.name);

        if (PlayerInRange())
        {
            //shoot
            if(laserCooldown == 0)
            {
                Shoot();
                laserCooldown = 10f;
            }
            laserCooldown -= dt;
        }
        else
        {
            //check obstacle if none, move to player else move away from obstacle
            if (CheckObstacle())
            {
                DodgeObstacle();
            }
            else
            {
                GoToPlayer();
            }

        }
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
        if (CharStats.health <= 0)
        {
            Explode();
            Die();
        }
    }

    bool PlayerInRange()
    {
        return (Vector3.SqrMagnitude(transform.position - player.position) < playerDetectionRange);
    }

    public void Shoot()
    {
        networkObject.SendRpc(RPC_SHOOT_LASER, Receivers.All);
    }

    public void Update()
    {
        
    }


    void DodgeObstacle()
    {
        RaycastHit hit;
        float step = MovementSpeed * Time.deltaTime; // calculate distance to move
        if (!(Physics.SphereCast(transform.position, sphereRadius, Vector3.left, out hit, sweepDistance)))
        {

            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(5, 0, 0), step);
        }
        else if (!(Physics.SphereCast(transform.position, sphereRadius, Vector3.right, out hit, sweepDistance)))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-5, 0, 0), step);
        }
        else if (!(Physics.SphereCast(transform.position, sphereRadius, Vector3.up, out hit, sweepDistance)))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 5, 0), step);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, -5, 0), step);
        }
    }

    void GoToPlayer()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotateSpeed * Time.deltaTime);
        //Move towards the player
        transform.position += transform.forward * MovementSpeed * Time.deltaTime;
    }

    bool CheckObstacle()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, sphereRadius, transform.forward, out hit, sweepDistance))
            return true;
        else
            return false;
        //see if there's anything around you
    }
    void Explode()
    {
        // Explosion effect goes here. Sound line is put in first cause sound takes a bit to load
        AudioSource.PlayClipAtPoint(AudioManager.Instance.soundDict["explosionSound"], transform.position);
        ParticleFactory.Instance.CreateParticle(ParticleFactory.ParticleType.HomingMissileExplosion, transform.position, Quaternion.identity);
    }

    public override void ShootLaser(RpcArgs args) => ProjectileFactory.Instance.CreateProjectile(ProjectileFactory.ProjectileType.Laser, barrel.position, player.position ,Quaternion.identity, 50);
}
