using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

#region SINGLETON
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent(typeof(GameManager)) as GameManager;
                    singleton.name = typeof(GameManager).ToString();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }
#endregion

    [SerializeField]
    public static Vector3 ScreenBoundsMin { get; private set; }
    [SerializeField]
    public static Vector3 ScreenBoundsMax { get; private set; }
    [SerializeField]
    public static Vector3 CamOffset { get; private set; }
    [SerializeField]
    [Range(0f, 5f)]
    public float edgeBuffer;

    private void Start()
    {
        CamOffset = Camera.main.transform.position;

        Ray minRay = Camera.main.ScreenPointToRay(Vector3.zero);
        Ray maxRay = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        RaycastHit hit;

        if (Physics.Raycast(minRay, out hit, LayerMask.GetMask("PlayField")))
            ScreenBoundsMin = hit.point;
        else
            ScreenBoundsMin = new Vector3(-10f, 0f, -5f);

        if (Physics.Raycast(maxRay, out hit, LayerMask.GetMask("PlayField")))
            ScreenBoundsMax = hit.point;
        else
            ScreenBoundsMax = new Vector3(10f, 0f, 5f);

        ScreenBoundsMin -= CamOffset;
        ScreenBoundsMax -= CamOffset;

        Vector3 edgeBufferVec = new Vector3(edgeBuffer, 0f, edgeBuffer);
        ScreenBoundsMin += edgeBufferVec;
        ScreenBoundsMax -= edgeBufferVec;
    }
}
