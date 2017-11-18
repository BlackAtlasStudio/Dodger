using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [Header("Menu Settings")]
    public bool stopTimeOnPause;
    public bool IsPaused { get; private set; }

    private Canvas menuCanvas;

    public void Pause()
    {
        IsPaused = true;
        if (stopTimeOnPause) Time.timeScale = 0f;
    }

    public void Resume()
    {
        IsPaused = false;
        if (stopTimeOnPause) Time.timeScale = 1f;
    }

    private void Start()
    {
        menuCanvas = GetComponent(typeof(Canvas)) as Canvas;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused) Resume();
            else Pause();
        }
    }
}
