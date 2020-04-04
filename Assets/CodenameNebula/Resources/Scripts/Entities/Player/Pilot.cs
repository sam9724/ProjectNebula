﻿using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;


public class Pilot : PilotBehavior, IBasePlayer, IDamagable
{

    public float ShieldCooldown { get; set; }
    public bool StalkEnemy { get; set; }
    public CharacterStats CharStats { get; set; }

    public float boost;
    float boostCooldown;

    float moveSpeed = 10;

    public Rigidbody rb;

    public Transform gunnerSpawnPos;

    DynamicJoystick DynamicJoystick;
    DynamicJoystick AltitudeJoystick;


    //required coz we don't control the spawning of networked objects in the scene.
    public void Start()
    {
        
        PlayerManager.Instance.pilot = this;
        gunnerSpawnPos = transform.Find("TurretSpawnLoc");
        rb = gameObject.GetComponent<Rigidbody>();

        DynamicJoystick = GameObject.FindGameObjectWithTag("dynamicjoystick").GetComponent<DynamicJoystick>();
        AltitudeJoystick = GameObject.FindGameObjectWithTag("Altitude").GetComponent<DynamicJoystick>();
    }
    public void Initialize()
    {

        //DynamicJoystick[] ds= GameObject.FindObjectsOfType<DynamicJoystick>();
        //if(ds[0].CompareTag(""))
        //{

        //}

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
    public void Refresh(float dt)
    {
        // If unity's Update() runs, before the object is
        // instantiated in the network, then simply don't
        // continue, otherwise a bug/error will happen.
        // 
        // Unity's Update() running, before this object is instantiated
        // on the network is **very** rare, but better be safe 100%
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

        // Let the owner move the cube around with the arrow keys
#if UNITY_ANDROID
        Vector3 direction = Vector3.right * DynamicJoystick.Horizontal;
        transform.Rotate(0, direction.x, 0);

        Vector3 acceleration = transform.forward * DynamicJoystick.Vertical;
        rb.AddForce(acceleration * moveSpeed * dt, ForceMode.VelocityChange);

        if (transform.rotation.x > -30f && transform.position.x < 30f)
        {
            Vector3 altitudeTilt = transform.forward * AltitudeJoystick.Vertical;
            Debug.Log(altitudeTilt);
            transform.Rotate(-altitudeTilt.z, 0, 0);
        }
#else
            rb.AddRelativeForce(Vector3.forward * InputManager.Instance.refreshInputPkg.accelerate * dt * moveSpeed);
            rb.AddRelativeTorque(new Vector3(InputManager.Instance.refreshInputPkg.yaw, InputManager.Instance.refreshInputPkg.pitch, 0) * dt * moveSpeed);
        
#endif

        // If we are the owner of the object we should send the new position
        // and rotation across the network for receivers to move to in the above code
        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;

        // Note: Forge Networking takes care of only sending the delta, so there
        // is no need for you to do that manually
    }

    public void TakeDamage(float damage)
    {
        //to be implemented
    }

    public void Die()
    {
        //to be implemented
    }
}
