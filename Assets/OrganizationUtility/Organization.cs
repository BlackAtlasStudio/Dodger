using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrgUtility
{
    public class Organization : MonoBehaviour
    {
        private static Organization instance = null;
        private static object _lock = new object();
        private static bool applicationIsQuitting = false;

        public static Organization Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    Debug.LogWarning("Singleton Instance already destroyed");
                    return null;
                }
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType(typeof(Organization)) as Organization;

                        if (instance == null)
                        {
                            GameObject singleton = new GameObject();
                            instance = singleton.AddComponent<Organization>();
                            singleton.name = "(singleton) Organization";

                            DontDestroyOnLoad(singleton);
                        }
                    }
                    return instance;
                }
            }
        }

        public Transform DataParent
        {
            get
            {
                if (dataParent == null)
                {
                    dataParent = CreateParent("DATA");
                    DontDestroyOnLoad(dataParent.gameObject);
                }
                return dataParent;
            }
        }
        public Transform ManagerParent
        {
            get
            {
                if (managerParent == null)
                    managerParent = CreateParent("MANAGERS");
                return managerParent;
            }
        }
        public Transform OtherParent
        {
            get
            {
                if (otherParent == null)
                    otherParent = CreateParent("OTHER");
                return otherParent;
            }
        }
        public Transform WorldParent
        {
            get
            {
                if (worldParent == null)
                    worldParent = CreateParent("WORLD");
                return worldParent;
            }
        }

        private Transform dataParent;
        private Transform managerParent;
        private Transform otherParent;
        private Transform worldParent;

        private Transform CreateParent(string parentName)
        {
            Transform parent;
            parent = new GameObject().transform;
            parent.name = string.Format("[{0}]", parentName);
            return parent;
        }

        public void Generate()
        {
            GameObject data = GameObject.Find("[DATA]");
            dataParent = data == null ? CreateParent("DATA") : data.transform;
            GameObject managers = GameObject.Find("[MANAGERS]");
            managerParent = managers == null ? CreateParent("MANAGERS") : managers.transform;
            GameObject other = GameObject.Find("[OTHER]");
            otherParent = other == null ? CreateParent("OTHER") : other.transform;
            GameObject world = GameObject.Find("[WORLD]");
            worldParent = world == null ? CreateParent("WORLD") : world.transform;
        }
    }
}
