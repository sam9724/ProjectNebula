using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour, IPoolable
{

    public ProjectileFactory.ProjectileType projType;
    public Vector3 target;
    public float speed = 10;
    float maxDistRayCast = 2;
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
            if (Physics.Raycast(transform.position, target, out RaycastHit hit, maxDistRayCast))
            {
                if (hit.transform.gameObject.TryGetComponent(out IDamagable d))
                    HitTarget(d, LayerMask.LayerToName(hit.transform.gameObject.layer));
                Explode();
                Died();
                return;
            } 
            else if(transform.position == target)
            {
                Explode();
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

    protected abstract void Explode();

    protected abstract void HitTarget(IDamagable targetHit, string layerName);
}
