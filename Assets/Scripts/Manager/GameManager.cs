using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] Text countDownText;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] Text winText;
    [SerializeField] Button Menubutton;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        Instance = this;

        Menubutton.onClick.AddListener(GoBackToMenu);
    }

    public void EndGame(Team winner)
    {
        gameOverCanvas.SetActive(true);
        winText.text = winner == Team.Ally ? "You Win" : "You Lose";
        Time.timeScale = 0;
    }

    public void GoBackToMenu()
    {
        Time.timeScale = 1;
        SceneLoader.Instance.ChangeScene("MainMenu");
    }
}
