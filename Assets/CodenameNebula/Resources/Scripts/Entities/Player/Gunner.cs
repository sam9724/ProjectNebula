﻿using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine.UI;

public class Gunner : GunnerBehavior, IBasePlayer
{
    public float ShieldCooldown { get; set; }
    public bool StalkEnemy { get; set; }
    public CharacterStats CharStats { get; set; }
    Button shootButton;
    float rotateSpeed = 2;
    [HideInInspector]
    public float gunCooldown = 0.1f;
    [HideInInspector]
    public float gunRange = 100;
    [HideInInspector]
    public float maxGunHeat = 4; // max continuous fire allowed 4 sec
    public float currentGunHeat = 0;
    float gunCounter;
    [HideInInspector]
    public Transform muzzle;
    [HideInInspector]
    public Vector3 target;
    [HideInInspector]
    public float bulletSpeed = 15;
    public GameObject GunnerCanvas;
    Transform gunBase;
    Transform barrel;


    bool gunOverheat = false;

    Vector3 previousGyroEuler;
    float gyroXsensitivity = 0.2f;
    float gyroYsensitivity = 0.2f;
    float barrelZoffset = 180;
    float barrelXoffset = 30;

    DynamicJoystick DynamicJoystick;
    Image healthbar;
    Image shieldbar;

    Vector3 screenCenter;
    public Camera mainCamera;
    GameObject EndScreen;

    //required coz we don't control the spawning of networked objects in the scene.
    public void Start()
    {
        PlayerManager.Instance.gunner = this;
        transform.SetParent(PlayerManager.Instance.pilot.transform);
        gunBase = transform.Find("Base"); //Base/ - for new prefab
        barrel = gunBase.Find("Barrel");
        muzzle = barrel.Find("Muzzle");
        CharStats = new CharacterStats();
        previousGyroEuler = DeviceRotation.Get().eulerAngles;
        EndScreen = GameObject.Find("EndScreen");
        GunnerCanvas = GameObject.Find("GunnerCanvas");

        if (GunnerCanvas)
        {
            DynamicJoystick = GunnerCanvas.transform.Find("Dynamic Joystick").GetComponent<DynamicJoystick>();
            shootButton = GunnerCanvas.transform.Find("Button").GetComponent<Button>();
            healthbar = GunnerCanvas.transform.Find("DoubleBar").Find("lifeBar").GetComponent<Image>();
            shieldbar = GunnerCanvas.transform.Find("DoubleBar").Find("shieldBar").GetComponent<Image>();

            shootButton.onClick.AddListener(onShootButton);
        }
        

        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        mainCamera = Camera.main;
    }
    public void Initialize()
    {
        
    }

    public void Die()
    {
        if (GunnerCanvas) // GunnerCanvas will be null if  this runs on Pilot's device and hence not required.
        {
            GetEndScreen();
        }
    }

    void GetEndScreen()
    {
        EndScreen.transform.Find("EndText").GetComponent<Text>().text = "You lose";
        GameFlow.Instance.isPaused = true;
        AudioSource.PlayClipAtPoint(AudioManager.Instance.soundDict["gameOver"], PlayerManager.Instance.pilot.transform.position);
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
        if(PlayerManager.Instance.pilot.CharStats.health <= 0)
        {
            Die();
        }


#if UNITY_ANDROID
        //Vector3 delta = CalculateGyroDelta();
        //gunBase.rotation = Quaternion.Euler(0, gunBase.rotation.eulerAngles.y - delta.y, 0);
        //barrel.rotation = Quaternion.Euler(barrel.rotation.eulerAngles.x - delta.x, gunBase.rotation.eulerAngles.y - delta.y, barrelZoffset);
        Vector3 direction = Vector3.right * DynamicJoystick.Horizontal;
        gunBase.Rotate(0, direction.x, 0);
        RaycastHit hit;
        Debug.DrawRay(barrel.position, barrel.up*40f,Color.yellow);
        Vector3 altitudeTilt = transform.forward * DynamicJoystick.Vertical;
        barrel.Rotate(Mathf.Clamp(-altitudeTilt.z, -78.289f, 30), 0, 0);
            
        

#else
        gunBase.Rotate(new Vector3(0, Mathf.Clamp(InputManager.Instance.inputPkg.gunYaw, 0, 36), 0)* rotateSpeed * dt);
        barrel.Rotate(new Vector3(-Mathf.Clamp(InputManager.Instance.inputPkg.gunPitch, 0, 36), 0, 0) * rotateSpeed * dt);
#endif
        // If we are the owner of the object we should send the new position
        // and rotation across the network for receivers to move to in the above code
        networkObject.baseRotation = gunBase.rotation;
        networkObject.barrelRotation = barrel.rotation;

        // Note: Forge Networking takes care of only sending the delta, so there
        // is no need for you to do that manually

        //gunCounter += dt;

        if (GunnerCanvas)
        {
            healthbar.fillAmount = PlayerManager.Instance.pilot.CharStats.health / PlayerManager.Instance.pilot.maxHealth;
            shieldbar.fillAmount = PlayerManager.Instance.pilot.CharStats.shield / PlayerManager.Instance.pilot.maxShield;
        }


        //Gun Heat mechanics
        if (gunOverheat)
            currentGunHeat -= dt;
        currentGunHeat = Mathf.Clamp(currentGunHeat, 0, 4);
        if (currentGunHeat >= maxGunHeat)
            gunOverheat = true;
        else if (currentGunHeat <= 0)
            gunOverheat = false;
    }
    void onShootButton()
    {
        //Debug.Log("Shoot Button");

        if (!gunOverheat)
        {
            currentGunHeat += Time.deltaTime;
            gunCounter = 0;
            target = mainCamera.ScreenPointToRay(screenCenter).GetPoint(gunRange);
            networkObject.target = target;
            networkObject.SendRpc(RPC_SHOOT, Receivers.All);
            Handheld.Vibrate();
        } 
    }
    public override void Shoot(RpcArgs args)
    {
        ProjectileFactory.Instance.CreateProjectile(ProjectileFactory.ProjectileType.Rail, muzzle.position, target, Quaternion.identity, bulletSpeed);
        AudioSource.PlayClipAtPoint(AudioManager.Instance.soundDict["rail"], muzzle.position);
    }

    Vector3 CalculateGyroDelta()
    {
        Vector3 deviceEulers = DeviceRotation.Get().eulerAngles;
        Vector3 deltaEulers = previousGyroEuler - deviceEulers;
        Debug.Log("Delta : " + deltaEulers);
        previousGyroEuler = deviceEulers;

        if (Mathf.Abs(deltaEulers.x) < gyroXsensitivity)
            deltaEulers.x = 0;

        if (Mathf.Abs(deltaEulers.y) < gyroYsensitivity)
            deltaEulers.y = 0;

        return new Vector3(Mathf.Clamp(deltaEulers.x, -10f, 10f), Mathf.Clamp(deltaEulers.y, -10f, 10f));
    }
}
