using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagGameManager : MonoBehaviour
{
    public static FlagGameManager Instance { get; private set; }

    [Header("Flag Settings")]
    [SerializeField] Text playerTeamScore;
    [SerializeField] Text enemyTeamScore;

    private int enemyScore;
    private int playerScore;
    //private float timer = 3f;


    private void Awake()
    {
        if (Instance != null) Destroy(this);
        Instance = this;
    }

    private void Start()
    {
        playerTeamScore.text = "PlayerTeamScore: " + playerScore.ToString() + "/3";
        enemyTeamScore.text = "EnemyTeamScore: " + enemyScore.ToString() + "/3";
    }

    private void Update()
    {
        //timer -= Time.deltaTime;
    }

    public void ScoreUp(Team team)
    {
        if (team == Team.Enemy)
        {
            enemyScore++;
            enemyTeamScore.text = "EnemyTeamScore: " + enemyScore.ToString() + "/3";
            if (enemyScore >= 3)
                Win(team);
        }
        else
        {
            playerScore++;
            playerTeamScore.text = "PlayerTeamScore: " + playerScore.ToString() + "/3";
            if (playerScore >= 3)
                Win(team);
        }
    }

    public void Win(Team team)
    {
        GameManager.Instance.EndGame(team);
    }
}
