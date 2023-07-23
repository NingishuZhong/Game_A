using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunc : MonoBehaviour
{
    public Animator Animals;
    public void EnterMainScene()
    {
        Animals.Play("Animals");
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void NextPanel()
    {
        PanelFunc.OpenPanel();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
