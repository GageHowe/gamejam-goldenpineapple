using UnityEngine;
using UnityEngine.SceneManagement;

public class Win_DeathMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Timer;
    
    void start()
    {
        Time.timeScale = 0f;
        //Timer.SetActive(false);
       
    }
   
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(1);
    }

}
