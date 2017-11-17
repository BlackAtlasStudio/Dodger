using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> instances = new List<GameObject>();
    public GameObject prefab;
    public string ID;
    public int maxEntities;

    private Vector3 storage = new Vector3(1000f, 1000f, 1000f);
    private Transform parent;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ObjectPool"/> class with string ID, GameObject prefab and a max entities integer.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="p">P.</param>
    /// <param name="max">Max.</param>
    /*public ObjectPool (string id, GameObject p, int max)
    {
        ID = id;
        prefab = p;
        maxEntities = max;
    }*/

    public GameObject Instantiate(Vector3 pos, Quaternion rot)
    {
        GameObject oldest;

        if (instances.Count == maxEntities)
        {
            oldest = instances[0];
            instances.RemoveAt(0);
            instances.Insert(instances.Count, oldest);
            oldest.transform.position = pos;
            oldest.transform.rotation = rot;
        }
        else
        {
            oldest = Instantiate(prefab, pos, rot, parent);
            instances.Insert(instances.Count, oldest);
        }

        oldest.SetActive(true);

        return oldest;
    }

    public void Destroy(GameObject obj)
    {
        if (obj != null)
        {
            obj.transform.position = storage;
            obj.SetActive(false);
        }
    }

    private void Start()
    {
        parent = new GameObject().transform;
        parent.name = ID;

        //Creates a set number of starting entities
        instances = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            GameObject o = Instantiate(prefab, storage, Quaternion.identity, parent);
            instances.Add(o);
            o.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        //Destroy all objects in pool
        foreach (GameObject o in instances)
        {
            GameObject.Destroy(o);
        }

        //Remove pool from manager
        ObjectPoolManager.Instance.RemovePool(ID);
    }
}