using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject pauseCanvas;
    private bool isPaused = false;

    public bool IsPaused()
    {
        return isPaused;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !GetComponent<DeathManager>().IsDead())
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        pauseCanvas.SetActive(true);
        camera.GetComponent<Blur>().NumberOfIterations = 10;
        camera.GetComponent<Blur>().enabled = true;
        canvas.GetComponent<CanvasGroup>().alpha = 0.1f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseCanvas.SetActive(false);
        camera.GetComponent<Blur>().enabled = false;
        canvas.GetComponent<CanvasGroup>().alpha = 1;
    }
}
