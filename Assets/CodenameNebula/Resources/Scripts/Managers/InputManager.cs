using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : IManagable
{
    #region Singleton
    private static InputManager instance;
    private InputManager() { }
    public static InputManager Instance { get { return instance ?? (instance = new InputManager()); } }
    #endregion

    public InputPkg refreshInputPkg = new InputPkg();
    public InputPkg physicsRefreshInputPkg = new InputPkg();

    public void Initialize()
    {

    }

    public void PhysicsRefresh()
    {
        SetInputPkg(physicsRefreshInputPkg);
    }

    private void SetInputPkg(InputPkg ip)
    {
#if UNITY_ANDROID

        //mobile controls to be implemented
#else
        //ship
        ip.yaw = Input.GetAxis("Mouse Y");
        ip.pitch = Input.GetAxis("Mouse X");
        ip.roll = Input.GetAxis("Horizontal");
        ip.accelerate = Input.GetAxis("Vertical");

        ip.hyperDrive = Input.GetMouseButton(0);

        // gun
        ip.gunYaw = Input.GetAxis("Mouse Y");
        ip.gunPitch = Input.GetAxis("Mouse X");

        ip.fire = Input.GetMouseButton(0);
#endif

    }

    public void Refresh()
    {
        SetInputPkg(refreshInputPkg);
    }

    public void PostInitialize()
    {

    }


    //Data class to handle player input.
    public class InputPkg
    {
        //ship controls
        public float yaw;
        public float pitch;
        public float roll;
        public float accelerate;

        public bool hyperDrive;

        //gun controls
        public float gunYaw;
        public float gunPitch;

        public bool fire;
    }


}
