using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MotherShip : MothershipBehavior, IBaseEnemy, IShielded
{
    public bool ChasePlayer { get; set; }
    public bool Wander { get; set; }
    public CharacterStats CharStats { get; set; }
    public bool IsAlive { get; set; }

    public float MaxHealth { get; set; }

    //public DestructorBeam destructorBeam;

    float shieldCooldown;
    float cloakCooldown;
    Transform player;
    GameObject EndScreen;
    Text EndText;
    public bool isOwner;

    public float playerDetectionRange = 15625f;
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

    public bool isCloaked = false, activateCloak = false, deactivateCloak = false;
    Material material;
    float cloakingShaderSpeed = 0.15f;

    public void Die()
    {
        //EnemyManager.Instance.EnemyDied(this);
        //isAlive = false;
        //gameObject.SetActive(false);
        GetEndScreen();
        AudioSource.PlayClipAtPoint(AudioManager.Instance.soundDict["win"], PlayerManager.Instance.pilot.transform.position);
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
        isOwner = networkObject.IsOwner;
        EnemyManager.Instance.mothership = this;
        IsAlive = true;
        CharStats = new CharacterStats(100, 0);
        MaxHealth = CharStats.health;
        EndScreen = GameObject.Find("EndScreen");
        material = transform.Find("F3_Green Variant").GetComponent<Renderer>().material;
    }

    public void Initialize()
    {

    }

    public void PhysicsRefresh(float dt)
    {
        
    }

    void GetEndScreen()
    { 
        EndScreen.transform.Find("EndText").GetComponent<Text>().text = "You win!";
        GameFlow.Instance.isPaused = true;
    }

    public void PostInitialize()
    {
        
    }

    public void SpawnMinions(EnemyType etype, int qty)
    {
        for (int i = 0; i < qty; i++)
        {
            if (etype == EnemyType.Seeker)
            {
                NetworkManager.Instance.InstantiateSeeker(0, MotherShipClass.MinionsSpawnLocation.position, Quaternion.identity);
            }
            else if (etype == EnemyType.Drones)
            {
                NetworkManager.Instance.InstantiateDrone(0, MotherShipClass.MinionsSpawnLocation.position, Quaternion.identity);
            }
        }
    }

    public void Update()
    {

        if (networkObject == null)
            return;

        //If we are not the owner of this network object then we should
        // move this cube to the position/ rotation dictated by the owner
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            material.SetFloat("_CloakSlider", networkObject.cloakValue);
            return;
        }
        //Debug.Log("player" + player.name);
        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;


        if (PlayerInRange())
        {
            motherShipAnimator.SetBool("PlayerInRange", true);
        }
        else
        {
            motherShipAnimator.SetBool("PlayerInRange", false);
        }

        float dt = Time.deltaTime;
        networkObject.cloakValue = material.GetFloat("_CloakSlider");
        if (!isCloaked && activateCloak)
        {
            Cloak(dt);
        }
        if (isCloaked && deactivateCloak)
        {
            UnCloak(dt);
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
            //Debug.Log("Mothership Hp: " + CharStats.health);
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
        //Buggy code commented out
        //destructorBeam =(DestructorBeam) 
        //ProjectileFactory.Instance.CreateProjectile(ProjectileFactory.ProjectileType.DestructorBeam, transform.position, player.position , Quaternion.identity, 10f); ;
        //AudioSource.PlayClipAtPoint(AudioManager.Instance.soundDict["destructorBeam"], transform.position);
    }

    public void Refresh(float dt)
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

    public void Cloak(float dt)
    {
        material.SetFloat("_CloakSlider", Mathf.Lerp(0, 1, cloakingShaderSpeed ));
        if (networkObject.cloakValue >= 1)
            isCloaked = true;
    }

    public void UnCloak(float dt)
    {
        material.SetFloat("_CloakSlider", Mathf.Lerp(1, 0, cloakingShaderSpeed * dt));
        if (networkObject.cloakValue <= 0)
            isCloaked = false;
    }
    //AI Logic in state behavior scripts
}