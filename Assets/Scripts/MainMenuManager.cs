using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; private set; }

    [SerializeField] Button playButton;
    [SerializeField] Button QuitButton;

    [Header("Tween Play Sequence")]
    [SerializeField] Image captureTheFlagButton;
    [SerializeField] Text captureTheFlagText;
    [SerializeField] Image bombButton;
    [SerializeField] Text bombText;
    [SerializeField] Image monsterButton;
    [SerializeField] Text monsterText;

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        Instance = this;

        playButton.onClick.AddListener(ShowUI);
        QuitButton.onClick.AddListener(Application.Quit);
        captureTheFlagButton.GetComponent<Button>().onClick.AddListener(StartFlagGame);
        bombButton.GetComponent<Button>().onClick.AddListener(StartBombGame);
        monsterButton.GetComponent<Button>().onClick.AddListener(StartMonsterGame);
    }

    public void ShowUI()
    {
        Sequence playSequence = DOTween.Sequence();
        playSequence.Append(captureTheFlagButton.DOFade(1, 0.5f).OnStart(() => captureTheFlagText.DOFade(1, 0.5f)));
        playSequence.Append(bombButton.DOFade(1, 0.5f).OnStart(() => bombText.DOFade(1, 0.5f)));
        playSequence.Append(monsterButton.DOFade(1, 0.5f).OnStart(() => monsterText.DOFade(1, 0.5f)));
    }

    public void StartFlagGame()
    {
        SceneLoader.Instance.ChangeScene("GameFlag");
    }

    public void StartBombGame()
    {
        SceneLoader.Instance.ChangeScene("GameBomb");
    }

    public void StartMonsterGame()
    {
        SceneLoader.Instance.ChangeScene("GameMonster");
    }
}
