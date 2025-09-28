using System.Diagnostics.CodeAnalysis;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject OtherCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SecretWin()
    {
        MainCanvas.SetActive(true);
        OtherCanvas.SetActive(false);
    }
}
