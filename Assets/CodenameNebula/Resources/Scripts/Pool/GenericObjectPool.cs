using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObjectPool// where T: IPoolable
{
    #region
    private static GenericObjectPool instance;

    private GenericObjectPool() { }

    public static GenericObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GenericObjectPool();
            }
            return instance;
        }
    }
    #endregion Singleton

    Dictionary<System.Type, Queue<IPoolable>> pool = new Dictionary<System.Type, Queue<IPoolable>>();

    public void PoolObject(System.Type type, IPoolable obj)
    {
        if (pool.Count > 0 && pool.ContainsKey(type))
            pool[type].Enqueue(obj);
        else
            pool.Add(type, new Queue<IPoolable>(new[] { obj }));
    }

    public bool TryDepool(System.Type type, out IPoolable toRet)
    {
        toRet = null;

        if (pool.Count > 0 && pool.ContainsKey(type) && pool[type].Count > 0)
        {
            toRet = pool[type].Dequeue();
            return true;
        }
        return false;        
    }
}
