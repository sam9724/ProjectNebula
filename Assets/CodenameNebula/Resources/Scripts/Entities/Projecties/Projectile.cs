using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour, IPoolable
{

    public ProjectileFactory.ProjectileType projType;
    public Vector3 target;
    public float speed = 10;
    //public GameObject collisionExplosion;

    public void Initialize()
    {

    }

    public void PhysicsRefresh()
    {

    }

    public void PostInitialize()
    {

    }

    public void Refresh(float dt)
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

    void Died()
    {
        gameObject.SetActive(false);
        ProjectileManager.Instance.ProjectileDied(this);
    }

    /*void Explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = Instantiate<GameObject>(collisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }*/
}
