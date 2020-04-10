using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour, IDamagable, IManagable
{
    public CharacterStats CharStats { get; set; }

    private float tumble = 0.2f;
    public void Die()
    {
        AstroidManager.Instance.DestroyAsteroid(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        CharStats.health -= damage;
        if (CharStats.health <= 0)
        {
            Explode();
            Die();
        }
    }

    // Use this for initialization
    public void PostInitialize()
    {

    }

    // Update is called once per frame
    public void Refresh(float dt)
    {

    }

    public void Initialize()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
        CharStats = new CharacterStats(15, 0);
    }

    public void PhysicsRefresh(float dt)
    {
        
    }
    void Explode()
    {
        // Explosion effect goes here. Sound line is put in first cause sound takes a bit to load
        AudioSource.PlayClipAtPoint(AudioManager.Instance.soundDict["explosionSound"], transform.position);
        ParticleFactory.Instance.CreateParticle(ParticleFactory.ParticleType.HomingMissileExplosion, transform.position, Quaternion.identity);
    }
    public void RegenHP(float dt)
    {
        //not required
    }
}
