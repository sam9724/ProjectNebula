using UnityEngine;
using System.Collections.Generic;

// Next level has to be singleton.
public class GenericFactory<T, U> where T : MonoBehaviour where U : System.Enum
{
    public Dictionary<string, GameObject> prefabDict;

    public T CreateObject(U oType, Vector3 pos, Quaternion rot, Transform parent = null)
    {
        T obj;

        if (GenericObjectPool.Instance.TryDepool(oType.GetType(), out IPoolable poolable))
        {
            obj = (T)poolable;
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = GameObject.Instantiate(prefabDict[oType.ToString()], pos, rot).GetComponent<T>();
        }
        obj.transform.SetParent(parent);
        
        return obj;
    }

}
