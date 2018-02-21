using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    Animator optionsAnim;
    Animator pauseAnim;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    public static bool gameIsPaused = false;

    // Use this for initialization
    void Start ()
    {
        optionsAnim = optionsMenuUI.GetComponent<Animator>();
        pauseAnim = pauseMenuUI.GetComponent<Animator>();

        pauseMenuUI.GetComponent<CanvasGroup>().alpha = 0;
        pauseMenuUI.GetComponent<CanvasGroup>().interactable = false;
        pauseMenuUI.GetComponent<CanvasGroup>().blocksRaycasts = false;

        optionsMenuUI.GetComponent<CanvasGroup>().alpha = 0;
        optionsMenuUI.GetComponent<CanvasGroup>().interactable = false;
        optionsMenuUI.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        optionsAnim.SetTrigger("Close");
        pauseAnim.SetTrigger("Close");

        Time.timeScale = 1f;

        Cursor.visible = false;

        gameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;

        pauseMenuUI.GetComponent<CanvasGroup>().alpha = 0;
        pauseMenuUI.GetComponent<CanvasGroup>().interactable = false;
        pauseMenuUI.GetComponent<CanvasGroup>().blocksRaycasts = false;

        optionsMenuUI.GetComponent<CanvasGroup>().alpha = 0;
        optionsMenuUI.GetComponent<CanvasGroup>().interactable = false;
        optionsMenuUI.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    void Pause()
    {
        optionsAnim.SetTrigger("Open");
        pauseAnim.SetTrigger("Open");

        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;

        gameIsPaused = true;

        pauseMenuUI.SetActive(true);

        pauseMenuUI.GetComponent<CanvasGroup>().alpha = 1;
        pauseMenuUI.GetComponent<CanvasGroup>().interactable = true;
        pauseMenuUI.GetComponent<CanvasGroup>().blocksRaycasts = true;

        optionsMenuUI.GetComponent<CanvasGroup>().alpha = 1;
        optionsMenuUI.GetComponent<CanvasGroup>().interactable = true;
        optionsMenuUI.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void Options()
    {
        pauseMenuUI.GetComponent<CanvasGroup>().alpha = 0;
        pauseMenuUI.GetComponent<CanvasGroup>().interactable = false;
        pauseMenuUI.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Back()
    {
        pauseMenuUI.GetComponent<CanvasGroup>().alpha = 1;
        pauseMenuUI.GetComponent<CanvasGroup>().interactable = true;
        pauseMenuUI.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
