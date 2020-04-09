using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour, IDamagable, IManagable
{

    private float tumble = 0.2f;
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
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }

    public void PhysicsRefresh()
    {
        
    }

    public void RegenHP(float dt)
    {
        //not required
    }
}
