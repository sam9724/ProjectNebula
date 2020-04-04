﻿using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class Gunner : GunnerBehavior, IBasePlayer
{
    public float ShieldCooldown { get; set; }
    public bool StalkEnemy { get; set; }
    public CharacterStats CharStats { get; set; }

    float rotateSpeed = 20;
     Gyroscope gryo;
    public float gunCooldown = 1;
    float gunCounter;

    public Transform muzzle;
    public Vector3 target;

    Transform gunBase;
    Transform barrel;


    //required coz we don't control the spawning of networked objects in the scene.
    public void Start()
    {
        PlayerManager.Instance.gunner = this;
        transform.SetParent(PlayerManager.Instance.pilot.transform);
        gunBase = transform.Find("Base"); //Base/ - for new prefab
        barrel = gunBase.Find("Barrel");
        muzzle = barrel.Find("Muzzle");
    }
    public void Initialize()
    {
        
    }

    // Use this for initialization
    public void PostInitialize()
    {

    }

    public void PhysicsRefresh()
    {
        
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
            gunBase.rotation = networkObject.baseRotation;
            barrel.rotation = networkObject.barrelRotation;
            target = networkObject.target;
            return;
        }

        // Let the owner move the cube around with the arrow keys

        

#if UNITY_ANDROID
        gryo = Input.gyro;
        gryo.enabled = true;
        transform.localRotation = gryo.attitude * new Quaternion(0,0,1,0);
#else
        gunBase.Rotate(new Vector3(0, InputManager.Instance.inputPkg.gunYaw, 0)* rotateSpeed * dt);
        barrel.Rotate(new Vector3(-InputManager.Instance.inputPkg.gunPitch, 0, 0) * rotateSpeed * dt);
#endif
        // If we are the owner of the object we should send the new position
        // and rotation across the network for receivers to move to in the above code
        networkObject.baseRotation = gunBase.rotation;
        networkObject.barrelRotation = barrel.rotation;

        // Note: Forge Networking takes care of only sending the delta, so there
        // is no need for you to do that manually

        gunCounter += dt;
        if (InputManager.Instance.inputPkg.fire)
        {
            if (gunCounter > gunCooldown)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
                {
                    gunCounter = 0;
                    target = hit.point;
                    networkObject.target = target;
                    networkObject.SendRpc(RPC_SHOOT, Receivers.All);
                }
                
            }
        }
    }

    public override void Shoot(RpcArgs args) => ProjectileFactory.Instance.CreateProjectile(ProjectileFactory.ProjectileType.Rail, muzzle.position, target, 50);
}
