using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class Seeker : SeekerBehavior, IMinion
{

    public float playerDetectionRange = 50000f;
    public float rotateSpeed = 5f;
    public float MovementSpeed = 5f;
    Transform player;
    public float sphereRadius = 1.5f;
    public float sweepDistance = 2f;
    Transform barrel;
    float laserCooldown = 0f;

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

    void Start()
    {
        player = PlayerManager.Instance.pilot.transform;
        barrel = transform.Find("Barrel");
        CharStats = new CharacterStats(10, 0);
        MaxHealth = CharStats.health;
        IsAlive = true;
        transform.SetParent(EnemyManager.Instance.enemyParent);
        EnemyManager.Instance.toAdd.Push(this);
    }

    public void Die()
    {
        gameObject.SetActive(false);
        IsAlive = false;
        EnemyManager.Instance.EnemyDied(this);
    }

    /*public void Enqueue()
    {

    }*/

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
        //player = player ?? PlayerManager.Instance.pilot.transform;

        //Debug.Log("player" + player.name);

        if (networkObject == null)
            return;



        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            return;
        }



        if (PlayerInRange())
        {
            //shoot
            if (laserCooldown == 0)
            {
                Debug.Log("Shot laser");
                Shoot();
                laserCooldown = 2f;
            }
            laserCooldown -= dt;
        }
        else
        {
            //check obstacle if none, move to player else move away from obstacle
            //if (CheckObstacle())
            //{
            //    DodgeObstacle();
            //}

            GoToPlayer();
        }
        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
    }

    public void RegenHP(float dt)
    {
        //throw new System.NotImplementedException();
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

    public override void ShootLaser(RpcArgs args)
    {
        AudioSource.PlayClipAtPoint(AudioManager.Instance.soundDict["laser"], transform.position);
        ProjectileFactory.Instance.CreateProjectile(ProjectileFactory.ProjectileType.Laser, barrel.position, player.position, Quaternion.identity, 50);

    }
}
