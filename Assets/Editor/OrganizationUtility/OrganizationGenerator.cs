using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using OrgUtility;

public class OrganizationGenerator : MonoBehaviour {

    [MenuItem("Tools/Organization/Generate Parents")]
    public static void GenerateParents()
    {
        Generate();
    }

    public static void Generate()
    {
        GameObject data = GameObject.Find("[DATA]");
        if (data == null) CreateParent("DATA");
        GameObject managers = GameObject.Find("[MANAGERS]");
        if (managers == null) CreateParent("MANAGERS");
        GameObject other = GameObject.Find("[OTHER]");
        if (other == null) CreateParent("OTHER");
        GameObject world = GameObject.Find("[WORLD]");
        if (world == null) CreateParent("WORLD");
    }

    private static Transform CreateParent(string parentName)
    {
        Transform parent;
        parent = new GameObject().transform;
        parent.name = string.Format("[{0}]", parentName);
        return parent;
    }
}
