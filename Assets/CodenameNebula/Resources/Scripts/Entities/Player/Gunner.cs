using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;

public class Gunner : GunnerBehavior, IBasePlayer
{
    public float ShieldCooldown { get; set; }
    public bool StalkEnemy { get; set; }
    public CharacterStats CharStats { get; set; }

    public float gunCooldown;

    //required coz we don't control the spawning of networked objects in the scene.
    public void Start()
    {
        PlayerManager.Instance.gunner = this;
        transform.SetParent(PlayerManager.Instance.pilot.transform);
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
            transform.rotation = networkObject.rotation;
            return;
        }

        // Let the owner move the cube around with the arrow keys
        Quaternion rot;

#if UNITY_ANDROID
        rot = Quaternion.Euler(variableJoystick.Vertical * speed * Time.deltaTime, variableJoystick.Horizontal * speed * Time.deltaTime, 0);
        //pos = new Vector3(variableJoystick.Horizontal, 0, variableJoystick.Vertical).normalized * speed * Time.deltaTime;
        //Debug.Log("Pos : " + pos);
#else
        //rot = Quaternion.Euler(Input.GetAxis("Vertical") * speed * Time.deltaTime, Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.A))
            transform.RotateAround(transform.position, -Vector3.up, 50 * dt);
        else if (Input.GetKey(KeyCode.D))
            transform.RotateAround(transform.position, Vector3.up, 50 * dt);
#endif
        //if (rot != Quaternion.identity)
            //transform.rotation = rot;

        // If we are the owner of the object we should send the new position
        // and rotation across the network for receivers to move to in the above code
        networkObject.rotation = transform.rotation;

        // Note: Forge Networking takes care of only sending the delta, so there
        // is no need for you to do that manually
    }
}
