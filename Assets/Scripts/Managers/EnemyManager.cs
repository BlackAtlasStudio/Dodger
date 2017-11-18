using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
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

    /// <summary>
    /// Controls the move speed of all enemies in the game
    /// </summary>
    /// <value>The speed.</value>
    public static float MoveSpeed { get; private set; }

    /// <summary>
    /// Controls the MoveSpeed as a function of time
    /// </summary>
    public static AnimationCurve MoveSpeedCurve;

    // Used for editing the curve in editor
    public AnimationCurve moveSpeedCurve;

    private float timer;

    private void Awake()
    {
        MoveSpeedCurve = moveSpeedCurve;
    }

    private void Update()
    {
        //Update the MoveSpeed
        timer += Time.deltaTime;
        MoveSpeed = MoveSpeedCurve.Evaluate(timer);
    }
}
