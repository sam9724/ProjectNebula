using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;


public class Pilot : PilotBehavior, IBasePlayer, IDamagable, IShielded
{

    public float ShieldCooldown { get; set; }
    public bool StalkEnemy { get; set; }
    [SerializeField]
    public CharacterStats CharStats { get; set; }
    float rotationSpeed = 15f;
    public float boost;
    float boostCooldown;

    float moveSpeed = 70f;

    float maxHealth;
    float maxShield;
    float healthRegen = 2;
    float notUnderAttackTimer;
    float regenCooldown = 4;

    float shieldRegen = 5;
    float shieldRepairSpeed = 1;
    public Rigidbody rb;

    public Transform gunnerSpawnPos;

    DynamicJoystick DynamicJoystick;
    DynamicJoystick AltitudeJoystick;

    Transform shield;
    Material shieldMat;

    //required coz we don't control the spawning of networked objects in the scene.
    public void Start()
    {
        PlayerManager.Instance.pilot = this;
        gunnerSpawnPos = transform.Find("P2SpawnPoint");
        shield = transform.Find("PlayerShield");
        rb = gameObject.GetComponent<Rigidbody>();

        Initialize();
    }
    public void Initialize()
    {
        GameObject.FindGameObjectWithTag("dynamicjoystick")?.TryGetComponent<DynamicJoystick>(out DynamicJoystick);
        GameObject.FindGameObjectWithTag("Altitude")?.TryGetComponent<DynamicJoystick>(out AltitudeJoystick);
        CharStats = new CharacterStats();
        maxHealth = CharStats.health;
        maxShield = CharStats.shield;
        shieldMat = shield.GetComponent<Renderer>().material;
        ShieldCooldown = 5;
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
        transform.Rotate(0, direction.x*dt* rotationSpeed, 0);

        Vector3 acceleration = transform.forward * DynamicJoystick.Vertical;
        rb.AddForce(acceleration * moveSpeed * dt, ForceMode.VelocityChange);

        //if (transform.rotation.x > -30f && transform.position.x < 30f)
        {
            Vector3 altitudeTilt = transform.forward * AltitudeJoystick.Vertical;
            Debug.Log("Altitude"+altitudeTilt);
            transform.Rotate(-altitudeTilt.z, 0, 0);
        }
#else
        rb.AddRelativeForce(Vector3.forward * InputManager.Instance.inputPkg.throttle * dt * moveSpeed);
        rb.AddRelativeTorque(new Vector3(InputManager.Instance.inputPkg.pitch, InputManager.Instance.inputPkg.yaw, 0) * dt * moveSpeed);
#endif

        // If we are the owner of the object we should send the new position
        // and rotation across the network for receivers to move to in the above code
        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;

        // Note: Forge Networking takes care of only sending the delta, so there
        // is no need for you to do that manually
        notUnderAttackTimer += dt;
        if (notUnderAttackTimer >= regenCooldown)
        {
            RegenHP(dt);
            RegenShield(dt);
        }
            
    }

    public void TakeDamage(float damage)
    {
        if (CharStats.shield > 0)
        {
            TakeShieldDamage(damage * 2);
            return;
        }
        CharStats.health -= damage;
        notUnderAttackTimer = 0;

        if (CharStats.health <= 0)
            Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void TakeShieldDamage(float damage)
    {
        CharStats.shield -= damage;
        if (CharStats.shield <= 0)
            BreakShield();
    }

    public void BreakShield()
    {
        shieldMat.SetFloat("_ShieldSlider", 1);
        shield.gameObject.SetActive(false);
    }

    public void RegenHP(float dt)
    {
        if (CharStats.health < maxHealth)
            CharStats.health = Mathf.Clamp(CharStats.health += healthRegen * dt, 0, maxHealth);  
    }

    public void RegenShield(float dt)
    {
        RepairShield(dt);
        if (CharStats.shield < maxShield)
            CharStats.shield = Mathf.Clamp(CharStats.shield += shieldRegen * dt, 0, maxShield);
    }

    void RepairShield(float dt)
    {
        if (CharStats.shield <= 0)
            shield.gameObject.SetActive(true);

        if (CharStats.shield < maxShield)
            shieldMat.SetFloat("_ShieldSlider", Mathf.Lerp(1, 0, shieldRepairSpeed *dt));
    }
}
