using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    public GameObject TimerObject;
    public GameObject Win;
    public GameObject Death;

    private void Start()
    {
        Win.SetActive(false);
        Death.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            Win.SetActive(true);
            TimerObject.SetActive(false);
            Time.timeScale = 0f;
        }
        
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
