using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour, IDamagable, IManagable
{
    public void Die()
    {
        //to be implemented;
    }

    public void TakeDamage(float damage)
    {
        //temp
        GameObject.Destroy(gameObject);
    }

    // Use this for initialization
    public void PostInitialize()
    {

    }

    // Update is called once per frame
    public void Refresh()
    {

    }

    public void Initialize()
    {
        
    }

    public void PhysicsRefresh()
    {
        
    }
}
