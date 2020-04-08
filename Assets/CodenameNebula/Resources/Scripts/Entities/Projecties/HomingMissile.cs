using UnityEngine;
using System.Collections;

public class HomingMissile : Projectile, IDamagable // Since Homing missiles can be shot down
{
    

    protected override void HitTarget(IDamagable targetHit, string layerName)
    {
        //throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {

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
