using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public Dictionary<string, ObjectPool> pools = new Dictionary<string, ObjectPool>();
    public int defaultMaxEntities = 10;

#region SINGLETON
    private static ObjectPoolManager _instance;

    public static ObjectPoolManager Instance
    {
        get{
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(ObjectPoolManager)) as ObjectPoolManager;

                if (_instance == null) {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent(typeof(ObjectPoolManager)) as ObjectPoolManager;
                    singleton.name = typeof(ObjectPoolManager).ToString();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }
#endregion

    /// <summary>
    /// Creates a new ObjectPool with an identifier and prefab.
    /// </summary>
    /// <param name="ID">Identifier.</param>
    /// <param name="prefab">Prefab.</param>
    public ObjectPool AddPool(string ID, GameObject prefab)
    {
        return AddPool(ID, prefab, defaultMaxEntities);
    }

    /// <summary>
    /// Creates a new ObjectPool with an identifier and prefab with a max number of entities in the pool.
    /// </summary>
    /// <param name="ID">Identifier.</param>
    /// <param name="prefab">Prefab.</param>
    /// <param name="maxEntity">Max entity count.</param>
    public ObjectPool AddPool(string ID, GameObject prefab, int maxEntity)
    {
        ObjectPool pool = Instance.gameObject.AddComponent<ObjectPool>() as ObjectPool;
        pool.ID = ID;
        pool.prefab = prefab;
        pool.maxEntities = maxEntity;
        pools.Add(ID, pool);
        return pool;
    }

    /// <summary>
    /// Gets a pool from a string identifier.
    /// </summary>
    /// <returns>The pool.</returns>
    /// <param name="ID">Identifier.</param>
    public ObjectPool GetPool(string ID)
    {
        return pools[ID];
    }

    /// <summary>
    /// Gets a pool from its pooled prefab.
    /// </summary>
    /// <returns>The pool.</returns>
    /// <param name="prefab">Prefab.</param>
    public ObjectPool GetPool(GameObject prefab)
    {
        foreach (ObjectPool p in pools.Values) {
            if (p.prefab = prefab)
                return p;
        }
        return null;
    }

    /// <summary>
    /// Removes a pool with ID.
    /// </summary>
    /// <param name="ID">Identifier.</param>
    public void RemovePool(string ID)
    {
        pools.Remove(ID);
    }

    /// <summary>
    /// Removes a pool spawning a certain prefab/
    /// </summary>
    /// <param name="prefab">Prefab.</param>
    public void RemovePool(GameObject prefab)
    {
        ObjectPool pool = GetPool(prefab);
        if (pool != null)
            pools.Remove(pool.ID);
    }

    /// <summary>
    /// Removes a pool.
    /// </summary>
    /// <param name="pool">Pool.</param>
    public void RemovePool(ObjectPool pool)
    {
        pools.Remove(pool.ID);
    }
}