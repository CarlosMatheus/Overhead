using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject pauseCanvas;
    private bool isPaused = false;

    private float previousValue;

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
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            previousValue = GameObject.Find("TutorialCanvas").GetComponent<CanvasGroup>().alpha;
            GameObject.Find("TutorialCanvas").GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseCanvas.SetActive(false);
        camera.GetComponent<Blur>().enabled = false;
        canvas.GetComponent<CanvasGroup>().alpha = 1;
        if (SceneManager.GetActiveScene().buildIndex == 4)
            GameObject.Find("TutorialCanvas").GetComponent<CanvasGroup>().alpha = previousValue;
    }
}
