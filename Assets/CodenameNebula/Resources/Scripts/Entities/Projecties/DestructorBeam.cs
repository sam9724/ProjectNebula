using UnityEngine;
using System.Collections;

public class DestructorBeam : Projectile
{
    protected override void Explode()
    {
        // Explosion effect goes here.
    }

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
}
