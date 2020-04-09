using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MotherShip : MothershipBehavior, IBaseEnemy, IShielded
{
    public bool ChasePlayer { get; set; }
    public bool Wander { get; set; }
    public CharacterStats CharStats { get; set; }
    public bool IsAlive { get; set; }
    public DestructorBeam destructorBeam;

    float shieldCooldown;
    float cloakCooldown;
    Transform player;

    public float playerDetectionRange = 225f;
    public float rotateSpeed = 5f;
    public float MovementSpeed = 5f;
    float tempTimeCounter = 0f;
    bool isUp = false;
    public float idleMovement = 0.005f;
    public Transform MinionsSpawnLocation;

    public Animator motherShipAnimator;

    public float xMin = 0;
    public float xMax = 0;
    public float yMin = 0;
    public float yMax = 0;
    public float zMin = 0;
    public float zMax = 0;

    Vector3 vtemp = Vector3.zero;

    public void Die()
    {
        //EnemyManager.Instance.EnemyDied(this);
        //isAlive = false;

        //game win message
    }

    

    //public Transform SwirlTransform;

    void Start()
    {

        player = PlayerManager.Instance.pilot.transform;
        MotherShipClass.player = player;
        MotherShipClass.motherShip = transform;
        MotherShipClass.playerDetectionRange = playerDetectionRange;
        MotherShipClass.rotateSpeed = rotateSpeed;
        MotherShipClass.MovementSpeed = MovementSpeed;
        MotherShipClass.MinionsSpawnLocation = MinionsSpawnLocation;
        MotherShipClass.xMin = xMin;
        MotherShipClass.xMax = xMax;
        MotherShipClass.yMin = yMin;
        MotherShipClass.yMax = yMax;
        MotherShipClass.zMin = zMin;
        MotherShipClass.zMax = zMax;

        EnemyManager.Instance.mothership = this;
        IsAlive = true;
        CharStats = new CharacterStats(100, 0);

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
        //Debug.Log("player"+player.name);

        if (PlayerInRange())
        {
            motherShipAnimator.SetBool("PlayerInRange", true);
        }
        else
        {
            motherShipAnimator.SetBool("PlayerInRange", false);
        }
    }

    bool PlayerInRange()
    {
      //kk
      //no

        return (Vector3.SqrMagnitude(transform.position - player.position) < playerDetectionRange);
    }

    public void TakeDamage(float damage)
    {
        CharStats.health -= damage;
        if (CharStats.health <= 0)
        {
            Debug.Log("Mothership Hp: " + CharStats.health);
            Die();
            IsAlive = false;
        }
    }

    public void shoot()
    {
        networkObject.SendRpc(RPC_FIRE_BEAM, Receivers.All);
    }

    public override void fireBeam(RpcArgs args)
    {
        destructorBeam =(DestructorBeam) ProjectileFactory.Instance.CreateProjectile(ProjectileFactory.ProjectileType.DestructionBeam, transform.position, player.position , Quaternion.identity, 10f); ;
        
    }

    public override void spawnMinions(RpcArgs args)
    {
        //throw new System.NotImplementedException();
    }

    public void Refresh()
    {
        //throw new System.NotImplementedException();
    }

    public void RegenHP(float dt)
    {
        //throw new System.NotImplementedException();
    }

    public void TakeShieldDamage(float damage)
    {
        //throw new System.NotImplementedException();
    }

    public void BreakShield()
    {
        //throw new System.NotImplementedException();
    }

    public void RegenShield(float dt)
    {
        //throw new System.NotImplementedException();
    }

    //AI Logic in state behavior scripts
}