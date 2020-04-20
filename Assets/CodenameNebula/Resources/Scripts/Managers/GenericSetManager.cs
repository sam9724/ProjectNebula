using UnityEngine;
using System.Collections.Generic;

public class GenericSetManager<T> where T : IManagable
{

    public Stack<T> toRemove;
    public Stack<T> toAdd;
    public HashSet<T> managingSet;

    public virtual void Initialize()
    {
        managingSet = new HashSet<T>();
        toRemove = new Stack<T>();
        toAdd = new Stack<T>();
    }

    public virtual void PhysicsRefresh(float dt)
    {
        
    }

    public virtual void PostInitialize()
    {
        
    }

    public virtual void Refresh(float dt)
    {
        foreach (T t in managingSet)
        {
            t.Refresh(dt);
        }

        while (toRemove.Count > 0)
            managingSet.Remove(toRemove.Pop());

        while (toAdd.Count > 0)
            managingSet.Add(toAdd.Pop());
    }
}
