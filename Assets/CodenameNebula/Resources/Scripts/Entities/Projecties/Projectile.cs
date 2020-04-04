using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour, IPoolable
{

    public ProjectileFactory.ProjectileType projType;
    public Vector3 target;
    public float speed = 10;
    //public GameObject collisionExplosion;

    public virtual void Initialize()
    {

    }

    public virtual void PhysicsRefresh()
    {

    }

    public virtual void PostInitialize()
    {

    }

    public virtual void Refresh(float dt)
    {
        FollowTarget(dt);
    }

    protected virtual void FollowTarget(float dt)
    {
        if (target != null)
        {
            if (transform.position == target)
            {
                //Explode();
                Died();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, speed * dt);
        }
    }

    protected void Died()
    {
        gameObject.SetActive(false);
        ProjectileManager.Instance.ProjectileDied(this);
    }

    /*protected virtual void Explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = Instantiate<GameObject>(collisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }*/
}
