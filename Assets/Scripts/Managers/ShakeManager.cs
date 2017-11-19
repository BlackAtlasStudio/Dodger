using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeManager : MonoBehaviour
{
    #region SINGLETON
    private static ShakeManager _instance;

    public static ShakeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(ShakeManager)) as ShakeManager;

                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent(typeof(ShakeManager)) as ShakeManager;
                    singleton.name = typeof(ShakeManager).ToString();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }
    #endregion

    public static float SmallIntensity = 0.1f;
    public static float MediumIntensity = 0.2f;
    public static float LargeIntensity = 0.5f;
    public static float HugeIntensity = 1f;

    private Transform camTrans;

    private bool shaking;
    private float shakeIntensity;
    private float shakeDuration;
    private float shakeTimer;
    private float shakeSmoothing;

    private Vector3 startingPosition;
    private Vector3 curPos;

    private void Start()
    {
        camTrans = Camera.main.transform;
        startingPosition = camTrans.position;
    }

    private void LateUpdate()
    {
        if (shaking)
        {
            //Calculate shake displacement
            Vector3 target;
            target = Random.insideUnitSphere.normalized * shakeIntensity;
            curPos = Vector3.Lerp(curPos, target, Time.deltaTime * shakeSmoothing);

            camTrans.position = curPos + startingPosition;

            shakeTimer += Time.deltaTime;
            if (shakeTimer >= shakeDuration)
            {
                shaking = false;
                shakeTimer = 0f;
            }
        }
    }

    public static void Shake(float intensity, float duration)
    {
        Instance.shaking = true;
        Instance.shakeIntensity = intensity;
        Instance.shakeDuration = duration;
        Instance.shakeTimer = 0f;
        Instance.shakeSmoothing = 10f;
    }

    public static void Shake(float intensity, float duration, float smoothing)
    {
        Instance.shaking = true;
        Instance.shakeIntensity = intensity;
        Instance.shakeDuration = duration;
        Instance.shakeTimer = 0f;
        Instance.shakeSmoothing = smoothing;
    }
}
