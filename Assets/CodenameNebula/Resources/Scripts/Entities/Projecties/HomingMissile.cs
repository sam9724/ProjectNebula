using UnityEngine;
using System.Collections;

public class HomingMissile : Projectile, IDamagable // Since Homing missiles can be shot down
{

    Transform player;
    public float MovementSpeed = 5f;
    public float rotateSpeed = 5f;
    public float missileExplosionRange = 5f;
    float missileHealth = 5f; 
    protected override void HitTarget(IDamagable targetHit, string layerName)
    {
        //throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {
        player = player ?? PlayerManager.Instance.pilot.transform;
    }


    protected override void FollowTarget(float dt)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - player.position), rotateSpeed * Time.deltaTime);
        transform.position += transform.forward * MovementSpeed * Time.deltaTime;

        if (PlayerInRange())
        {
            Explode();
            Die();
            //blow up
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void TakeDamage(float damage)
    {
        missileHealth -= damage;
        

        if (missileHealth <= 0)
        {
            Explode();
            Die();
        }
    }
    bool PlayerInRange()
    {
        return (Vector3.SqrMagnitude(transform.position - player.position) < missileExplosionRange*missileExplosionRange);
    }
    public void RegenHP(float dt)
    {
        //not required
    }

    protected override void Explode()
    {
        // Explosion effect goes here. Sound line is put in first cause sound takes a bit to load
        AudioSource.PlayClipAtPoint(AudioManager.Instance.soundDict["explosionSound"], transform.position);
        ParticleFactory.Instance.CreateParticle(ParticleFactory.ParticleType.HomingMissileExplosion, transform.position,Quaternion.identity);
    }

    public void Die()
    {
        base.Died();
    }
}
