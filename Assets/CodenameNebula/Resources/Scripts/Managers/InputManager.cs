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

    public InputPkg inputPkg = new InputPkg();
    public InputPkg physicsInputPkg = new InputPkg();

    Vector3 previousGyroEuler;
    float gyroXsensitivity = 10;
    float gyroYsensitivity = 10;

    public void Initialize()
    {
        //previousGyroEuler = DeviceRotation.Get().eulerAngles;
    }

    public void PhysicsRefresh()
    {
        SetInputPkg(physicsInputPkg);
    }

    public void Refresh()
    {
        SetInputPkg(inputPkg);
    }

    private void SetInputPkg(InputPkg ip)
    {
#if UNITY_ANDROID

        //UpdateGyroInput(ip);
#else
        //ship
        ip.yaw = Input.GetAxis("Horizontal");
        ip.pitch = Input.GetAxis("Vertical");
        ip.roll = -Input.GetAxis("Horizontal") * 0.5f;
        UpdateKeyboardThrottle(KeyCode.LeftShift, ip);

        ip.hyperDrive = Input.GetKey(KeyCode.Space);

        // gun
        UpdateGunMovement(ip);

        ip.fire = Input.GetMouseButton(0);
#endif

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
        public float throttle;

        public bool hyperDrive;

        //gun controls
        public float gunYaw;
        public float gunPitch;

        public bool fire;
    }

    void UpdateKeyboardThrottle(KeyCode increaseKey, InputPkg ip)
    {
        if (Input.GetKey(increaseKey))
            ip.throttle += 50f;
        else
            ip.throttle -= 10f;

        ip.throttle = Mathf.Clamp(ip.throttle, 0.0f, 500);
    }

    void UpdateGunMovement(InputPkg ip)
    {
        Vector3 mousePos = Input.mousePosition;

        // Figure out most position relative to center of screen.
        // (0, 0) is center, (-1, -1) is bottom left, (1, 1) is top right.      
        ip.gunPitch = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
        ip.gunYaw = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

        // Make sure the values don't exceed limits.
        ip.gunPitch = -Mathf.Clamp(ip.gunPitch, -1.0f, 1.0f);
        ip.gunYaw = Mathf.Clamp(ip.gunYaw, -1.0f, 1.0f);
    }

    void UpdateGyroInput(InputPkg ip)
    {
        Vector3 deviceEulers = DeviceRotation.Get().eulerAngles;
        Vector3 deltaEulers = previousGyroEuler - deviceEulers;
        previousGyroEuler = deviceEulers;

        //if (deltaEulers.x > gyroXsensitivity)
            ip.gunPitch = deltaEulers.x;
        /*else
            ip.gunPitch = 0;*/

        //if (deltaEulers.y > gyroYsensitivity)
            ip.gunYaw = deltaEulers.y;
        /*else
            ip.gunYaw = 0;*/
    }
}
