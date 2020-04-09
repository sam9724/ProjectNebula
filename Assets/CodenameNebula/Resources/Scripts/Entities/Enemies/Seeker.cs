using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Seeker : MonoBehaviour, IMinion
{

    public float playerDetectionRange = 225f;
    public float rotateSpeed = 5f;
    public float MovementSpeed = 5f;
    Transform player;

    public bool MoveWithBoss { get; set; }
    public bool SeekPlayer { get; set; }
    public bool ChasePlayer { get; set; }
    public bool Wander { get; set; }
    public CharacterStats CharStats { get; set; }
    public bool IsAlive { get; set; }

    /*public IPoolable Dequeue()
    {
        // stub implementation
        return this;
    }*/

    public void Die()
    {
        //to be implemented
    }

    /*public void Enqueue()
    {

    }*/

    public void Initialize()
    {
        CharStats = new CharacterStats(10, 0);
    }

    public void PhysicsRefresh()
    {
        
    }

    public void PostInitialize()
    {
        
    }

    public void Refresh()
    {
        
    }

    public void RegenHP(float dt)
    {
        //throw new System.NotImplementedException();
    }

    public void TakeDamage()
    {
        
    }

    public void TakeDamage(float damage)
    {
        //throw new System.NotImplementedException();
    }

    bool PlayerInRange()
    {
        return (Vector3.SqrMagnitude(transform.position - player.position) < playerDetectionRange);
    }

    public void Update()
    {
        player = player ?? PlayerManager.Instance.pilot.transform;

        Debug.Log("player" + player.name);

        if (PlayerInRange())
        {
            //do stuff
        }
        else
        {
            //look for player
        }
    }
}
