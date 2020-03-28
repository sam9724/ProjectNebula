using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;


public class Pilot : PilotBehavior, IBasePlayer, IDamagable
{

    public float ShieldCooldown { get; set; }
    public bool StalkEnemy { get; set; }
    public CharacterStats CharStats { get; set; }

    public float boost;
    float boostCooldown;

    float speed = 5;

    public Rigidbody rb;

    public Transform gunnerSpawnPos;

    //required coz we don't control the spawning of networked objects in the scene.
    public void Start()
    {
        PlayerManager.Instance.pilot = this;
        gunnerSpawnPos = transform.Find("TurretSpawnLoc");
        rb = gameObject.GetComponent<Rigidbody>();
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
        Vector3 pos;

        //pos = Vector3.forward * Input.GetAxis("Vertical")//variableJoystick.Vertical 
        //+ Vector3.right * Input.GetAxis("Horizontal");//variableJoystick.Horizontal;
#if UNITY_ANDROID

        pos = new Vector3(variableJoystick.Horizontal, 0, variableJoystick.Vertical).normalized * speed * Time.deltaTime;
        //Debug.Log("Pos : " + pos);
#else
        pos = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * speed * dt;
        //Debug.Log("Pos : " + pos);
#endif
        transform.position += pos;

        // If we are the owner of the object we should send the new position
        // and rotation across the network for receivers to move to in the above code
        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;

        // Note: Forge Networking takes care of only sending the delta, so there
        // is no need for you to do that manually
    }

    public void TakeDamage()
    {
        //to be implemented
    }

    public void Die()
    {
        //to be implemented
    }
}
