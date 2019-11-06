using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance { get; private set; }
}

   /* public void PauseGame()
    {
        for (int i = 0; i < alliedShips.Count; i++)
        {
            alliedShips[i].GetComponent<AIFlagController>().Pause();
        }
        for (int i = 0; i < enemyShips.Count; i++)
        {
            enemyShips[i].GetComponent<AIFlagController>().Pause();
        }
        player.Pause();
    }

    public void ResumeGame()
    {
        for (int i = 0; i < alliedShips.Count; i++)
        {
            alliedShips[i].GetComponent<AIFlagController>().Resume();
        }
        for (int i = 0; i < enemyShips.Count; i++)
        {
            enemyShips[i].GetComponent<AIFlagController>().Resume();
        }
        player.Resume();
    }
}

public interface IPaussable
{
    void Pause();
}

public interface IResume
{
    void Resume();
}*/
