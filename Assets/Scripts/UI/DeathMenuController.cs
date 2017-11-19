using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuController : MonoBehaviour
{
    public bool pauseOnDeath;
    private Canvas deathCanvas;

    public void OnDeath()
    {
        deathCanvas.enabled = true;
        if (pauseOnDeath) Time.timeScale = 0f;
    }

    public void Restart()
    {
        deathCanvas.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        deathCanvas.enabled = false;
        SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        deathCanvas = GetComponent(typeof(Canvas)) as Canvas;
        if (deathCanvas == null){
            Debug.Log("Failed to find deathCanvas");
        }
        deathCanvas.enabled = false;
    }
}
