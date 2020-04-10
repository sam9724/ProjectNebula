using UnityEngine;
using System.Collections;

public class HomingMissile : Projectile, IDamagable // Since Homing missiles can be shot down
{

    Transform player;
    public float MovementSpeed = 5f;
    public float rotateSpeed = 5f;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        //throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        //throw new System.NotImplementedException();
    }

    public void RegenHP(float dt)
    {
        //not required
    }

    protected override void Explode()
    {
        // Explosion effect goes here.
    }
}
