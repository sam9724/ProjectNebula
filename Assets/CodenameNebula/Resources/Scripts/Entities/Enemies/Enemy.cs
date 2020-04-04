using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public bool isAlive;
    protected float hp;
    public virtual void Initialize(float _hp = 100)
    {   
        isAlive = true;
        hp = _hp;
    }
    public virtual void HitByProjectile(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log("Hp: " + hp);
            Die();
            isAlive = false;
        }
    }
    public virtual void Refresh()
    {
    }
    public virtual void Shoot() {  }
    public virtual void Die()
    {
        
    }

}
