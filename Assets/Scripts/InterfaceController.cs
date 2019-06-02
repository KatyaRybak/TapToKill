using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceController : MonoBehaviour
{
    public GameObject endRoundMenu;
    public GameObject pauseMenu;
    public Slider healthSlider;
    public Text scoreText;
    public Text timerText;
    public Text scoreEnd;
    public Text scorePoint;

    public Button pauseButton;
    MainLogic logic;

    private void Start()
    {
        logic = MainLogic.instance;
    }
    public void UpdateText()
    {
        
        scoreText.text = logic.totalScore.ToString();
        timerText.text = string.Format("{0:D2}:{1:D2}", logic.timerMin, logic.timerSec);
    }

    public void ShowEndRoundMenu()
    {     
        endRoundMenu.SetActive(true);
        scoreEnd.text = scoreText.text;
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        Time.timeScale = 0;
        logic.isGamePaused = true;
    }

    public void ContinueGame()
    {
        pauseMenu.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1;
        logic.isGamePaused = false;
    }

    public void StartNewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void AddScorePointVisual(int point, Vector2 pos)
    {
        scorePoint.transform.position = pos;
        scorePoint.text = point.ToString();
        
    }

    public void ChangeHealth(float health)
    {
        if (healthSlider.value > 0)
        {
            healthSlider.value += health;
        }
        else
        {
            logic.PlayGameOver();
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
