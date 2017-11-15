using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> instances = new List<GameObject>();
    public GameObject prefab;
    public string ID;
    public int maxEntities;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ObjectPool"/> class with string ID, GameObject prefab and a max entities integer.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="p">P.</param>
    /// <param name="max">Max.</param>
    public ObjectPool (string id, GameObject p, int max)
    {
        ID = id;
        prefab = p;
        maxEntities = max;
    }

    private void Start()
    {
        instances = new List<GameObject>();

        //TODO Spawn a set number of starting instances for the pool to draw from
    }

    private void OnDestroy()
    {
        //TODO Destroy all entities in the pool
    }
}