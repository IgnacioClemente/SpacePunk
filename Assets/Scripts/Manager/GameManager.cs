using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] float timeToStartMatch = 3;
    [SerializeField] Text countDownText;
    [SerializeField] Image pauseBackground;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] Text winText;
    [SerializeField] Button Menubutton;
    [Header("Paussable Units")]
    [SerializeField] private List<Unit> paussableObjects;

    private bool countingDown;
    private float countDownTimer;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;

        Menubutton.onClick.AddListener(GoBackToMenu);
    }

    private void Start()
    {
        countingDown = true;
        countDownTimer = timeToStartMatch;
        PauseUnits();
        pauseBackground.gameObject.SetActive(true);
    }

    //Test
    private void Update()
    {
        if(countingDown)
        {
            countDownTimer -= Time.deltaTime;
            countDownText.text = countDownTimer.ToString("#");
            if(countDownTimer <= 0)
            {
                countingDown = false;
                countDownText.text = "Go!";
                pauseBackground.DOFade(0, .25f).SetEase(Ease.OutQuad).OnComplete(()=>pauseBackground.gameObject.SetActive(false));
                countDownText.DOFade(0, 1f).SetEase(Ease.OutQuad);
                ResumeUnits();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
            PauseGame();
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ResumeGame();
        if (Input.GetKeyDown(KeyCode.Alpha5))
            Time.timeScale = 5;
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

    public void PauseGame()
    {
        PauseUnits();
        pauseBackground.gameObject.SetActive(true);
        pauseBackground.DOFade(.4f, 0).SetEase(Ease.OutQuad);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseBackground.DOFade(0, .25f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            ResumeUnits();
            pauseBackground.gameObject.SetActive(false);
        });
        
        Time.timeScale = 1;
    }

    public void PauseUnits()
    {
        for (int i = 0; i < paussableObjects.Count; i++)
        {
            paussableObjects[i].Pause();
        }
    }

    public void ResumeUnits()
    {
        for (int i = 0; i < paussableObjects.Count; i++)
        {
            paussableObjects[i].Resume();
        }
    }
}
