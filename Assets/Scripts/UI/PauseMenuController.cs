using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("Menu Settings")]
    public bool stopTimeOnPause;
    public bool IsPaused { get; private set; }

    private Canvas menuCanvas;

    public void Pause()
    {
        menuCanvas.enabled = true;
        IsPaused = true;
        if (stopTimeOnPause) Time.timeScale = 0f;
    }

    public void Resume()
    {
        menuCanvas.enabled = false;
        IsPaused = false;
        if (stopTimeOnPause) Time.timeScale = 1f;
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        menuCanvas = GetComponent(typeof(Canvas)) as Canvas;
        menuCanvas.enabled = false;
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
