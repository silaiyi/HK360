using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Text diffcultText;
    void Update()
    {
    }
    public void StartGame(){
        SceneManager.LoadScene("MainGame");
    }
    public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit Game");
    }
    public void BackToMenu(){
        //SampleScene
        SceneManager.LoadScene("SampleScene");
    }
    public void EasyMode(){
        diffcultText.text = "Current Difficulty：Easy";
        GameManager.difficultyLevel=1;
    }
    public void NormalMode(){
        diffcultText.text = "Current Difficulty：Normal";
        GameManager.difficultyLevel=2;
    }
    public void HardMode(){
        diffcultText.text = "Current Difficulty：Hard";
        GameManager.difficultyLevel=3;
    }
    public void ExpertMode(){
        diffcultText.text = "Current Difficulty：Expert";
        GameManager.difficultyLevel=4;
    }
}
