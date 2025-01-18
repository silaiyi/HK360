using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI,pauseBtn,TotalMenu,hideBte,ChessBoard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseBtn.SetActive(true);
        TotalMenu.SetActive(true);
        hideBte.SetActive(true);
        ChessBoard.SetActive(true);
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseBtn.SetActive(false);
        TotalMenu.SetActive(false);
        hideBte.SetActive(false);
        ChessBoard.SetActive(false);
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    public void LoadMenu()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Confined;        
        SceneManager.LoadScene("MiniGameMenu");
        Debug.Log("Loading Menu");
    }

    public void QuitGame()
    {
        GameIsPaused = false;
        Application.Quit();
        Debug.Log("Quitting game");
    }
}
