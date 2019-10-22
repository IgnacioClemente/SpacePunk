using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    List<AIController> enemyShips;
    List<AIController> alliedShips;

    [SerializeField] PlayerController player;

    public static AIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Destroy(this);

        Instance = this;

        enemyShips = new List<AIController>();
        alliedShips = new List<AIController>();

        foreach (Transform child in transform)
        {
            var ship = child.GetComponent<AIController>();
            if (ship.Team == Team.Enemy)
                enemyShips.Add(ship);
            else
                alliedShips.Add(ship);
        }
        for (int i = 0; i < enemyShips.Count; i++)
        {
            enemyShips[i].SetShip(alliedShips);
        }

        for (int i = 0; i < alliedShips.Count; i++)
        {
            alliedShips[i].SetShip(enemyShips);
        }

        for (int i = 0; i < enemyShips.Count; i++)
        {
            if (enemyShips[i].gameObject.activeInHierarchy)
            {
                enemyShips[i].GetComponent<AIFlagController>().isInChargeOfTakingFlag = true;
                break;
            }
        }

        for (int i = 0; i < alliedShips.Count; i++)
        {
            if (alliedShips[i].gameObject.activeInHierarchy)
            {
                alliedShips[i].GetComponent<AIFlagController>().isInChargeOfTakingFlag = true;
                break;
            }
        }

        //Test
        Time.timeScale = 2;
    }

    public void FlagCarrierDied(AIController ship)
    {
        //enemyShips.Remove(ship);
        if (ship.Team == Team.Enemy)
        {
            for (int i = 0; i < enemyShips.Count; i++)
            {
                if (enemyShips[i].gameObject.activeInHierarchy)
                {
                    enemyShips[i].GetComponent<AIFlagController>().isInChargeOfTakingFlag = true;
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < alliedShips.Count; i++)
            {
                if (alliedShips[i].gameObject.activeInHierarchy)
                {
                    alliedShips[i].GetComponent<AIFlagController>().isInChargeOfTakingFlag = true;
                    return;
                }
            }
        }
        //si no encontro nave, ya sea enemigo o aliado, espera unos segundos y vuelve a intentar
        StartCoroutine(SetNewFlagCarrier(0.5f, ship));
    }

    IEnumerator SetNewFlagCarrier(float delay, AIController ship)
    {
        yield return new WaitForSeconds(delay);
        FlagCarrierDied(ship);
    }
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
